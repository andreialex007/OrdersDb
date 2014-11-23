define([
        "knockout",
        "./tableBase",
        "./column",
        "./rowEditable",
        "./cell"
], function (ko, tableBase, column, row, cell) {

    function table(columns) {
        var self = new tableBase();
        self.isLoading = ko.observable(false);
        self.isScrollLoading = ko.observable(false);
        self.columns = ko.observableArray(columns || []);

        //Loading parameters
        self.take = 100;
        self.takeAppend = 50;


        self.click = function (item) {
            $(self.columns()).each(function (index, element) {
                element.isSort(false);
            });
            item.isSort(true);
            self.isDesc(!self.isDesc());
            self.load();
        };
        self.isDesc = ko.observable(false);
        self.sortColumn = ko.computed({
            read: function () {
                for (var i in self.columns()) {
                    var col = self.columns()[i];
                    if (col.isSort())
                        return col;
                }
                return null;
            },
            write: function (value) {
                var found = $.grep(self.columns(), function (el) {
                    return el.id() == value.id();
                });
                if (found.length == 1)
                    found[0].isSort(true);
            }
        });
        self.rows = ko.observableArray([]);

        self.allChecked = ko.computed({
            read: function () {
                var uncheckedItems = $.grep(self.rows(), function (el) {
                    return el.isChecked() == false;
                });
                if (uncheckedItems.length == 0)
                    return true;
                return false;
            },
            write: function (value) {
                $(self.rows()).each(function (index, element) {
                    element.isChecked(value);
                });
            }
        });
        self.clear = function () {
            self.rows.removeAll();
        };

        self.load = function () {
            self.clear();
            self.isLoading(true);
            self.onLoad();
        };

        self.loadAppend = function () {
            self.isScrollLoading(true);
            self.onScrollLoad();
        };

        self.scroll = function (percentage) {
            if (percentage > 80 && !self.isScrollLoading()) {
                self.loadAppend();
            }
        };

        self.onLoad = function () {
        };

        self.onScrollLoad = function () {
        };

        self.onLoadCompleted = function () {
            self.isLoading(false);
            window.onTableDataLoaded();
        };

        self.onScrollLoadCompleted = function () {
            self.isScrollLoading(false);
            window.onTableDataLoaded();
        };

        self.setColums = function (cols) {
            self.columns(cols);
            $(self.columns()).first()[0].isSort(true);
        };

        self.edit = function (rowItem) {
            self.onEdit(rowItem);
        };

        self.onEdit = function () {

        };

        self.delete = function (rowItem) {
            self.onDelete(rowItem);
        };

        self.onDelete = function () {
            console.log("onDelete");
        };

        return self;
    }

    return table;

});