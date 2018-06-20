(function (angular) {
    'use strict';

    angular
        .module('app')
        .controller('searchOrganizationController', searchOrganizationController);

    searchOrganizationController.$inject = ['$location', 'Alertify', 'organizationHub', '$scope'];

    function searchOrganizationController($location, Alertify, organizationHub, $scope) {
        var vm = this;
        vm.query = null;
        vm.organizationList = [];
        vm.search = search;
        vm.balancerView = balancerView;
        constructor();

        /**
         * Initial with argument 0 to search handler to get all organization by default
         * @returns {} 
         */
        function constructor() {
            organizationHub.initDone().then(function () {
                search(0);
            }, function (error) {
                Alertify.error(error);
            });
        }

        /**
         * Search handler
         * @param {} query 
         * @returns {} 
         */
        function search(query) {
            // Example query: 
            // 10203041
            vm.organizationList = [];
            organizationHub.getOrganizationsByKardex(query).then(function (response) {
                console.log(response);
                vm.organizationList = response.Organizations;
                $scope.$apply();
            }, function (error) {
                console.log(error);
                Alertify.error(error);
            });
        }

        /**
         * Redirection handler to org-balance
         * @param {} item 
         * @returns {} 
         */
        function balancerView(item) {
            $location.url('/organization-balance/' + item.Kardex);
        }
    }
})(angular);