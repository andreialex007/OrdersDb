define(["knockout",
        "knockout.mapping",
        "sammy",
        "Scripts/models/pages/base/edit",
        "Scripts/models/controls/dropdowns/region/cityDropDown",
         "Scripts/models/controls/fields/textField",
         "Scripts/models/controls/fields/spinner"
], function (ko, mapping, sammy, editPageBase, City, textField, spinner) {
    function cityPage(parent) {
        var self = new editPageBase(parent);
        self.EDIT_TITLE(CommonResources.Edit_Street);
        self.NEW_TITLE(CommonResources.New_Street);

        self.fields.Name = new textField(EntitiesResources.Street_Name, "", EntitiesResources.Street_Name + CommonResources._Required_);
        self.fields.City = new City(EntitiesResources.Street_City, "", "", EntitiesResources.Select_a_City_From_Dropdown);

        self.fields.City.loadCountries = function () {
            $.ajax({
                url: "/Countries/GetAllNameValues",
                dataType: "json",
            }).done(function (data) {
                self.fields.City.loadCountriesCompleted($.map(data, function (el) {
                    return { Text: el.Name, Value: el.Id };
                }));
            });
        };
        self.fields.City.loadRegions = function (id) {

            if (!id)
                return;

            $.ajax({
                url: "/Regions/GetRegionsInCountry",
                dataType: "json",
                data: { countryId: id }
            }).done(function (data) {
                self.fields.City.loadRegionsCompleted($.map(data, function (el) {
                    return { Text: el.Name, Value: el.Id };
                }));
            });
        };

        self.fields.City.loadCities = function (id) {

            if (!id)
                return;

            $.ajax({
                url: "/Cities/GetCitiesInRegion",
                dataType: "json",
                data: { regionId: id }
            }).done(function (data) {
                self.fields.City.loadCitiesCompleted($.map(data, function (el) {
                    return { Text: el.Name, Value: el.Id };
                }));
            });
        };

        self.fromJSON = function (json) {
            self.fields.City.selectedField.value(json.CityName);
            self.fields.City.selectedId = json.CityId;
            self.fields.Name.value(json.Name);
        };
        self.toJSON = function () {
            var json = { Id: self.Id() };
            json.Name = self.fields.Name.value();
            json.CityId = self.fields.City.selectedId;
            json.City = {
                Id: self.fields.City.selectedId,
            };
            return json;
        };
        return self;
    }
    return cityPage;
});



