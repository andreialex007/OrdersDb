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
        self.EDIT_TITLE(CommonResources.Edit_User);
        self.NEW_TITLE(CommonResources.New_User);

        self.fields.Name = new textField(EntitiesResources.User_Name, "", EntitiesResources.User_Name + CommonResources._Required_);
        self.fields.Email = new textField(EntitiesResources.User_Email, "", EntitiesResources.User_Email + CommonResources._Required_);
        self.fields.Password = new textField(EntitiesResources.User_Password, "", EntitiesResources.User_Password + CommonResources._Required_);
        self.Roles = ko.observableArray([]);
        self.userImage = ko.observable("");

        self.userImageSelected = function (item, event) {
            var xhr = new XMLHttpRequest();
            var formData = new FormData();

            if (event.target.files.length != 1)
                return;

            formData.append("file", event.target.files[0]);
            formData.append("id", self.Id());
            xhr.open("POST", "/Users/UploadImage/", true);
            xhr.send(formData);
            xhr.addEventListener("load", function () {
                self.refreshImage();
            }, false);
        };

        self.refreshImage = function () {
            return self.userImage("/Users/GetImage/" + self.Id() + "?t=" + Math.random());
        };

        self.fromJSON = function (json) {
            self.fields.Name.value(json.Name);
            self.fields.Email.value(json.Email);
            self.fields.Password.value(json.Password);

            $(json.Roles).each(function (i, element) {
                element.IsSelected = ko.observable(element.IsSelected || false);
            });

            self.Roles(mapping.fromJS(json.Roles)());
            self.refreshImage();
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



