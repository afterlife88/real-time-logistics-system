(function (angular) {
  'use strict';

  angular
    .module('app')
    .factory('organizationHub', organizationHub);

  organizationHub.$inject = ['Hub', 'appConfig'];

  function organizationHub(Hub, appConfig) {

    //declaring the hub connection 
    var hub = new Hub('OrganizationHub',
    {
      rootPath: appConfig.signalrUrl,
      //server side methods
      methods: ['getOrganizationsByKardex'],
      //handle connection error
      errorHandler: function (error) {
        console.error(error);
      }
    });

    var service = {
      initDone: initDone,
      getOrganizationsByKardex: getOrganizationsByKardex
    };

    return service;

    function getOrganizationsByKardex(kerdex) {
      var request = {
        Kardex: kerdex
      };
      var res = hub.getOrganizationsByKardex(request);
      return res;
    }

    function initDone() {
      return hub.promise.done();
    }
  }
})(angular);