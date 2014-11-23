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
        self.EDIT_TITLE("Редактирование пользователя");
        self.NEW_TITLE("Создание пользователя");

        self.fields.Name = new textField("Имя", "", "Имя (обязательно)");
        self.fields.Email = new textField("Email", "", "Email (обязательно)");
        self.fields.Password = new textField("Пароль", "", "Пароль (обязательно)");
        self.Roles = ko.observableArray([]);

        self.fromJSON = function (json) {
            self.fields.Name.value(json.Name);
            self.fields.Email.value(json.Email);
            self.fields.Password.value(json.Password);

            $(json.Roles).each(function (i, element) {
                element.IsSelected = ko.observable(element.IsSelected || false);
            });

            self.Roles(mapping.fromJS(json.Roles)());
        };
        self.toJSON = function () {
            var json = { Id: self.Id() };
            json.Name = self.fields.Name.value();
            json.Email = self.fields.Email.value();
            json.Password = self.fields.Password.value();

            var selected = $.grep(self.Roles(), function (element) {
                return element.IsSelected() == true;
            });
            json.Roles = ko.toJS(selected);
            return json;
        };
        self.loadEntityData = function (params) {
            self.getById(self.Id(), function (json) {
                self.fromJSON(json);
            });
        };
        return self;
    }
    return cityPage;
});



