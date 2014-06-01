/// <reference path="typings/angularjs/angular.d.ts" />
/// <reference path="task.ts" />
import T = require("task");
export class Board {
    BoardID: number;
    Name: string;
    ColorCode: string;
    Tasks: T.Task[];
    Filter: number;
    Menu: boolean;
    selectLast: () => any;
} 

export interface BoardScope extends ng.IScope{
    boards: Board[];
    taskMoved: (taskID: number, boardID: number) => any;
    deleteTask: (task: T.Task, board: Board) => any;
    updateBoard: (board: Board) => any;
    refreshBoard: (board: Board) => any;
    refreshBoards: (scope: BoardScope) => any;
    refeshBoardTasks: (scope: BoardScope) => any;
    setTaskEditable: (task: T.Task) => any;
    editTask: (task: T.Task) => any;
    deleteBoard: (board: Board) => any;
    createBoard: () => any;
    newBoard: Board;
    taskEdit: number;
    name: string;
    details: string;
    addNewTask: any;
    task: T.Task; // Current task when scope is used in child scope context.
    $parent: BoardScope;
    board: Board;

    TaskEquals: (a: T.Task, b: T.Task) => boolean;
    TaskListEquals: (a: T.Task[], b: T.Task[]) => boolean;
}