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
        self.title(CommonResources.Houses_List);

        self.table.columns(self.table.columns().concat([
            new column("Number", EntitiesResources.House_Number),
            new column("Building", EntitiesResources.House_Building),
            new column("PostalCode", EntitiesResources.House_PostalCode),
            new column("Street.Name", EntitiesResources.House_Street)
        ]));

        self.number = new intListField(EntitiesResources.House_Number);
        self.building = new textField(EntitiesResources.House_Building);
        self.postalCode = new textField(EntitiesResources.House_PostalCode);
        self.streetName = new textField(EntitiesResources.House_Street);

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