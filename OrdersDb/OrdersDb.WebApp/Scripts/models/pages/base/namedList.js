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
        self.title("Список клиентов");

        self.table.columns.push(new column("Name", "Название"));

        self.name = new textField("Название", "", "Введите как минимум 3 символа");

        //Маппинг json данных в строку
        var toRowBase = self.toRow;
        self.toRow = function (el) {
            var rowBase = toRowBase(el);
            rowBase.addCells(new cell(el.Name));
            return rowBase;
        };

        //Получение параметров поиска
        var searchParamsBase = self.getSearchParams;
        self.getSearchParams = function () {
            var params = searchParamsBase();
            params.Name = self.name.value().length < 3 ? "" : self.name.value();
            return params;
        };

        return self;
    }

    return listPage;
});