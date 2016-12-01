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

        var getRepository = function(userName, repositoryName) {
            return $http.get("https://api.github.com/repos/" + userName + "/" + repositoryName)
                .then(function(response) {
                    return response.data;
                });
        };

        var getContributors = function(userName, repositoryName) {
            return $http.get("https://api.github.com/repos/" + userName + "/" + repositoryName + "/contributors")
                .then(function(response) {
                    return response.data;
                });
        };

        return {
            getUser: getUser,
            getRepositories: getRepositories,
            getRepository: getRepository,
            getContributors: getContributors
        };
    };

    var module = angular.module("githubViewer");
    module.factory("githubService", githubService);
}());