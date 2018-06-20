(function (angular) {
  'use strict';

  // Angular module for the application
  angular.module('app', [
    'ngRoute',
    'Alertify',
    'angularSpinner',
    'ui.bootstrap',
    'SignalR',
    'datatables',
    'LocalStorageModule'
  ]);

  angular.module('app').run(['$rootScope',
    function ($rootScope) {
      // Route has changed successfull
      $rootScope.$on('$routeChangeSuccess', function (event, current) {
        // Set current title
        $rootScope.title = current.$$route ? current.$$route.title : '';
        $rootScope.breadcrumb = current.$$route ? current.$$route.breadcrumb : '';
      });
    }
  ]);
})(angular);