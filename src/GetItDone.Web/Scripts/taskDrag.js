define(["require", "exports", "jqueryUI"], function(require, exports, jQuery) {
    function taskDrag() {
        return {
            link: function (scope, element, attr) {
                function update(event, ui) {
                    scope.taskMoved(event, ui);
                }
                if (scope.$last) {
                    jQuery(".tasklist").sortable({ connectWith: ".tasklist", update: update, forcePlaceholderSize: true, handle: 'h3' });
                }
            }
        };
    }
    exports.taskDrag = taskDrag;
});
//# sourceMappingURL=taskDrag.js.map
