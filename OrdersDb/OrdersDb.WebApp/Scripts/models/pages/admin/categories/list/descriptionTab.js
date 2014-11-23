define([
        "knockout",
        "Scripts/models/pages/base/page",
        "Scripts/models/controls/fields/textField",
        "Scripts/models/controls/fields/intField",
        "Scripts/models/controls/fields/intListField",
        "Scripts/models/controls/fields/rangeSpinnerControl"
], function (ko, pageBase, textField) {

    function descriptionTab(parent) {
        var self = new pageBase(parent);
        self.visible = ko.observable(false);
        self.id = 0;
        self.name = new textField();
        self.description = new textField();
        self.save = function () {
            console.log("save");
        };
        self.reset = function () {
            console.log("reset");
        };
        self.fromJSON = function (json) {
            self.id = json.Id;
            self.name.value(json.Name);
            self.description.value(json.Description);
        };
        self.toJSON = function () {
            var json = {};
            json.Name = self.name.value();
            json.Description = self.description.value();
            json.Id = self.id;
            return json;
        };
        return self;
    }

    return descriptionTab;

});