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
        self.title("Список товаров");

        self.table.columns(self.table.columns().concat([
            new column("Category.Name", "Категория продукта", "200px"),
            new column("SellPrice", "Цена продажи", "150px"),
            new column("BuyPrice", "Цена покупки", "150px"),
            new column("IsService", "Является услугой", "150px")
        ]));

        self.categoryName = new textField("Категория продукта", "", "Введите как минимум 3 символа");
        self.sellPrice = new rangeSpinnerControl("Цена продажи", 0, 1500, 1);
        self.buyPrice = new rangeSpinnerControl("Цена покупки", 0, 1500, 1);

        var toRowBase = self.toRow;
        self.toRow = function (el) {
            var rowBase = toRowBase(el);
            rowBase.addCells([
                new cell(el.CategoryName, el.CategoryId),
                new cell(el.SellPrice),
                new cell(el.BuyPrice),
                new cell(el.IsService)
            ]);
            return rowBase;
        };

        var getSearchParamsBase = self.getSearchParams;
        self.getSearchParams = function () {
            var searchParamsBase = getSearchParamsBase();
            return $.extend(searchParamsBase, {
                MinBuyPrice: self.buyPrice.min.value(),
                MaxBuyPrice: self.buyPrice.max.value(),
                MinSellPrice: self.sellPrice.min.value(),
                MaxSellPrice: self.sellPrice.max.value(),
                IsService: null,
                CategoryName: self.categoryName.value().length < 3 ? "" : self.categoryName.value()
            });
        };

        self.init();
        return self;
    }

    return listPage;
});