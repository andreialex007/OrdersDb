define([
        "knockout",
        "Scripts/models/pages/base/page",
        "Scripts/models/controls/fields/textField",
        "Scripts/models/controls/fields/intField",
        "Scripts/models/controls/fields/intListField",
        "Scripts/models/controls/fields/rangeSpinnerControl"
], function (ko, pageBase, textField) {

    function imageTab(parent) {
        var self = new pageBase(parent);
        self.id = 0;
        self.visible = ko.observable(false);
        self.imageurlPreview = ko.observable("");
        self.imageurlFull = ko.observable("");
        self.hasImage = ko.observable(false);

        self.upload = function () {
            console.log("upload");
        };
        self.remove = function () {
            console.log("delete");
        };
        self.beforeupload = function (e, data) {
            data.formData = { id: self.id };
        };
        self.done = function(e, data) {
            self.hasImage(true);
            self.refreshImage();
        };
        self.fromJSON = function (json) {
            self.id = json.Id;
            self.hasImage(json.HasImage);
            if (self.hasImage()) {
                self.refreshImage();
            }
        };
        self.refreshImage = function () {
            self.imageurlPreview(self.parent.controllerName + "/ShowImagePreview/" + self.id + "?t=" + Math.random());
            self.imageurlFull(self.parent.controllerName + "/ShowImageFull/" + self.id + "?t=" + Math.random());
        };
        self.unload = function () {

        };
        return self;
    }

    return imageTab;

});