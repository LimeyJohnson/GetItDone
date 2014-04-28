/// <reference path="directives.ts" />
/// <reference path="taskcontroller.ts" />
import angular = require("angular");
import directives = require("directives");
import TCntrl = require("taskcontroller");

'use strict';

export var app = angular.module('todomvc', []).controller("TaskController", TCntrl.TaskController).directive("accordion", directives.accordion).directive("setBoardId", directives.setBoardId);
