define([
        "knockout",
        "Scripts/models/pages/base/page",
        "Scripts/models/controls/fields/textField",
        "Scripts/models/controls/fields/intField",
        "Scripts/models/controls/fields/intListField",
        "Scripts/models/controls/fields/rangeSpinnerControl"
], function (ko, pageBase, textField) {

    function informationTab(parent) {
        var self = new pageBase(parent);
        self.visible = ko.observable(false);
        self.categoriesAmount = ko.observable(0);
        self.productsAmount = ko.observable(0);
        self.creationDate = ko.observable("");
        self.modificationDate = ko.observable("");
        self.fromJSON = function (json) {
            self.creationDate(json.Created);
            self.modificationDate(json.Modified);
            self.categoriesAmount(json.CategoriesAmount);
            self.productsAmount(json.ProductsAmount);
        };
        self.unload = function () {

        };
        return self;
    }

    return informationTab;

});