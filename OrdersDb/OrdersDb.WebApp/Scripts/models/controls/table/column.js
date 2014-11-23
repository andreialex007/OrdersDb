define([
        "knockout",
        "./cell"
], function (ko, cell) {

    function column(id, name, width) {
        var self = this;
        self.id = ko.observable(id);
        self.name = ko.observable(name);
        self.isSort = ko.observable(false);
        self.width = ko.observable(width || "auto");
        self.valueToCell = function (value) {
            return new cell(self, value);
        };
        return self;
    }

    return column;

});