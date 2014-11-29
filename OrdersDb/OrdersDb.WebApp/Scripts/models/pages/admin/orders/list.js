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
            new column("Code.Value", "Код", "100px"),
            new column("Client.Name", "Заказчик"),
            new column("BuyPrice", "Цена покупки", "100px"),
            new column("SellPrice", "Цена покупки", "100px"),
            new column("Total", "Всего", "100px")
        ]));

        self.Code = new textField("Код закза", "", "Введите как минимум 3 символа");
        self.Client = new textField("Заказчик", "", "Введите как минимум 3 символа");
        self.BuyPrice = new rangeSpinnerControl("Цена покупки", 0, 100000, 1);
        self.SellPrice = new rangeSpinnerControl("Цена продажи", 0, 100000, 1);

        var toRowBase = self.toRow;
        self.toRow = function (el) {
            var rowBase = toRowBase(el);
            rowBase.addCells([
                new cell(el.Code),
                new cell(el.ClientName),
                new cell(el.BuyPrice),
                new cell(el.SellPrice),
                new cell(el.TotalItems)
            ]);
            return rowBase;
        };

        var getSearchParamsBase = self.getSearchParams;
        self.getSearchParams = function () {
            var searchParamsBase = getSearchParamsBase();
            return $.extend(searchParamsBase, {
                Code: self.Code.value().length < 3 ? "" : self.Code.value(),
                ClientName: self.Client.value().length < 3 ? "" : self.Client.value(),
                MinSellPrice: self.SellPrice.min.value(),
                MaxSellPrice: self.SellPrice.max.value(),
                MinBuyPrice: self.BuyPrice.min.value(),
                MaxBuyPrice: self.BuyPrice.max.value(),
            });
        };

        self.init();
        return self;
    }

    return listPage;
});