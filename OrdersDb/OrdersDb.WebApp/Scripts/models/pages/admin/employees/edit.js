﻿define([
    "knockout",
    "knockout.mapping",
    "sammy",
    "Scripts/models/pages/base/edit",
    "Scripts/models/controls/fields/dropdown",
    "Scripts/models/controls/fields/textField"
], function (ko, mapping, sammy, editPageBase, dropdown, textField) {

    function editPage(parent) {
        var self = new editPageBase(parent);

        self.EDIT_TITLE("Редактирование сотрудника");
        self.NEW_TITLE("Создание сотрудника");

        self.fields.FirstName = new textField("Имя", "", "Имя (обязательно)");
        self.fields.LastName = new textField("Фамилия", "", "Фамилия (обязательно)");
        self.fields.Patronymic = new textField("Отчество", "", "Отчество");
        self.fields.Position = new dropdown("Профессия", 0, [], "Выберите профессию из выпадающего списка");
        self.fields.Email = new textField("Email", "", "Email (обязательно)");
        self.fields.SNILS = new textField("SNILS", "", "SNILS (обязательно)");



        var loadBase = self.load;
        self.load = function (params) {
            loadBase(params);
        };

        self.fromJSON = function (json) {
            self.fields.Position.avaliableValues(utils.idNameToTextValue(json.AvaliablePositions));
            self.fields.Position.value(json.PositionId);
            self.fields.Position.text(json.PositionName);

            self.fields.FirstName.value(json.FirstName);
            self.fields.LastName.value(json.LastName);
            self.fields.Patronymic.value(json.Patronymic);
            self.fields.Email.value(json.Email);
            self.fields.SNILS.value(json.SNILS);
        };

        self.toJSON = function () {
            var json = { Id: self.Id() };

            if (self.fields.Position.value()) {
                json.Position = {};
                json.Position.Id = self.fields.Position.value();
                json.PositionId = self.fields.Position.value();
                json.Position.Name = self.fields.Position.text();
            }


            json.FirstName = self.fields.FirstName.value();
            json.LastName = self.fields.LastName.value();
            json.Patronymic = self.fields.Patronymic.value();

            json.Email = self.fields.Email.value();
            json.SNILS = self.fields.SNILS.value();

            return json;
        };

        self.loadEntityData = function (params) {
            self.getById(self.Id(), function (json) {
                self.fromJSON(json);
            });
        };
        return self;
    }

    return editPage;

});



