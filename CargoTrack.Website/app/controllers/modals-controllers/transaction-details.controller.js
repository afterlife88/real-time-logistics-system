(function (angular) {
    'use strict';

    angular
        .module('app')
        .controller('transactionDetailsController', transactionDetailsController);

    transactionDetailsController.$inject = ['data', '$uibModalInstance'];

    /**
     * Transaction detail modal controller 
     * Passed data from transaction controller @param {} data 
     * @param {} $uibModalInstance 
     * @returns {} 
     */
    function transactionDetailsController(data, $uibModalInstance) {
        var vm = this;
        vm.cancel = cancel;
        vm.data = data;

        function cancel() {
            $uibModalInstance.close();
        }
    }
})(angular);
