(function (angular) {
    'use strict';

    angular
        .module('app')
        .controller('transactionsController', transactionsController);

    transactionsController.$inject = ['$location', 'organizationHub', 'Alertify', 'transactionHub', '$routeParams',
    '$scope', '$route', 'localStorageService', '$uibModal'];

    function transactionsController($location, organizationHub, Alertify, transactionHub,
      $routeParams, $scope, $route, localStorageService, $uibModal) {
        var vm = this;
        vm.kardex = $routeParams.kardex;
        vm.ean = $routeParams.ean;
        vm.transactions = [];
        vm.tranasctionDetails = tranasctionDetails;

        // Filters
        vm.searchById = '';
        vm.searchTimestamp = '';
        vm.searchTransactionType = '';
        vm.searchAmount = '';

        setBreadcrump();

        transactionHub.initDone().then(function () {
            getLedgerTransactions($routeParams.organizationId, $routeParams.cargoId);
        }, function (error) {
            Alertify.error(error);
        });

        function getLedgerTransactions(orgId, cargoId) {
            transactionHub.getLedgerTransactions(orgId, cargoId, 0, 7).then(function (response) {
                if (response.ServiceStatus === 0)
                    vm.transactions = response.LedgerTransactions;
                else
                    Alertify.error(response.ErrorMessage);
                $scope.$apply();
            }, function (error) {
                console.log(error);
                Alertify.error(error);
            });
        }

        /**
         * Handler for transaction details, getting ledger transaction details from backend and opening modal on success
         * @param {} item 
         * @returns {} 
         */
        function tranasctionDetails(item) {
            transactionHub.getLedgerTransactionDetails(item.Id).then(function (response) {
                if (response.ServiceStatus === 0)
                    openTransactionModal(response.LedgerTransaction);
                else
                    Alertify.error(response.ErrorMessage);
            }, function (error) {
                console.log(error);
                Alertify.error(error);
            });
        }

        /**
        * Set breadcrump for the navigation
        * @returns {} 
        */
        function setBreadcrump() {
            $route.current.$$route.breadcrumb[$route.current.$$route.breadcrumb.length - 2].title = localStorageService.get('previousrootName');
            $route.current.$$route.breadcrumb[$route.current.$$route.breadcrumb.length - 2]
              .view = localStorageService.get('previousrootPath');

            $route.current.$$route.breadcrumb[$route.current.$$route.breadcrumb.length - 1].title = $routeParams.ean;
            $route.current.$$route.breadcrumb[$route.current.$$route.breadcrumb.length - 1]
              .view = localStorageService.get('previousrootPath');
        }

        /**
         * Opening transaction detail modal
         * LedgerTransactions from backend @param {} item 
         * @returns {} 
         */
        function openTransactionModal(item) {
            var renameFileModal = $uibModal.open({
                animation: true,
                templateUrl: '/app/views/balance/modals/transaction-detail.modal.view.html',
                backdrop: 'static',
                controller: 'transactionDetailsController as vm',
                scope: $scope,
                resolve: {
                    data: function () {
                        return item;
                    }
                }
            });
            return renameFileModal;
        }
    }
})(angular);