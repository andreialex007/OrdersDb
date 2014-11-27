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
//            new column("Region.Name", "Регион", "200px"),
//            new column("Population", "Население", "150px")
        ]));

        self.regionName = new textField("Название региона", "", "Введите как минимум 3 символа");
//        self.population = new rangeSpinnerControl("Население", 0, 15000000);

        var toRowBase = self.toRow;
        self.toRow = function (el) {
            var rowBase = toRowBase(el);
            rowBase.addCells([
                new cell("Код")
            ]);
            return rowBase;
        };

        var getSearchParamsBase = self.getSearchParams;
        self.getSearchParams = function () {
            var searchParamsBase = getSearchParamsBase();
            return searchParamsBase;
        };

        self.init();
        return self;
    }

    return listPage;
});