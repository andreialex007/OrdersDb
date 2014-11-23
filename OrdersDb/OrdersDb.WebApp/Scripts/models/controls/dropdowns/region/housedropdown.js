define([
        "knockout",
        "Scripts/models/controls/fields/textField",
        "Scripts/models/controls/fields/dropdown",
        "./citydropdown"
], function (ko, textField, dropdown, citydropdown) {

    function citiesDropdown(title, text, value, placeholder) {
        var self = new citydropdown(title, text, value, placeholder);
        self.streets = new dropdown("", "", [], "Выберите улицу");

        self.cities.change = function () {
            self.loadstreets(self.cities.value());
        };

        self.loadstreets = function () {

        };

        self.loadstreetsCompleted = function (json) {
            self.streets.avaliableValues(json);
        };

        self.accept = function () {
            debugger;
            if (self.streets.value() != "0") {
                self.selectedId = self.streets.value();
                self.selectedField.value(self.streets.text());
                self.reset();
            }
        };

        self.reset = function () {
            self.countries.avaliableValues([]);
            self.cities.avaliableValues([]);
            self.streets.avaliableValues([]);
            self.regions.avaliableValues([]);
            self.dropAreaVisible(false);
        };
        return self;
    }

    return citiesDropdown;

});