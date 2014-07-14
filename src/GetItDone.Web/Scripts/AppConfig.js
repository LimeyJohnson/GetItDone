/// <reference path="../scripts/typings/requirejs/require.d.ts" />
require.config({
    baseUrl: '//Scripts',
    paths: {
        jquery: "jquery-2.1.1.min",
        angular: "angular.min",
        angularbootstrap: "angular-ui/ui-bootstrap-tpls",
    },
    shim: {
        jquery: {
            exports: "$"
        },
        angular: {
            deps: ["jquery"],
            exports: 'angular'
        },
        angularbootstrap: {
            deps: ['angular']
        }
        
    }
});

require(['angular', 'app', "jquery", "angularbootstrap"], function (angular) {
    // code from window.onload
    angular.bootstrap(document, ['todomvc']);
});
