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
        self.EDIT_TITLE("Редактирование должности");
        self.NEW_TITLE("Создание должности");

        self.fields.Name = new textField("Название", "", "Название должности (обязательно)");

        self.fromJSON = function (json) {
            self.fields.Name.value(json.Name);
        };
        self.toJSON = function () {
            var json = { Id: self.Id() };
            json.Name = self.fields.Name.value();
            return json;
        };
        return self;
    }
    return cityPage;
});



