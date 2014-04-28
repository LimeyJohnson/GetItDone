/// <reference path="typings/angularjs/angular.d.ts" />
/// <reference path="task.ts" />
import T = require("task");
export class Board {
    BoardID: number;
    Name: string;
    ColorCode: string;
    Tasks: T.Task[];
    newTask: T.Task;
} 

export interface BoardScope extends ng.IScope{
    boards: Board[];
    taskMoved: (taskID: number, boardID: number) => any;
    deleteTask: (task: T.Task, board:Board) => any;
    name: string;
    details: string;
    addTask: any;
    task: T.Task; // Current task when scope is used in child scope context.
    $parent: BoardScope;
    board: Board;
}