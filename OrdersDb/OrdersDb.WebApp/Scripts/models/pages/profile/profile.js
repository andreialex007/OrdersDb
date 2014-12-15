define([
        "knockout",
        "Scripts/models/pages/base/page",
        "Scripts/models/pages/admin/users/edit",
        "Scripts/models/controls/fields/dropdown",
        "Scripts/models/controls/fields/textField",
        "Scripts/models/controls/fields/intField",
        "Scripts/models/controls/fields/intListField",
        "Scripts/models/controls/fields/rangeSpinnerControl"
], function (ko, pageBase, editUserPage, dropdown, textField, intField, intListField) {

    function profilePage() {
        var self = new editUserPage(parent);

        self.controllerName = "Users";

        var loadBase = self.load;
        self.load = function (params) {
            $.ajax({
                url: "/Users/UserId",
                type: "POST",
                dataType: "JSON",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({}),
                success: function (json) {
                    params.id = json.Id;
                    loadBase(params);
                    console.log("profile page loaded");
                    self.refreshImage();
                }
            });
        }

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

        self.cancel = function () {
            self.clearErrors();
            alert("successfully saved");
        }

        return self;
    }

    return profilePage;

});