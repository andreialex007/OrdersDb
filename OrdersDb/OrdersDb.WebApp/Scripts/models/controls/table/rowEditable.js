define([
        "knockout",
        "./row"
], function (ko, rowBase) {

    function rowEditable(table) {
        var self = new rowBase(table);
        self.delete = function() {
            console.log("delete");
        };
       
        self.edit = function() {
            console.log("edit");
        };
        return self;
    }

    return rowEditable;

});