/// <reference path="../scripts/typings/requirejs/require.d.ts" />
require.config({
    baseUrl: 'Scripts',
    paths: {
        jquery: "jquery-2.1.1.min",
        angular: "angular",
    },
    shim: {
        jquery: {
            exports: "$"
        },
        angular: {
            deps: ["jquery"],
            exports: 'angular'
        },
    }
});

require(['angular', 'app', "jquery"], function (angular) {
    // code from window.onload
    angular.bootstrap(document, ['todomvc']);
});
