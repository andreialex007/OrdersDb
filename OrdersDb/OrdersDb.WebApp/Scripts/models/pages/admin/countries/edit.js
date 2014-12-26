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
        self.EDIT_TITLE(CommonResources.Edit_Country);
        self.NEW_TITLE(CommonResources.Create_Country);

        self.fields.Name = new textField(EntitiesResources.Country_Name, "", CommonResources.English_Country_Name_required);
        self.fields.RussianName = new textField(EntitiesResources.Country_RussianName, "", CommonResources.Russian_Country_Name_required);
        self.fields.Code = new textField(EntitiesResources.Country_Code, "", CommonResources.Country_Code_required);
        self.flagImage = ko.observable("");

        self.flagSelected = function (item, event) {
            var xhr = new XMLHttpRequest();
            var formData = new FormData();

            if (event.target.files.length != 1)
                return;

            formData.append("file", event.target.files[0]);
            formData.append("id", self.Id());
            xhr.open("POST", "/Countries/UploadFlag/", true);
            xhr.send(formData);
            xhr.addEventListener("load", function () {
                self.refreshFlag();
            }, false);

        };

        self.refreshFlag = function () {
            return self.flagImage("/Countries/GetFlag/" + self.Id() + "?t=" + Math.random());
        };

        self.loadEntityData = function (params) {
            self.refreshFlag();
            self.fields.Name.value("");
            self.fields.RussianName.value("");
            self.fields.Code.value("");
        };

        self.fromJSON = function (json) {
            self.fields.Name.value(json.Name);
            self.fields.RussianName.value(json.RussianName);
            self.fields.Code.value(json.Code);
            self.refreshFlag();
        };
        self.toJSON = function () {
            var json = { Id: self.Id() };
            json.Name = self.fields.Name.value();
            json.RussianName = self.fields.RussianName.value();
            json.Code = self.fields.Code.value();
            return json;
        };
        return self;
    }
    return cityPage;
});



