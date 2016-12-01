/// <reference path="bower_components/angular/angular.js" />
(function () {

    var mainController = function ($scope, $http, $interval) {
        $scope.welcomeMessage = "Github User Viewer";
        $scope.userName = "angular";
        $scope.repositorySortOrder = "-stargazers_count";
        $scope.countdown = 5;

        var onError = function(reason) {
            $scope.error = "Error in connection";
        };

        var onGetRepositoriesComplete = function(response) {
            $scope.repositories = response.data;
        };

        var onGetUserComplete = function(response) {
            $scope.user = response.data;
            $http.get($scope.user.repos_url)
                .then(onGetRepositoriesComplete, onError);
        };

        var decrementCountdown = function() {
            $scope.countdown -= 1;
            if ($scope.countdown < 1) {
                $scope.search($scope.userName);
            }
        };

        var countdownInterval = null;
        var startCountdown = function() {
            countdownInterval = $interval(decrementCountdown, 1000, $scope.countdown);
        }

        $scope.search = function (userName) {
            $http.get("https://api.github.com/users/" + userName)
                .then(onGetUserComplete, onError);

            if (countdownInterval) {
                $interval.cancel(countdownInterval);
                $scope.countdown = null;
            }
        };

        startCountdown();
    };

    var app = angular.module("githubViewer", []);
    app.controller("MainController", ["$scope", "$http", "$interval", mainController]);
}());