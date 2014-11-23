define([
        "knockout"
], function (ko, tableBase) {

    function cell(value, id, column) {
        var self = {};
        self.column = column || null;
        self.data = null;
        self.value = ko.observable(value);
        self.id = ko.observable(id || value);
        return self;
    }
    return cell;
});