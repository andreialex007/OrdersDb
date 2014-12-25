define([
        "knockout",
        "Scripts/models/controls/fields/textField",
        "Scripts/models/controls/fields/dropdown",
        "./dropdown"
], function (ko, textField, dropdown, regionDropDown) {

    function regionsDropdown(title, text, value, placeholder) {
        var self = new regionDropDown(title, text, value, placeholder);
        self.cities = new dropdown("", "", [], CommonResources.Please_Choose_The_City);

        self.regions.change = function () {
            self.loadCities(self.regions.value());
        };

        self.errorsText = ko.observable("");
        self.appendError = function (txt) {
            var newErrorText = self.errorsText() + ", " + txt;
            self.errorsText(trim(newErrorText, ", "));
        };
        self.hasErrors = ko.computed({
            read: function () {
                if (self.errorsText())
                    return true;
                return false;
            },
            write: function (val) {
                if (val == false)
                    self.errorsText("");
            }
        });

        self.loadCities = function () {

        };

        self.loadCitiesCompleted = function (json) {
            self.cities.avaliableValues(json);
        };

        self.accept = function () {
            if (self.cities.value() != "0") {
                self.selectedId = self.cities.value();
                self.selectedField.value(self.cities.text());
                self.reset();
            }
        };

        self.reset = function () {
            self.countries.avaliableValues([]);
            self.regions.avaliableValues([]);
            self.cities.avaliableValues([]);
            self.dropAreaVisible(false);
        };
        return self;
    }

    return regionsDropdown;

});