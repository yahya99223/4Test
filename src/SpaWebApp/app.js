(function() {
    var app = angular.module("githubViewer", ["ngRoute"]);

    app.config(function ($routeProvider) {
        $routeProvider
            .when("/main",
            {
                templateUrl: "main/main.html",
                controller: "mainController"
            })
            .when("/user/:userName",
            {
                templateUrl: "user/index.html",
                controller: "userController"
            })
            .when("/user/:userName/Repository/:repositoryName",
            {
                templateUrl: "repository/index.html",
                controller: "repositoryController"
            })
            .otherwise({ redirectTo: "/main" });
    });

}());