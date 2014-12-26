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
        self.EDIT_TITLE(CommonResources.Edit_Position);
        self.NEW_TITLE(CommonResources.Create_Position);

        self.fields.Name = new textField(EntitiesResources.Position_Name, "", EntitiesResources.Position_Name + CommonResources._Required_);

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



