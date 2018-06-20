(function (angular) {
    'use strict';
    /**
     * Return string of the transaction from integer value
     */
    angular.module('app').filter('transactionType', function () {
        return function (input) {
            switch (input) {
                case 1:
                    return '01 Product delivery to store';
                case 2:
                    return '02 Receive Cargo From Supplier';
                case 3:
                    return '03 Status Correction';
                case 4:
                    return '04 Buy Cargo';
                case 5:
                    return '05 Sell Cargo';
                case 6:
                    return '06 Manual Transfer Of Cargo';
                case 7:
                    return '07 Trash cargo';
                default:
                    return input;
            }
        }
    });

})(angular);