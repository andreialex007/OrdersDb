define([
        "knockout",
        "./tableBase"
], function (ko, tableBase) {

    function row(table, id, cells) {
        var self = this;
        self.table = table;
        self.id = ko.observable(id);
        self.cells = ko.observableArray([]);

        self.isChecked = ko.observable(false);

        self.addColumnsToCells = function (targetCells) {
            $(targetCells).each(function (index, element) {
                var col = self.table.columns()[index + self.cells().length];
                if (!col) {
                    debugger;
                }
                element.column = col;
            });
        };
        self.addColumnsToCells(cells);
        self.cells(cells);

        self.addCells = function (newCells) {
            self.addColumnsToCells(newCells);
            self.cells(self.cells().concat(newCells));
        };

        return self;
    }

    return row;

});