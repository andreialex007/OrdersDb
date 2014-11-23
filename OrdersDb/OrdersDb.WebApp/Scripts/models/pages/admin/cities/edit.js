define(["knockout",
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

        self.fields.name = new textField("Название", "", "Введите название города (обязательно)");
        self.fields.regionDropDown = new regionDropDown("Регион", "", "", "Выберите регион из выпадающего списка");
        self.fields.population = new spinner("Численность населения", 1000, 1000, 1000, 15000000);

        self.fields.regionDropDown.loadCountries = function () {
            $.ajax({
                url: "/Countries/GetAllNameValues",
                dataType: "json",
            }).done(function (data) {
                self.fields.regionDropDown.loadCountriesCompleted($.map(data, function (el) {
                    return { Text: el.Name, Value: el.Id };
                }));
            });
        };
        self.fields.regionDropDown.loadRegions = function () {
            $.ajax({
                url: "/Regions/GetAllNameValues",
                dataType: "json",
            }).done(function (data) {
                self.fields.regionDropDown.loadRegionsCompleted($.map(data, function (el) {
                    return { Text: el.Name, Value: el.Id };
                }));
            });
        };
        self.fromJSON = function (json) {
            self.fields.regionDropDown.selectedField.value(json.RegionName);
            self.fields.regionDropDown.selectedId = json.RegionId;
            self.fields.name.value(json.Name);
            self.fields.population.value(json.Population);
        };
        self.toJSON = function () {
            var json = { Id: self.Id() };
            json.RegionName = self.fields.regionDropDown.selectedField.value();
            json.RegionId = self.fields.regionDropDown.selectedId;
            json.Name = self.fields.name.value();
            json.Population = self.fields.population.value();
            return json;
        };
        return self;
    }
    return cityPage;
});



