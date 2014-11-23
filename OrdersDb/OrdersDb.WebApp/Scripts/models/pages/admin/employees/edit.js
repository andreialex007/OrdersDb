define([
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
        };

        self.toJSON = function () {
            var json = { Id: self.Id() };

            json.PositionId = self.fields.Position.value();
            json.PositionName = self.fields.Position.text();

            json.FirstName = self.fields.FirstName.value();
            json.LastName = self.fields.LastName.value();
            json.Patronymic = self.fields.Patronymic.value();

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



