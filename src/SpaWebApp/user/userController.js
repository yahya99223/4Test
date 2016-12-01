(function() {
    var userController = function($scope, githubService, $routeParams, $log) {
        $scope.userName = $routeParams.userName;
        $scope.repositorySortOrder = "-stargazers_count";

        var onError = function(reason) {
            $scope.error = "Error in connection";
        };


        var onGetRepositoriesComplete = function(repositoriesData) {
            $scope.repositories = repositoriesData;
        };

        var onGetUserComplete = function(userData) {
            $scope.user = userData;
            githubService.getRepositories(userData)
                .then(onGetRepositoriesComplete, onError);
        };

        githubService.getUser($scope.userName)
                .then(onGetUserComplete, onError);
    };

    var app = angular.module("githubViewer");
    app.controller("userController", ["$scope", "githubService", "$routeParams", "$log", userController]);
}());