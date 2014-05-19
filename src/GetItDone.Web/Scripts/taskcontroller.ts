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

        this.scope.taskMoved = function (taskID: number, boardID: number) {
            var originalBoard: B.Board, originalTask: T.Task, newBoard: B.Board;

            for (var x: number = 0; x < $scope.boards.length; x++) {
                if ($scope.boards[x].BoardID == boardID) {
                    newBoard = $scope.boards[x];
                }
                for (var y: number = 0; y < $scope.boards[x].Tasks.length; y++) {
                    if ($scope.boards[x].Tasks[y].TaskID == taskID) {
                        originalTask = $scope.boards[x].Tasks[y];
                        originalBoard = $scope.boards[x];
                    }
                }
            }
            $http.post("/api/Task/MoveTask/" + boardID, originalTask).success(function (updatedTask) {
                setTimeout(function () {
                    $scope.$apply(function () {
                        newBoard.Tasks.push(updatedTask);
                        originalBoard.Tasks.splice(originalBoard.Tasks.indexOf(originalTask), 1);
                    });
                }, 0);
            });
        };
        this.scope.TaskEquals = function (a: T.Task, b: T.Task): boolean {
            if (a.Name != b.Name) return false;
            if (a.Details != b.Details) return false;
            return true;
        }
        this.scope.TaskListEquals = function (a: T.Task[], b: T.Task[]): boolean {

            if (!a || !b || a.length != a.length) return false;
            for (var x = 0; x < a.length; x++) {
                if (!$scope.TaskEquals(a[x], b[x])) return false;
            }
            return true;
        };
        this.scope.addTask = function () {
            var board:B.Board = this.board;
            $http.post("/api/Task/NewTask/" + board.BoardID, board.newTask).success(function (postedTask: T.Task) {
                board.Tasks.push(postedTask);
                delete board.newTask;
                board.collapseAll();
            }).error(function (callback) {
                    alert("error");
                });
        };
        this.scope.deleteTask = function (task: T.Task, board: B.Board) {
            $http.delete("/api/Task/DeleteTask/" + task.TaskID).success(function (callback) {
                board.Tasks.splice(board.Tasks.indexOf(task), 1);
            });
        };
        this.scope.updateBoard = function (board: B.Board) {
            $http.post("/api/Board/Update", board).success(function (data: B.Board) {
                $scope.refreshBoard(board);

            });
        };
        this.scope.refreshBoard = function (board: B.Board) {
            $http.get("/api/Board/GetTaskList/" + board.BoardID).success(function (tl, status2) {
                board.Tasks = tl;
            });
        }
        this.scope.refeshBoardTasks = function (scope: B.BoardScope) {
            for (var x = 0; x < scope.boards.length; x++) {
                this.refreshBoard(scope.boards[x]);
            }
        }
        this.scope.refreshBoards = function (scope: B.BoardScope) {
            $http.get("/api/Task/Boards/").success((data: B.Board[], status) => {
                scope.boards = data;

                $scope.refeshBoardTasks(scope);
            });
        }

        this.scope.setTaskEditable = function (task: T.Task) {
            task.EditMode = true;
        }
        this.scope.editTask = function (task: T.Task) {
            $http.post("/api/Task/Update/", task).success(function (data) {
                task = data;
            });
            task.EditMode = false;
        }

        this.scope.deleteBoard = function (board: B.Board) {
            $http.delete("/api/Board/DeleteBoard/" + board.BoardID).success(function (callback) {
                $scope.boards.splice($scope.boards.indexOf(board), 1);
            });
        }
        this.scope.refreshBoards(this.scope);


        this.scope.createBoard = function () {
            $http.post("/api/Board/NewBoard/", $scope.newBoard).success(function (postedBoard: B.Board) {
                $scope.boards.push(postedBoard);
                $scope.newBoard = null;
            }).error(function (callback) {
                    alert("error");
                });
        };

        setInterval(function () {
            $scope.refeshBoardTasks($scope);
        }, 60000);
    }


}
