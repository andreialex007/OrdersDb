define(["knockout",
        "knockout.mapping",
        "sammy",
        "Scripts/models/pages/base/edit",
        "Scripts/models/controls/fields/dropdown",
         "Scripts/models/controls/fields/textField",
         "Scripts/models/controls/fields/spinner"
], function (ko, mapping, sammy, editPageBase, dropdown, textField, spinner) {
    function cityPage(parent) {
        var self = new editPageBase(parent);
        self.EDIT_TITLE(CommonResources.Regions_List);
        self.NEW_TITLE(CommonResources.Create_Region);

        self.fields.Name = new textField(EntitiesResources.Region_Name, "", EntitiesResources.Region_Name + CommonResources._Required_);
        self.fields.Country = new dropdown(EntitiesResources.Region_Country, 0, [], EntitiesResources.Region_Country + CommonResources._Required_);

        self.fromJSON = function (json) {
            self.fields.Country.avaliableValues(utils.idNameToTextValue(json.Countries));
            self.fields.Country.value(json.CountryId);
            self.fields.Name.value(json.Name);
        };
        self.toJSON = function () {
            var json = { Id: self.Id() };
            json.Name = self.fields.Name.value();
            var countryId = self.fields.Country.value();
            json.CountryId = countryId;
            json.Country = countryId ? {
                Id: countryId
            } : null;

            return json;
        };

        self.loadEntityData = function (params) {
            self.getById(self.Id(), function (json) {
                self.fromJSON(json);
            });
        };
        return self;
    }
    return cityPage;
});



