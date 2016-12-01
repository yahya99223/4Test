(function() {
    var mainController = function($scope, $interval, $location, $log) {
        $scope.userName = "angular";
        $scope.countdown = 9;

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


        $scope.search = function(userName) {
            $log.info("Searching fo " + userName);
            if (countdownInterval) {
                $interval.cancel(countdownInterval);
                $scope.countdown = null;
            }
            $location.path("/user/" + userName);
        };

        startCountdown();
    };

    var app = angular.module("githubViewer");
    app.controller("mainController", ["$scope", "$interval", "$location", "$log", mainController]);
}());