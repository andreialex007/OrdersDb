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
        self.EDIT_TITLE("Редактирование заказа");
        self.NEW_TITLE("Создание заказа");

//        self.fields.name = new textField("Название", "", "Введите название города (обязательно)");
//        self.fields.population = new spinner("Численность населения", 1000, 1000, 1000, 15000000);

        self.fromJSON = function (json) {
            
        };
        self.toJSON = function () {
            var json = { Id: self.Id() };
           
            return json;
        };
        return self;
    }
    return cityPage;
});



