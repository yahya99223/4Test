(function() {

    var repositoryController = function($scope, githubService, $routeParams) {
        $scope.userName = $routeParams.userName;
        $scope.repositoryName = $routeParams.repositoryName;

        var onError = function(reason) {
            $scope.error = "Error in connection";
        };
        var onGetContributorsComplete = function(contributorsData) {
            $scope.contributors = contributorsData;
        };
        var onGetRepositoryComplete = function(repositoryData) {
            $scope.repository = repositoryData;
            githubService.getContributors($scope.userName, $scope.repositoryName)
                .then(onGetContributorsComplete, onError);
        };

        githubService.getRepository($scope.userName, $scope.repositoryName)
            .then(onGetRepositoryComplete, onError);
    };

    var module = angular.module("githubViewer");
    module.controller("repositoryController", repositoryController);
}());