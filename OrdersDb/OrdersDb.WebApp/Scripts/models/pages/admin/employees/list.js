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
        "Scripts/models/controls/fields/intListField"
], function (ko, mapping, sammy, listPageBase, table, column, row, cell, dropdown, textField, intField, intListField, rangeSpinnerControl) {

    function listPage(parent) {
        var self = new listPageBase(parent);
        self.title(CommonResources.Employees_List);

        self.table.columns(self.table.columns().concat([
            new column("FirstName", EntitiesResources.Employee_FirstName),
            new column("LastName", EntitiesResources.Employee_LastName),
            new column("Patronymic", EntitiesResources.Employee_Patronymic),
            new column("Email", EntitiesResources.Employee_Email),
            new column("Position.Name", EntitiesResources.Position_Name),
            new column("SNILS", EntitiesResources.Employee_SNILS)
        ]));

        self.firstName = new textField(EntitiesResources.Employee_FirstName, "", CommonResources.Please_Enter_At_Least_ThreeSymbols);
        self.lastName = new textField(EntitiesResources.Employee_LastName, "", CommonResources.Please_Enter_At_Least_ThreeSymbols);
        self.patronymic = new textField(EntitiesResources.Employee_Patronymic, "", CommonResources.Please_Enter_At_Least_ThreeSymbols);
        self.email = new textField(EntitiesResources.Employee_Email, "", CommonResources.Please_Enter_At_Least_ThreeSymbols);
        self.positionName = new textField(EntitiesResources.Position_Name, "", CommonResources.Please_Enter_At_Least_ThreeSymbols);
        self.snils = new textField(EntitiesResources.Employee_SNILS, "", CommonResources.Please_Enter_At_Least_ThreeSymbols);

        var toRowBase = self.toRow;
        self.toRow = function (el) {
            var rowBase = toRowBase(el);
            rowBase.addCells([
                new cell(el.FirstName),
                new cell(el.LastName),
                new cell(el.Patronymic),
                new cell(el.Email),
                new cell(el.PositionName),
                new cell(el.SNILS)
            ]);
            return rowBase;
        };

        var getSearchParamsBase = self.getSearchParams;
        self.getSearchParams = function () {
            var searchParamsBase = getSearchParamsBase();
            return $.extend(searchParamsBase, {
                FirstName: self.firstName.value().length < self.SEARCH_MIN_LENGTH ? "" : self.firstName.value(),
                LastName: self.lastName.value().length < self.SEARCH_MIN_LENGTH ? "" : self.lastName.value(),
                Patronymic: self.patronymic.value().length < self.SEARCH_MIN_LENGTH ? "" : self.patronymic.value(),
                Email: self.email.value().length < self.SEARCH_MIN_LENGTH ? "" : self.email.value(),
                PositionName: self.positionName.value().length < self.SEARCH_MIN_LENGTH ? "" : self.positionName.value(),
                SNILS: self.snils.value().length < self.SEARCH_MIN_LENGTH ? "" : self.snils.value(),
            });
        };

        self.init();
        return self;
    }

    return listPage;
});