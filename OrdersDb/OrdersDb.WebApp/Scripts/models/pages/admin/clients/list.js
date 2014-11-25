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
        self.title("Список клиентов");

        self.table.columns(self.table.columns().concat([
            new column("FullName", "Полное имя"),
            new column("INN", "ИНН", "200px"),
            new column("OGRN", "ОГРН", "200px"),
            new column("Location", "Адрес", "200px")
        ]));

        self.fullName = new textField("Полное название", "", "Введите как минимум 3 символа");
        self.inn = new textField("ИНН", "", "Введите как минимум 3 символа");
        self.ogrn = new textField("ОГРН", "", "Введите как минимум 3 символа");
        self.location = new textField("Адрес", "", "Введите как минимум 3 символа");

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