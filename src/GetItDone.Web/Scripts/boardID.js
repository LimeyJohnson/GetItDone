define(["require", "exports"], function(require, exports) {
    function accordion() {
        return {
            link: function (scope, element, attr) {
                element.attr('id', scope.board.BoardID);
            }
        };
    }
    exports.accordion = accordion;
});
//# sourceMappingURL=boardID.js.map
