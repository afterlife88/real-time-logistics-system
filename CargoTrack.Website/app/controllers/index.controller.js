(function (angular) {
  'use strict';

  angular
      .module('app')
      .controller('indexController', indexController);

  indexController.$inject = ['$location', '$scope'];

  function indexController($location, $scope) {
    var vm = this;

  }
})(angular);
