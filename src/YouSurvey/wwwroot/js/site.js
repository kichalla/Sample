/// <reference path="models.js" />

var myApp = angular.module('myApp', []);
myApp.directive('fileModel', ['$parse', function ($parse) {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            var model = $parse(attrs.fileModel);
            var modelSetter = model.assign;

            element.bind('change', function () {
                scope.$apply(function () {
                    modelSetter(scope, element[0].files[0]);
                });
            });
        }
    };
}]);

myApp.service('surveyCreatorService', ['$http', function ($http) {
    this.getListOfSurveys = function () {
        return $http.get('/surveycreator/list', {
            headers: {
                'Content-Type': 'application/json'
            }
        });
    };
}]);

myApp.controller('surveyCreatorCtrl', ['$scope', 'surveyCreatorService', function ($scope, surveyCreatorService) {
    $scope.getListOfSurveys = function () {
        surveyCreatorService.getListOfSurveys().then(function (response) { // success
            if (response.status === 200) {
                $scope.listOfSurveys = response.data;
            }
            else {
                alert('Not a 200:' + response.status);
            }
        },
        function (response) { // failure
            alert('Error:' + response.status);
        });
    };
}]);