(function (angular) {
  'use strict';

  angular
      .module('app')
      .controller('balanceTransactionsController', balanceTransactionsController);

  balanceTransactionsController.$inject = ['$location', 'organizationHub', 'Alertify', 'transactionHub'];

  function balanceTransactionsController($location, organizationHub, Alertify, transactionHub) {
    var vm = this;
    //vm.showModal = showModal;
    vm.click = click;


    function click() {
      transactionHub.addTransaction(1, 5, "Some comment", 2, 1, 1)
        .then(function (response) {
          console.log(response);
        }, function (error) {
          Alertify.error(error);
        });
    }
    //function showModal() {
    //  console.log('click');
    //}

    // It's like constructor, all requests to get data from API organization hub place inside successful promise
    organizationHub.initDone().then(function () {
      getTestOrganization();
    }, function (error) {
      Alertify.error(error);
    });

    transactionHub.initDone().then(function () {
      getLedgerTransactionDetails();
      getLedgerTransactions();
    }, function (error) {
      Alertify.error(error);
    });


    function getLedgerTransactionDetails() {
      transactionHub.getLedgerTransactionDetails(5).then(function (response) {
        console.log("Test data from transaction 1 hub: ", response);
      }, function (error) {
        console.log(error);
        Alertify.error(error);
      });
    }

    function getLedgerTransactions() {
      transactionHub.getLedgerTransactions('s', 's', 1, 2).then(function (response) {
        console.log("Test data from transaction 2 hub: ", response);
      }, function (error) {
        console.log(error);
        Alertify.error(error);
      });
    }


    function getTestOrganization() {
      organizationHub.getOrganizationsByKardex("10203041").then(function (response) {
        console.log("Test data from organization hub: ", response);
      }, function (error) {
        console.log(error);
        Alertify.error(error);
      });
    }
  }
})(angular);