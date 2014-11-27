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
        self.title("Список стран");

        self.table.columns(self.table.columns().concat([
            new column("FirstName", "Имя"),
            new column("LastName", "Фамилия"),
            new column("Patronymic", "Отчество"),
            new column("Email", "Email"),
            new column("Position.Name", "Должность"),
            new column("SNILS", "СНИЛС")
        ]));

        self.firstName = new textField("Имя", "", "Введите как минимум 3 символа");
        self.lastName = new textField("Фамилия", "", "Введите как минимум 3 символа");
        self.patronymic = new textField("Отчество", "", "Введите как минимум 3 символа");
        self.email = new textField("Email", "", "Введите email (минимум 3 символа)");
        self.positionName = new textField("Название должности", "", "Введите как минимум 3 символа");
        self.snils = new textField("СНИЛС", "", "Введите как минимум 3 символа");

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