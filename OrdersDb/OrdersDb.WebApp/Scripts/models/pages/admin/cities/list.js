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
        self.title(CommonResources.List_Of_Cities);

        self.table.columns(self.table.columns().concat([
            new column("Region.Name", EntitiesResources.Region, "200px"),
            new column("Population", CommonResources.Population, "150px")
        ]));

        self.regionName = new textField(CommonResources.Name_Of_Region, "", CommonResources.Please_Enter_At_Least_ThreeSymbols);
        self.population = new rangeSpinnerControl(CommonResources.Population, 0, 15000000);

        var toRowBase = self.toRow;
        self.toRow = function (el) {
            var rowBase = toRowBase(el);
            rowBase.addCells([
                new cell(el.RegionName, el.RegionId),
                new cell(el.Population)
            ]);
            return rowBase;
        };

        var getSearchParamsBase = self.getSearchParams;
        self.getSearchParams = function () {
            var searchParamsBase = getSearchParamsBase();
            return $.extend(searchParamsBase, {
                MinPopulation: self.population.min.value(),
                MaxPopulation: self.population.max.value(),
                RegionName: self.regionName.value().length < 3 ? "" : self.regionName.value()
            });
        };

        self.init();
        return self;
    }

    return listPage;
});