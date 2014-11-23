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
        self.title("Список домов");

        self.table.columns(self.table.columns().concat([
            new column("Number", "Номер"),
            new column("Building", "Корпус"),
            new column("PostalCode", "Индекс"),
            new column("Street.Name", "Улица")
        ]));

        self.number = new intListField("Номера домов");
        self.building = new textField("Корпус");
        self.postalCode = new textField("Почтовый индекс");
        self.streetName = new textField("Название улицы");

        var toRowBase = self.toRow;
        self.toRow = function (el) {
            var rowBase = toRowBase(el);
            rowBase.addCells([
                new cell(el.Number),
                new cell(el.Building),
                new cell(el.PostalCode),
                new cell(el.StreetName, el.StreetId)
            ]);
            return rowBase;
        };

        var getSearchParamsBase = self.getSearchParams;
        self.getSearchParams = function () {
            var searchParamsBase = getSearchParamsBase();
            return $.extend(searchParamsBase, {
                Numbers: utils.commaListToArray(self.number.value()),
                Building: self.building.value() || "",
                PostalCode: self.postalCode.value() || "",
                StreetName: self.streetName.value() || "",
            });
        };

        self.init();
        return self;
    }

    return listPage;
});