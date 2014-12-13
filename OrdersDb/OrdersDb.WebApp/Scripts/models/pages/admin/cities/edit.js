﻿define(["knockout",
        "knockout.mapping",
        "sammy",
        "Scripts/models/pages/base/edit",
        "Scripts/models/controls/dropdowns/region/dropdown",
         "Scripts/models/controls/fields/textField",
         "Scripts/models/controls/fields/spinner"
], function (ko, mapping, sammy, editPageBase, regionDropDown, textField, spinner) {
    function cityPage(parent) {
        var self = new editPageBase(parent);
        self.EDIT_TITLE("Редактирование города");
        self.NEW_TITLE("Создание города");

        self.fields.Name = new textField("Название", "", "Введите название города (обязательно)");
        self.fields.Region = new regionDropDown("Регион", "", "", "Выберите регион из выпадающего списка");
        self.fields.population = new spinner("Численность населения", 1000, 1000, 1000, 15000000);

        self.fields.Region.loadCountries = function () {
            $.ajax({
                url: "/Countries/GetAllNameValues",
                dataType: "json",
            }).done(function (data) {
                self.fields.Region.loadCountriesCompleted($.map(data, function (el) {
                    return { Text: el.Name, Value: el.Id };
                }));
            });
        };
        self.fields.Region.loadRegions = function () {
            $.ajax({
                url: "/Regions/GetAllNameValues",
                dataType: "json",
            }).done(function (data) {
                self.fields.Region.loadRegionsCompleted($.map(data, function (el) {
                    return { Text: el.Name, Value: el.Id };
                }));
            });
        };
        self.fromJSON = function (json) {
            self.fields.Region.selectedField.value(json.RegionName);
            self.fields.Region.selectedId = json.RegionId;
            self.fields.Name.value(json.Name);
            self.fields.population.value(json.Population);
        };
        self.toJSON = function () {
            var json = { Id: self.Id() };

            if (self.fields.Region.selectedId) {
                json.Region = {};
                json.Region.Name = self.fields.Region.selectedField.value();
                json.Region.Id = self.fields.Region.selectedId;
                json.RegionId = self.fields.Region.selectedId;
            }

            json.Name = self.fields.Name.value();
            json.Population = self.fields.population.value();
            return json;
        };
        return self;
    }
    return cityPage;
});



