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
        self.title(CommonResources.Clients_List);

        self.table.columns(self.table.columns().concat([
            new column("FullName", EntitiesResources.Client_FullName),
            new column("INN", EntitiesResources.Client_INN, "200px"),
            new column("OGRN", EntitiesResources.Client_OGRN, "200px"),
            new column("Location", CommonResources.Address, "200px")
        ]));

        self.fullName = new textField(EntitiesResources.Client_FullName, "", CommonResources.Please_Enter_At_Least_ThreeSymbols);
        self.inn = new textField("ИНН", "", CommonResources.Please_Enter_At_Least_ThreeSymbols);
        self.ogrn = new textField("ОГРН", "", CommonResources.Please_Enter_At_Least_ThreeSymbols);
        self.location = new textField("Адрес", "", CommonResources.Please_Enter_At_Least_ThreeSymbols);

        var toRowBase = self.toRow;
        self.toRow = function (el) {
            var rowBase = toRowBase(el);
            rowBase.addCells([
                new cell(el.FullName),
                new cell(el.INN),
                new cell(el.OGRN),
                new cell(el.FullLocationString)
            ]);
            return rowBase;
        };

        var getSearchParamsBase = self.getSearchParams;
        self.getSearchParams = function () {
            var searchParamsBase = getSearchParamsBase();
            return $.extend(searchParamsBase, {
                FullName: self.fullName.value().length < 3 ? "" : self.fullName.value(),
                INN: self.inn.value().length < 3 ? "" : self.inn.value(),
                OGRN: self.ogrn.value().length < 3 ? "" : self.ogrn.value(),
                LocationString: self.location.value().length < 3 ? "" : self.location.value()
            });
        };

        self.init();
        return self;
    }

    return listPage;
});