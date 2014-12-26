define(["knockout",
        "knockout.mapping",
        "sammy",
        "Scripts/models/pages/base/edit",
        "Scripts/models/controls/dropdowns/region/housedropdown",
         "Scripts/models/controls/fields/textField",
         "Scripts/models/controls/fields/spinner"
], function (ko, mapping, sammy, editPageBase, housedropdown, textField, spinner) {
    function StreetPage(parent) {
        var self = new editPageBase(parent);
        self.EDIT_TITLE(CommonResources.Edit_House);
        self.NEW_TITLE(CommonResources.Create_House);

        self.fields.Number = new spinner(EntitiesResources.House_Number, "", 1, 1, 999999);
        self.fields.Building = new textField(EntitiesResources.House_Building, "", CommonResources.Enter_Building);
        self.fields.PostalCode = new textField(EntitiesResources.House_PostalCode, "", CommonResources.Enter_PostalCode);
        self.fields.Street = new housedropdown(EntitiesResources.House_Street, "", "", CommonResources.Select_Street_From_Dropdown);

        self.fields.Street.loadCountries = function () {
            $.ajax({
                url: "/Countries/GetAllNameValues",
                dataType: "json",
            }).done(function (data) {
                self.fields.Street.loadCountriesCompleted($.map(data, function (el) {
                    return { Text: el.Name, Value: el.Id };
                }));
            });
        };

        self.fields.Street.loadRegions = function (id) {

            if (!id)
                return;

            $.ajax({
                url: "/Regions/GetRegionsInCountry",
                dataType: "json",
                data: { countryId: id }
            }).done(function (data) {
                self.fields.Street.loadRegionsCompleted($.map(data, function (el) {
                    return { Text: el.Name, Value: el.Id };
                }));
            });
        };

        self.fields.Street.loadCities = function (id) {

            if (!id)
                return;

            $.ajax({
                url: "/Cities/GetCitiesInRegion",
                dataType: "json",
                data: { regionId: id }
            }).done(function (data) {
                self.fields.Street.loadCitiesCompleted($.map(data, function (el) {
                    return { Text: el.Name, Value: el.Id };
                }));
            });
        };

        self.fields.Street.loadstreets = function (id) {

            if (!id)
                return;

            $.ajax({
                url: "/Streets/GetStreetsByCity",
                dataType: "json",
                data: { cityId: id }
            }).done(function (data) {
                self.fields.Street.loadstreetsCompleted($.map(data, function (el) {
                    return { Text: el.Name, Value: el.Id };
                }));
            });
        };

        self.fromJSON = function (json) {
            self.fields.Street.selectedField.value(json.StreetName);
            self.fields.Street.selectedId = json.StreetId;
            self.fields.Number.value(json.Number);
            self.fields.Building.value(json.Building);
            self.fields.PostalCode.value(json.PostalCode);
        };
        self.toJSON = function () {
            var json = { Id: self.Id() };
            json.StreetName = self.fields.Street.selectedField.value();
            json.StreetId = self.fields.Street.selectedId;
            json.Number = self.fields.Number.value();
            json.Building = self.fields.Building.value();
            json.PostalCode = self.fields.PostalCode.value();
            json.Street = {
                Id: self.fields.Street.selectedId
            };
            return json;
        };
        return self;
    }
    return StreetPage;
});



