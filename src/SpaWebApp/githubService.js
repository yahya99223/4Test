(function () {
    var githubService = function($http) {


        var getUser = function(userName) {
            return $http.get("https://api.github.com/users/" + userName)
                .then(function(response) {
                    return response.data;
                });
        };

        var getRepositories = function(userData) {
            return $http.get(userData.repos_url)
                .then(function(response) {
                    return response.data;
                });
        };

        return {
            getUser: getUser,
            getRepositories:getRepositories
        };
    };

    var module = angular.module("githubViewer");
    module.factory("githubService", githubService);
}());