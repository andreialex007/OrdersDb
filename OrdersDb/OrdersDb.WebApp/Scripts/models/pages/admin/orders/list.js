define([
        "knockout",
        "knockout.mapping",
        "sammy",
        "Scripts/models/pages/base/identityList",
        "Scripts/models/controls/table/table",
        "Scripts/models/controls/table/column",
        "Scripts/models/controls/table/row",
        "Scripts/models/controls/table/cell",
        "Scripts/models/controls/fields/dropdown",
        "Scripts/models/controls/fields/textField",
        "Scripts/models/controls/fields/intField",
        "Scripts/models/controls/fields/intListField",
        "Scripts/models/controls/fields/rangeSpinnerControl"
], function (ko, mapping, sammy, listPageBase, table, column, row, cell, dropdown, textField, intField, intListField, rangeSpinnerControl) {

    function listPage(parent) {
        var self = new listPageBase(parent);
        self.title("Список городов");

        self.table.columns(self.table.columns().concat([
            new column("Code.Value", "Код")
//            new column("Population", "Население", "150px")
        ]));

        self.Code = new textField("Код закза", "", "Введите как минимум 3 символа");

        var toRowBase = self.toRow;
        self.toRow = function (el) {
            var rowBase = toRowBase(el);
            rowBase.addCells([
                new cell(el.Code)
            ]);
            return rowBase;
        };

        var getSearchParamsBase = self.getSearchParams;
        self.getSearchParams = function () {
            var searchParamsBase = getSearchParamsBase();
            return $.extend(searchParamsBase, {
                Code: self.Code.value().length < 3 ? "" : self.Code.value(),
            });
        };

        self.init();
        return self;
    }

    return listPage;
});