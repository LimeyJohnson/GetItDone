/// <reference path="./task.ts" />
/// <reference path="./app.ts" />
/// <reference path="./typings/angularjs/angular.d.ts" />
/// <amd-dependency path="app" />
import T = require("./task");
import B = require("./board");
'use strict';
require("app");
export class TaskController {
    private httpService: ng.IHttpService;
    private scope: B.BoardScope;
    private $parent: any;
    public static $inject = ['$scope', '$location', '$http'];
    constructor($scope: B.BoardScope, $location: ng.ILocationService, $http: ng.IHttpService) {
        this.httpService = $http;
        this.scope = $scope;
        this.refreshProducts(this.scope);
        this.scope.taskMoved = function (taskID: number, boardID: number) {
            var originalBoard: B.Board, originalTask: T.Task, newBoard: B.Board;

            for (var x: number = 0; x < $scope.boards.length; x++) {
                if ($scope.boards[x].BoardID == boardID) {
                    newBoard = $scope.boards[x];
                }
                for (var y: number = 0; y < $scope.boards[x].Tasks.length; y++) {
                    if ($scope.boards[x].Tasks[y].TaskID == taskID) {
                        originalTask= $scope.boards[x].Tasks[y];
                        originalBoard = $scope.boards[x];
                    }
                } 
            }

            $http.post("/api/Task/MoveTask/" + boardID, originalTask).success(function (updatedTask) {
                $scope.$apply(function (){
                    originalBoard.Tasks.splice(originalBoard.Tasks.indexOf(originalTask), 1);
                    newBoard.Tasks.push(updatedTask);
                });
                
            })

        };
        this.scope.addTask = function () {
            var board = this.board;
            $http.post("/api/Task/NewTask/"+board.BoardID, board.newTask).success(function (postedTask:T.Task) {
                board.Tasks.push(postedTask);
                board.newTask = {};
            }).error(function (callback) {
                    alert("error");
                });
        };
        this.scope.deleteTask = function (task: T.Task, board:B.Board) {
            $http.delete("/api/Task/DeleteTask/" + task.TaskID).success(function (callback) {
                board.Tasks.splice(board.Tasks.indexOf(task), 1);
            });
        }

    }
    refreshBoard(board: B.Board) {
        this.httpService.get("/api/Task/GetTaskList/" + board.BoardID).success(function (tl, status2) {
            board.Tasks = tl;
        });
    }

    refreshProducts(scope: B.BoardScope) {

        this.httpService.get("/api/Task/Boards/").success((data: B.Board[], status) => {
            scope.boards = data;
            for (var x = 0; x < scope.boards.length; x++) {
                this.refreshBoard(scope.boards[x]);
            }

        });
    }
    
}
