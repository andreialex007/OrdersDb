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
        self.EDIT_TITLE("Редактирование роли");
        self.NEW_TITLE("Создание роли");

        self.ACCESS_TYPE_READ = 1;
        self.ACCESS_TYPE_ADD = 2;
        self.ACCESS_TYPE_UPDATE = 3;
        self.ACCESS_TYPE_DELETE = 4;

        self.permissions = ko.observableArray([]);
        self.checkedItem = function (permissionGroup, permission) {
            if (permission.AccessType() == self.ACCESS_TYPE_READ && permission.Checked() == true) {
                $(permissionGroup.Permissions()).each(function (index, element) {
                    element.Checked(false);
                });
            }

            if (permission.AccessType() != self.ACCESS_TYPE_READ && permission.Checked() == false) {
                permissionGroup.Permissions()[0].Checked(true);
            }

            console.log('checkedItem');
        };

        self.fields.Name = new textField("Имя роли", "", "Имя роли (обязательно)");

        self.fromJSON = function (json) {
            self.fields.Name.value(json.roleDto.Name);
            self.permissions(mapping.fromJS(json.permissionGroups)());
            self.readonly(json.IsReadOnly || false);
        };

        self.save = function () {
            var json = self.toJSON();

            $.ajax({
                url: "/" + self.controllerName + (json.role.Id === 0 ? "/Add" : "/Update"),
                dataType: "json",
                type: "POST",
                contentType: "application/json",
                data: JSON.stringify(json)
            }).done(function (data) {
                if (data.Result == "ok") {
                    console.log("saved");
                    self.cancel();
                } else {
                    self.onError(data);
                }
            });
        };

        self.loadEntityData = function (params) {
            self.getById(self.Id(), function (json) {
                self.fromJSON(json);
            });
        };

        self.toJSON = function () {
            debugger;
            var role = { Id: self.Id() };
            role.Name = self.fields.Name.value();

            var permissions = [];
            var permissionsGroups = mapping.toJS(self.permissions);

            $(permissionsGroups).each(function (index, element) {
                permissions = permissions.concat(element.Permissions);
            });

            return { role: role, permissions: permissions };
        };
        return self;
    }
    return cityPage;
});



