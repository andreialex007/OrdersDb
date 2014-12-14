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
                }
            });
        }

        self.cancel = function () {
            self.clearErrors();
            alert("successfully saved");
        }

        return self;
    }

    return profilePage;

});