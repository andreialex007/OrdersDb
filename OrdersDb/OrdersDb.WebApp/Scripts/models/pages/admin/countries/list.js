define([
        "knockout",
        "knockout.mapping",
        "sammy",
        "Scripts/models/pages/base/namedList",
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
        self.title(CommonResources.Countries_List);

        self.table.columns(self.table.columns().concat([
            new column("RussianName", EntitiesResources.Country_RussianName, "200px"),
            new column("Code", EntitiesResources.Country_Code, "200px")
        ]));

        self.russianName = new textField(EntitiesResources.Country_RussianName, "", CommonResources.Please_Enter_At_Least_ThreeSymbols);
        self.code = new textField(EntitiesResources.Country_Code);

        var toRowBase = self.toRow;
        self.toRow = function (el) {
            var rowBase = toRowBase(el);
            rowBase.addCells([
                new cell(el.RussianName),
                new cell(el.Code)
            ]);
            return rowBase;
        };

        var getSearchParamsBase = self.getSearchParams;
        self.getSearchParams = function () {
            var searchParamsBase = getSearchParamsBase();
            return $.extend(searchParamsBase, {
                RussianName: self.russianName.value().length < 3 ? "" : self.russianName.value(),
                Code: self.code.value() || ""
            });
        };

        self.init();
        return self;
    }

    return listPage;
});