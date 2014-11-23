define([
        "knockout",
        "knockout.mapping",
        "sammy",
        "Scripts/models/pages/base/list",
        "Scripts/models/controls/table/table",
        "Scripts/models/controls/table/column",
        "Scripts/models/controls/table/row",
        "Scripts/models/controls/table/cell",
        "Scripts/models/controls/fields/dropdown",
        "Scripts/models/controls/fields/textField",
        "Scripts/models/controls/fields/intField",
        "Scripts/models/controls/fields/intListField",
        "Scripts/models/controls/fields/rangeSpinnerControl"
], function (ko, mapping, sammy, listPageBase, table, column, row, cell, dropdown, textField, intField, intListField) {

    function listPage(parent) {
        var self = new listPageBase(parent);
        self.title("");

        self.table.columns([
            new column("Id", "#", "50px")
        ]);
        $(self.table.columns()).first()[0].isSort(true);

        self.id = new intListField("Идентификаторы");

        self.toRow = function (el) {
            return new row(
                self.table,
                el.Id,
                [
                    new cell(el.Id)
                ]
            );
        };

        self.getSearchParams = function () {
            return {
                Ids: utils.commaListToArray(self.id.value())
            };
        };

        self.table.onEdit = function(rowItem) {
            document.location.hash = "#/" + self.controllerName + "/" + rowItem.id();
        };

        return self;
    }

    return listPage;
});