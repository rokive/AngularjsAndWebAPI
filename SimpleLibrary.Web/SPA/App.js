var App = angular.module('App', ['ngRoute', 'ui.bootstrap', 'chart.js', 'angular.chosen','bootstrapSubmenu']);
App.service('Api', ['$http', ApiService]);
App.directive('phoneNumbers', function () {
    return {
      require: 'ngModel',
      restrict: 'A',
      link: function (scope, element, attr, ctrl) {
        function inputValue(val) {
          if (val) {
            var digits = val.replace(/[^0-9+]/g, '');

            if (digits !== val) {
              ctrl.$setViewValue(digits);
              ctrl.$render();
            }
            return parseInt(digits,10);
          }
          return undefined;
        }            
        ctrl.$parsers.push(inputValue);
      }
    };
}).directive('isbn', function () {
    return {
        require: 'ngModel',
        restrict: 'A',
        link: function (scope, element, attr, ctrl) {
            function inputValue(val) {
                if (val) {
                    var digits = val.replace(/[^0-9]/g, '');

                    if (digits !== val) {
                        ctrl.$setViewValue(digits);
                        ctrl.$render();
                    }
                    return parseInt(digits, 10);
                }
                return undefined;
            }
            ctrl.$parsers.push(inputValue);
        }
    };
}).directive('myEnter', function () {
    return function (scope, element, attrs) {
        element.bind("keydown keypress", function (event) {
            if (event.which === 13 || event.which === 9) {
                scope.$apply(function () {
                    scope.$eval(attrs.myEnter);
                });

                event.preventDefault();
            }
        });
    };
});;

App.controller('MainController', MainController);
App.controller('HomeController', HomeController);

App.controller('BookController', BookController);
App.controller('BookIssueController', BookIssueController);
App.controller('StudentController', StudentController);

var configFunction = function ($routeProvider, $httpProvider) {
    $routeProvider.
        when('/home', {
            templateUrl: 'SPA/Views/main.html',
            controller: HomeController
        })
        .when('/book', {
            templateUrl: 'SPA/Views/book.html',
            controller: BookController
        })
        .when('/book-issue', {
            templateUrl: 'SPA/Views/bookissue.html',
            controller: BookIssueController
        })
        .when('/student', {
            templateUrl: 'SPA/Views/student.html',
            controller: StudentController
        })
       .otherwise({
        redirectTo: function () {
            return '/home';
       }
   });
}
configFunction.$inject = ['$routeProvider', '$httpProvider'];

App.config(configFunction);

App.run(function ($rootScope, $timeout) {
    $rootScope.errorMessage = "";
    $rootScope.isError = false;

    $rootScope.setError = function (errorMessage) {
        $rootScope.errorMessage = errorMessage;
        if (errorMessage != null && errorMessage != "") {
            $rootScope.isError = true;
        } else {
            $rootScope.isError = false;
        }
        $timeout(function () {
            $rootScope.setError(null);
        }, 3000);
    }
});


//function SetBusy(element, hide) {
//    if (hide == true) {
//        element.LoadingOverlay("hide");
//    } else {
//        element.LoadingOverlay("show", {
//            image: "",
//            fontawesome: "fa fa-spinner fa-spin"
//        });
//    }
//}
