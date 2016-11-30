(function () {

    var app = angular.module('githubViewer', []);

    app.controller('MainController', ["$scope", "$http", function ($scope, $http) {

        var onGetUserComplete = function (response) {
            $scope.user = response.data;
        }


        var onGetUserError = function (reason) {
            $scope.error = "Error in connection";
        }

        $http.get("https://api.github.com/users/docker")
            .then(onGetUserComplete, onGetUserError);

    }]);
}());