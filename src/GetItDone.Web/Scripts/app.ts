/// <reference path="directives.ts" />
/// <reference path="todocontroller.ts" />
import angular = require("angular");
import directives = require("directives");
import TCntrl = require("todocontroller");
require("uidate");
'use strict';

export var app = angular.module('todomvc', ['ui.date']).controller("TodoController", TCntrl.TodoController).directive("accordion", directives.accordion).directive("setBoardId", directives.setBoardId);
