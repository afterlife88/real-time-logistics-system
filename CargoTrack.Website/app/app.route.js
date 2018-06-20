angular.module('app').config(['$routeProvider', '$locationProvider', '$httpProvider', 'localStorageServiceProvider', '$qProvider',
  function ($routeProvider, $locationProvider, $httpProvider, localStorageServiceProvider, $qProvider) {
    $routeProvider
      .when('/transactions/:kardex/:ean/:organizationId/:cargoId',
      {
        templateUrl: '/app/views/balance/transactions.view.html',
        controller: 'transactionsController',
        controllerAs: 'vm',
        breadcrumb: [{ view: '#/search-organization', title: 'Home' }, { view: '#/search-organization', title: 'Balancer' }
        , { view: '', title: '' }, { view: '', title: '' }]
      })
      .when('/organization-balance/:kardex',
      {
        templateUrl: '/app/views/balance/organization-balance.view.html',
        controller: 'organizationBalanceController',
        controllerAs: 'vm',
        breadcrumb: [{ view: '#/search-organization', title: 'Home' }, { view: '#/search-organization', title: 'Balancer' }
        , { view: '', title: '' }]
      })
      .when('/search-organization',
      {
        templateUrl: '/app/views/balance/search-organization.view.html',
        controller: 'searchOrganizationController',
        controllerAs: 'vm',
        breadcrumb: [{ view: '/', title: 'Home' }, { view: '/', title: 'Balancer' }]
      })
      .otherwise({
        redirectTo: "/search-organization"
      });

    $locationProvider.hashPrefix('');

    //for localstorage
    localStorageServiceProvider
        .setPrefix('previous_path')
         .setStorageType('localStorage');

    $qProvider.errorOnUnhandledRejections(false);
  }
]);