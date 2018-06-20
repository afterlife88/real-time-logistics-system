(function (angular) {
    'use strict';

    angular
        .module('app')
        .controller('organizationBalanceController', organizationBalanceController);

    organizationBalanceController.$inject = ['$location', '$routeParams', 'transactionHub', 'Alertify', '$scope', '$route', 'localStorageService'];

    function organizationBalanceController($location, $routeParams, transactionHub, Alertify, $scope, $route,
      localStorageService) {
        var vm = this;
        var currentPath = '#/organization-balance/' + $routeParams.kardex;

        vm.kardexCompany = $routeParams.kardex;
        vm.balances = [];
        vm.tranasctionsView = tranasctionsView;

        // Filters
        vm.searchEan = '';
        vm.searchDescription = '';
        vm.searchCargoBalance = '';
        setBreadcrump();


        // Set in local storage current and prev path for the navigation on transaction page
        localStorageService.add('previousrootPath', currentPath);
        localStorageService.add('previousrootName', vm.kardexCompany);

        transactionHub.initDone().then(function () {
            getBalancesForOrganization($routeParams.kardex);
        }, function (error) {
            Alertify.error(error);
        });


        function getBalancesForOrganization(kardex) {
            transactionHub.getBalancesByKardex(kardex).then(function (response) {
                console.log(response);
                vm.balances = response.Balances;
                $scope.$apply();
            }, function (error) {
                console.log(error);
                Alertify.error(error);
            });
        }

        /**
        * Redirection to transaction view, handler for click in table
        * @param {} item 
        * @returns {} 
        */
        function tranasctionsView(item) {
            console.log(item);
            $location.url('/transactions/' + vm.kardexCompany + '/' + item.Ean + '/' + item.OrganizationId + '/' + item.CargoId);
        }

        /**
         * Set breadcrump for the navigation
         * @returns {} 
         */
        function setBreadcrump() {
            $route.current.$$route.breadcrumb[$route.current.$$route.breadcrumb.length - 1].title = vm.kardexCompany;
            $route.current.$$route.breadcrumb[$route.current.$$route.breadcrumb.length - 1]
              .view = currentPath;
        }
    }
})(angular);