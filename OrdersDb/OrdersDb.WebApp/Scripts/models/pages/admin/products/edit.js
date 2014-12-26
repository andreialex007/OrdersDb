define(["knockout",
        "knockout.mapping",
        "sammy",
        "Scripts/models/pages/base/edit",
        "Scripts/models/controls/dropdowns/region/dropdown",
         "Scripts/models/controls/fields/textField",
         "Scripts/models/controls/fields/spinner",
        "Scripts/models/controls/fields/dropdown"
], function (ko, mapping, sammy, editPageBase, regionDropDown, textField, spinner, dropdown) {
    function cityPage(parent) {
        var self = new editPageBase(parent);
        self.EDIT_TITLE(EntitiesResources.Edit_Product);
        self.NEW_TITLE(EntitiesResources.Create_Product);

        //Основные данные
        self.fields.Name = new textField(EntitiesResources.Product_Name, "", EntitiesResources.Product_Name + CommonResources._Required_);
        self.fields.Category = new dropdown(EntitiesResources.Product_Category, 0, [], CommonResources.Select_a_Category);
        self.fields.BuyPrice = new spinner(EntitiesResources.Product_BuyPrice, 1, 1, 1, 1000000);
        self.fields.SellPrice = new spinner(EntitiesResources.Product_SellPrice, 1, 1, 1, 1000000);
        self.fields.IsService = ko.observable(false);


        self.fromJSON = function (json) {
            self.fields.Name.value(json.Name);
            self.fields.BuyPrice.value(json.BuyPrice || 1);
            self.fields.SellPrice.value(json.SellPrice || 1);
            self.fields.IsService(json.IsService);

            var mapped = $.map(json.CategoryItems, function (item) {
                var spacePrefix = ".";
                for (var i = 0; i < item.ParentsCount; i++) {
                    spacePrefix += spacePrefix;
                }
                return { Value: item.Id, Text: spacePrefix + item.Name };
            });

            self.fields.Category.avaliableValues(mapped);
            self.fields.Category.value(json.CategoryId);
        };

        self.loadEntityData = function (params) {
            self.getById(self.Id(), function (json) {
                self.fromJSON(json);
            });
        };

        self.toJSON = function () {
            var json = { Id: self.Id() };
            json.Name = self.fields.Name.value();
            json.BuyPrice = self.fields.BuyPrice.value();
            json.SellPrice = self.fields.SellPrice.value();
            json.IsService = self.fields.IsService();
            json.CategoryId = self.fields.Category.value();
            if (json.CategoryId) {
                json.Category = {
                    Id: self.fields.Category.value()
                };
            }

            return json;
        };
        return self;
    }
    return cityPage;
});



