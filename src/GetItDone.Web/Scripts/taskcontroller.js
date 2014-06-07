define(["require", "exports", "app"], function(require, exports) {
    'use strict';
    require("app");
    var TaskController = (function () {
        function TaskController($scope, $location, $http, $timeout) {
            this.httpService = $http;
            this.scope = $scope;

            this.scope.taskMoved = function (taskID, boardID) {
                var originalBoard, originalTask, newBoard;

                for (var x = 0; x < $scope.boards.length; x++) {
                    if ($scope.boards[x].BoardID == boardID) {
                        newBoard = $scope.boards[x];
                    }
                    for (var y = 0; y < $scope.boards[x].Tasks.length; y++) {
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
            this.scope.TaskEquals = function (a, b) {
                if (a.Name != b.Name)
                    return false;
                if (a.Details != b.Details)
                    return false;
                return true;
            };
            this.scope.TaskListEquals = function (a, b) {
                if (!a || !b || a.length != a.length)
                    return false;
                for (var x = 0; x < a.length; x++) {
                    if (!$scope.TaskEquals(a[x], b[x]))
                        return false;
                }
                return true;
            };
            this.scope.addNewTask = function (board) {
                var newTask = { Name: "New Task" };

                $http.post("/api/Task/NewTask/" + board.BoardID, newTask).success(function (postedTask) {
                    postedTask.EditMode = true;
                    board.Tasks.push(postedTask);
                    $timeout(function () {
                        board.selectLast();
                    }, 0, false);
                }).error(function (callback) {
                    alert("error");
                });
            };
            this.scope.deleteTask = function (task, board) {
                $http.delete("/api/Task/DeleteTask/" + task.TaskID).success(function (callback) {
                    board.Tasks.splice(board.Tasks.indexOf(task), 1);
                });
            };
            this.scope.updateBoard = function (board) {
                $http.post("/api/Board/Update", board).success(function (data) {
                    $scope.refreshBoard(board);
                });
            };
            this.scope.refreshBoard = function (board) {
                $http.get("/api/Board/GetTaskList/" + board.BoardID).success(function (tl, status2) {
                    board.Tasks = tl;
                });
            };
            this.scope.refeshBoardTasks = function (scope) {
                for (var x = 0; x < scope.boards.length; x++) {
                    this.refreshBoard(scope.boards[x]);
                }
            };
            this.scope.refreshBoards = function (scope) {
                $http.get("/api/Task/Boards/").success(function (data, status) {
                    scope.boards = data;

                    $scope.refeshBoardTasks(scope);
                });
            };

            this.scope.setTaskEditable = function (task) {
                task.EditMode = true;
            };
            this.scope.editTask = function (task) {
                $http.post("/api/Task/Update/", task).success(function (data) {
                    task = data;
                });
                task.EditMode = false;
            };

            this.scope.deleteBoard = function (board) {
                $http.delete("/api/Board/DeleteBoard/" + board.BoardID).success(function (callback) {
                    $scope.boards.splice($scope.boards.indexOf(board), 1);
                });
            };
            this.scope.refreshBoards(this.scope);

            this.scope.createBoard = function () {
                $http.post("/api/Board/NewBoard/", $scope.newBoard).success(function (postedBoard) {
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
        TaskController.$inject = ['$scope', '$location', '$http', '$timeout'];
        return TaskController;
    })();
    exports.TaskController = TaskController;
});
//# sourceMappingURL=taskcontroller.js.map
