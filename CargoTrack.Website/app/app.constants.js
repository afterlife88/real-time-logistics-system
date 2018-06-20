(function (angular) {
    'use strict';
    /**
     * Constans of the application
     */
    angular.module('app')
      .constant('appConfig', {
          signalrUrl: 'http://localhost:33406/signalr'
      });

})(angular);