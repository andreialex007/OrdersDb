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

        self.EDIT_TITLE(CommonResources.Edit_Employee);
        self.NEW_TITLE(CommonResources.Create_Employee);

        self.fields.FirstName = new textField(EntitiesResources.Employee_FirstName, "", EntitiesResources.Employee_FirstName + CommonResources._Required_);
        self.fields.LastName = new textField(EntitiesResources.Employee_LastName, "", EntitiesResources.Employee_LastName + CommonResources._Required_);
        self.fields.Patronymic = new textField(EntitiesResources.Employee_Patronymic, "", EntitiesResources.Employee_Patronymic + CommonResources._Required_);
        self.fields.Position = new dropdown(EntitiesResources.Employee_Position, 0, [], CommonResources.Please_Enter_The_Position_From_DropDown);
        self.fields.Email = new textField(EntitiesResources.Employee_Email, "", EntitiesResources.Employee_Email + CommonResources._Required_);
        self.fields.SNILS = new textField(EntitiesResources.Employee_SNILS, "", EntitiesResources.Employee_SNILS + CommonResources._Required_);

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
            self.fields.Email.value(json.Email);
            self.fields.SNILS.value(json.SNILS);
        };

        self.toJSON = function () {
            var json = { Id: self.Id() };

            if (self.fields.Position.value()) {
                json.Position = {};
                json.Position.Id = self.fields.Position.value();
                json.PositionId = self.fields.Position.value();
                json.Position.Name = self.fields.Position.text();
            }


            json.FirstName = self.fields.FirstName.value();
            json.LastName = self.fields.LastName.value();
            json.Patronymic = self.fields.Patronymic.value();

            json.Email = self.fields.Email.value();
            json.SNILS = self.fields.SNILS.value();

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



