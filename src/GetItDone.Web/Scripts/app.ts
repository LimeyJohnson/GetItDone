/// <reference path="directives.ts" />
/// <reference path="todocontroller.ts" />
import angular = require("angular");
import directives = require("directives");
import TCntrl = require("todocontroller");
'use strict';

export var app = angular.module('todomvc',['ui.bootstrap']).controller("TodoController", TCntrl.TodoController);
