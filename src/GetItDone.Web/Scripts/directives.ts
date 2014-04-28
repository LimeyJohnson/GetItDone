/// <reference path="./typings/jqueryui/jqueryui.d.ts" />
/// <reference path="./app.ts" />
/// <reference path="./typings/jquery/jquery.d.ts" />
/// <reference path="board.ts" />

import jQuery = require("jqueryUI");
import board = require("board");
export function accordion(): ng.IDirective {
         return {
             link: function (scope: board.BoardScope, element: JQuery, attr) {
                 function update(event, ui) {
                     if (ui.sender) { //Only trigger for the last item
                         var taskID = ui.item.attr("task-id");
                         var boardID = ui.item.closest("div[board-id]").attr("board-id");
                         scope.taskMoved(taskID, boardID);
                     }
                 }
                 function createAccordion() {

                     if (element.parent().hasClass('ui-accordion')) {
                         element.parent().accordion('refresh');
                     }
                     else {
                         element.closest("div[class='tasklist']").accordion({ collapsible: true, heightStyle: "content", header: "div > h3", active: false });
                         jQuery(".tasklist").sortable({ connectWith: ".tasklist", update: update, forcePlaceholderSize: true, handle: 'h3' });
                     }
                     
                 }
                 if (scope.task) {
                     element.attr("task-id", scope.task.TaskID);
                     element.find("h3").css("background", scope.$parent.board.ColorCode);
                     if (scope.$last) {
                         createAccordion();

                        
                     }
                 }
                 else {
                     //this was called on a ne task item. Just run the create accordion command if it has
                     createAccordion();
                 }

        }
    }
    }

export function setBoardId(): ng.IDirective {
         return {
        link: function (scope, element: JQuery, attr) {
            element.attr('board-id', scope.board.BoardID);
        }
    }
    }




