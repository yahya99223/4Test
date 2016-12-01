(function() {

    var app = angular.module("githubViewer", []);

    app.controller("MainController",
    [
        "$scope", "$http", function($scope, $http) {
            $scope.welcomeMessage = "Github User Viewer";
            $scope.userName = "angular";
            $scope.repositorySortOrder = "-stargazers_count";
            
            var onError = function (reason) {
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

            $scope.search = function(userName) {
                $http.get("https://api.github.com/users/" + userName)
                    .then(onGetUserComplete, onError);
            };
        }
    ]);
}());