define([
        "knockout",
        "Scripts/models/controls/fields/textField",
        "Scripts/models/controls/fields/dropdown"
], function (ko, textField, dropdown) {

    function regionsDropdown(title, text, value, placeholder) {
        var self = {};
        self.selectedId = value;
        self.selectedField = new textField(title, text, placeholder);

        self.countries = new dropdown("", "", [], CommonResources.Please_Choose_The_Country);
        self.regions = new dropdown("", "", [], CommonResources.Please_Choose_The_Region);

        self.dropAreaVisible = ko.observable(false);
        self.clickDropDown = function (item, event) {
            event.stopPropagation();
            self.dropAreaVisible(!self.dropAreaVisible());
            if (self.dropAreaVisible()) {
                self.loadCountries();
            }
        };

        self.clickInside = function (item, event) {
            event.stopPropagation();
        };

        self.loadCountries = function () {
            
        };

        self.loadCountriesCompleted = function (json) {
            self.countries.avaliableValues(json);
        };

        self.loadRegions = function () {
           
        };

        self.loadRegionsCompleted = function (json) {
            self.regions.avaliableValues(json);
        };

        self.countries.change = function () {
            self.loadRegions(self.countries.value());
        };

        self.accept = function () {
            if (self.regions.value() != "0") {
                self.selectedId = self.regions.value();
                self.selectedField.value(self.regions.text());
                self.reset();
            }
        };

        self.reset = function() {
            self.countries.avaliableValues([]);
            self.regions.avaliableValues([]);
            self.dropAreaVisible(false);
        };

        $(document.body).click(function () {
            self.reset();
        });
        

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


        return self;
    }

    return regionsDropdown;

});