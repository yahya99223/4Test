
(function() {

        var mainController = function($scope, githubService, $interval, $location, $anchorScroll, $log) {
            $scope.welcomeMessage = "Github User Viewer";
            $scope.userName = "angular";
            $scope.repositorySortOrder = "-stargazers_count";
            $scope.countdown = 5;

            var onError = function(reason) {
                $scope.error = "Error in connection";
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
            };

            var onGetRepositoriesComplete = function(repositoriesData) {
                $scope.repositories = repositoriesData;
                $location.hash("userDetails");
                $anchorScroll();
            };

            var onGetUserComplete = function (userData) {
                $scope.user = userData;
                githubService.getRepositories(userData)
                    .then(onGetRepositoriesComplete, onError);
            };

            $scope.search = function(userName) {
                $log.info("Searching fo " + userName);
                githubService.getUser(userName)
                    .then(onGetUserComplete, onError);

                if (countdownInterval) {
                    $interval.cancel(countdownInterval);
                    $scope.countdown = null;
                }
            };

            startCountdown();
        };

        var app = angular.module("githubViewer", []);
        app
            .controller("MainController",
                ["$scope", "githubService", "$interval", "$location", "$anchorScroll", "$log", mainController]);
    }
    ());;