/// <reference path="../scripts/typings/requirejs/require.d.ts" />
require.config({
    baseUrl: '/Scripts',
    paths: {
        jquery: "jquery-2.1.0.min",
        jqueryUI: "jquery-ui-1.10.4.min",
        angular: "angular.min"
    },
    shim: {
        jqueryUI: {
            deps: ["jquery"],
            exports: "$"
        },
        angular: {
            deps: ["jquery", "jqueryUI"],
            exports: 'angular'
        }
    }
});

require(['angular', 'app', "jqueryUI"], function (angular) {
    // code from window.onload
    angular.bootstrap(document, ['todomvc']);
});
