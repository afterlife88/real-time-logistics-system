(function (angular) {
  'use strict';

  angular
    .module('app')
    .factory('transactionHub', transactionHub);

  transactionHub.$inject = ['Hub', 'appConfig'];

  function transactionHub(Hub, appConfig) {

    //declaring the hub connection 
    var hub = new Hub('TransactionHub',
    {
      rootPath: appConfig.signalrUrl,
      //server side methods
      methods: ['getBalancesByKardex',
                'getBalancesByOrganizationId',
                'getLedgerTransactions',
                'getLedgerTransactionDetails',
                'addTransaction'],

      //handle connection error
      errorHandler: function (error) {
        console.error(error);
      }
    });

    var service = {
      initDone: initDone,
      getBalancesByOrganizationId: getBalancesByOrganizationId,
      getBalancesByKardex: getBalancesByKardex,
      getLedgerTransactions: getLedgerTransactions,
      getLedgerTransactionDetails: getLedgerTransactionDetails,
      addTransaction: addTransaction
    };

    return service;

    function getBalancesByOrganizationId(organizationId) {
      var request = {
        OrganizationId: organizationId
      }
      var res = hub.getBalancesByOrganizationId(request);
      return res;
    }

    function getBalancesByKardex(kardex) {
      var request = {
        Kardex: kardex
      };
      var res = hub.getBalancesByKardex(request);
      return res;
    }

    function getLedgerTransactions(ordId, cargoId, skip, take) {
      var request = {
        OrganizationId: ordId,
        CargoId: cargoId,
        Skip: skip,
        Take: take
      };
      var res = hub.getLedgerTransactions(request);
      return res;
    }

    function getLedgerTransactionDetails(ledgerTransactionId) {
      var request = {
        LedgerTransactionId: ledgerTransactionId
      };
      var res = hub.getLedgerTransactionDetails(request);
      return res;
    }

    function addTransaction(transactionType, amount, comments, srcOrgId, targetOrgId, cargoId) {
      var request = {
        AddTransaction: {
          TransactionType: transactionType,
          Amount: amount,
          Comments: comments,
          SourceOrganizationId: srcOrgId,
          TargetOrganizationId: targetOrgId,
          CargoId: cargoId
        }
      }
      var res = hub.addTransaction(request);
      return res;
    }
    function initDone() {
      return hub.promise.done();
    }
  }
})(angular);