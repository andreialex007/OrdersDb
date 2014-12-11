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
        self.EDIT_TITLE("Редактирование клиента");
        self.NEW_TITLE("Создание клиента");

        //Основные данные
        self.fields.Name = new textField("Общее имя", "", "Введите общее имя (обязательно)");
        self.fields.FullName = new textField("Полное имя", "", "Введите полное имя (обязательно)");
        self.fields.INN = new textField("ИНН", "", "Введите ИНН (обязательно)");
        self.fields.OGRN = new textField("ОГРН", "", "Введите ОГРН (обязательно)");

        //Местонахождение
        self.fields.Country = new dropdown("Страна", 0, [], "Выберите страну");
        self.fields.Region = new dropdown("Регион", 0, [], "Выберите регион");
        self.fields.City = new dropdown("Город", 0, [], "Выберите город");
        self.fields.Street = new dropdown("Улица", 0, [], "Выберите улицу");

        self.locationId = 0;
        self.fields.Number = new textField("Номер дома", "");
        self.fields.Building = new textField("Строение", "");
        self.fields.PostalCode = new textField("Почтовый индекс", "");

        self.fields.Country.change = function () {
            var val = self.fields.Country.value();

            if (!val) {
                self.fields.Region.avaliableValues([]);
                return;
            }

            $.ajax({
                url: "/Regions/GetRegionsInCountry",
                dataType: "json",
                data: { countryId: val }
            }).done(function (data) {
                self.fields.Region.avaliableValues(utils.idNameToTextValue(data));
            });
        };

        self.fields.Region.change = function () {
            var val = self.fields.Region.value();

            if (!val) {
                self.fields.City.avaliableValues([]);
                return;
            }

            $.ajax({
                url: "/Cities/GetCitiesInRegion",
                dataType: "json",
                data: { regionId: val }
            }).done(function (data) {
                self.fields.City.avaliableValues(utils.idNameToTextValue(data));
            });
        };

        self.fields.City.change = function () {
            var val = self.fields.City.value();

            if (!val) {
                self.fields.Street.avaliableValues([]);
                return;
            }

            $.ajax({
                url: "/Streets/GetStreetsByCity",
                dataType: "json",
                data: { cityId: val }
            }).done(function (data) {
                self.fields.Street.avaliableValues(utils.idNameToTextValue(data));
            });
        };

        self.fromJSON = function (json) {
            self.fields.Country.avaliableValues(utils.idNameToTextValue(json.Countries));
            self.fields.Region.avaliableValues(utils.idNameToTextValue(json.Regions));
            self.fields.Street.avaliableValues(utils.idNameToTextValue(json.Streets));
            self.fields.City.avaliableValues(utils.idNameToTextValue(json.Cities));

            self.fields.Country.value(json.CountryId);
            self.fields.Region.value(json.RegionId);
            self.fields.Street.value(json.StreetId);
            self.fields.City.value(json.CityId);

            self.fields.Name.value(json.Name);
            self.fields.FullName.value(json.FullName);
            self.fields.INN.value(json.INN);
            self.fields.OGRN.value(json.OGRN);

            self.locationId = json.Location.Id;
            self.fields.Number.value(json.Location.Number);
            self.fields.Building.value(json.Location.Building);
            self.fields.PostalCode.value(json.Location.PostalCode);
        };

        self.onError = function (json) {
            $(self.fieldsArr()).each(function (index, element) {
                element.hasErrors(false);
            });

            $(json.Errors).each(function (index, element) {
                if (element.PropertyName == "Location.Street")
                    self.fields.Street.appendError(element.ErrorMessage);
                else if (element.PropertyName == "Location.Street.City")
                    self.fields.City.appendError(element.ErrorMessage);
                else if (element.PropertyName == "Location.Street.City.Region")
                    self.fields.Region.appendError(element.ErrorMessage);
                else if (element.PropertyName == "Location.Street.City.Region.Country")
                    self.fields.Country.appendError(element.ErrorMessage);
                else {
                    self.fields[element.PropertyName].appendError(element.ErrorMessage);
                }
            });
        };

        self.loadEntityData = function (params) {
            self.getById(self.Id(), function (json) {
                self.fromJSON(json);
            });
        };

        self.toJSON = function () {
            var json = { Id: self.Id() };
            json.Name = self.fields.Name.value();
            json.FullName = self.fields.FullName.value();
            json.INN = self.fields.INN.value();
            json.OGRN = self.fields.OGRN.value();
            json.LocationId = self.locationId;

            json.Location = {
                "Id": self.locationId,
                "Number": self.fields.Number.value() || "0",
                "Building": self.fields.Building.value(),
                "PostalCode": self.fields.PostalCode.value(),
                "StreetId": self.fields.Street.value(),
                "Street": {
                    "Id": self.fields.Street.value(),
                    "CityId": self.fields.City.value(),
                    "City": {
                        "Id": self.fields.City.value(),
                        "RegionId": self.fields.Region.value(),
                        "Region": {
                            "Id": self.fields.Region.value(),
                            "CountryId": self.fields.Country.value(),
                            "Country": {
                                "Id": self.fields.Country.value()
                            }
                        }
                    }
                }
            };

            return json;
        };
        return self;
    }
    return cityPage;
});



