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
        self.title(CommonResources.Products_List);

        self.table.columns(self.table.columns().concat([
            new column("Category.Name", EntitiesResources.Product_Category, "200px"),
            new column("SellPrice", EntitiesResources.Product_SellPrice, "150px"),
            new column("BuyPrice", EntitiesResources.Product_BuyPrice, "150px"),
            new column("IsService", EntitiesResources.Product_IsService, "150px")
        ]));

        self.categoryName = new textField(CommonResources.Product_Category, "", CommonResources.Please_Enter_At_Least_Three_Symbols);
        self.sellPrice = new rangeSpinnerControl(CommonResources.Product_SellPrice, 0, 1500, 1);
        self.buyPrice = new rangeSpinnerControl(CommonResources.Product_BuyPrice, 0, 1500, 1);

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