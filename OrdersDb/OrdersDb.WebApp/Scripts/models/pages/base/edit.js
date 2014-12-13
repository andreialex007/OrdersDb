define([
        "knockout",
        "Scripts/models/pages/base/page"
], function (ko, pageBase) {

    function editPageBase(parent) {
        var self = new pageBase(parent);
        var loadBase = self.load;

        self.EDIT_TITLE = ko.observable("");
        self.NEW_TITLE = ko.observable("");
        self.Id = ko.observable(0);
        self.title = ko.computed({
            read: function () {
                return self.Id() == 0 ? self.NEW_TITLE() : self.EDIT_TITLE();
            }
        });

        self.otherErrors = ko.observable("");
        self.otherErrorsVisible = ko.observable(false);

        self.fields = {};
        self.fieldsArr = function () {
            return $.map(self.fields, function (value, index) {
                return value;
            });
        };
        self.load = function (params) {
            self.clearErrors();

            self.Id(params.id == "new" ? 0 : params.id);
            if (params.id == "new") {
                self.loadEntityData(params);
            } else {
                self.loadEntity(params);
            }

            loadBase();
        };

        self.loadEntity = function (params) {
            self.getById(self.Id(), function (json) {
                self.fromJSON(json);
            });
        };

        self.loadEntityData = function (params) {

        };

        self.fromJSON = function () {

        };

        self.toJSON = function () {

        };

        self.save = function () {
            var json = self.toJSON();

            $.ajax({
                url: "/" + self.controllerName + (json.Id === 0 ? "/Add" : "/Update"),
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

        self.clearErrors = function () {
            $(self.fieldsArr()).each(function (index, element) {
                if (element.hasErrors)
                    element.hasErrors(false);

                if (element.errorsText)
                    element.errorsText("");
            });
            self.hasErrors(false);
            self.otherErrorsVisible(false);
        };

        self.onError = function (json) {
            self.clearErrors();

            $(json.Errors).each(function (index, element) {
                if (self.fields[element.PropertyName])
                    self.fields[element.PropertyName].appendError(element.ErrorMessage);
            });
            var otherErrors = $.map($.grep(json.Errors, function (x) { return !self.fields[x.PropertyName]; }), function (a) { return a.ErrorMessage; }).join();
            if (otherErrors) {
                self.otherErrors(otherErrors);
                self.otherErrorsVisible(true);
            }

            self.checkErrors();
        };

        self.hasErrors = ko.observable(false);

        self.checkErrors = function () {
            var hasErrors = false;

            var hasAnyErrors = $.grep(self.fieldsArr(), function (x) {
                if (x.hasErrors && x.hasErrors() == true)
                    return true;
                return false;
            }).length > 0;

            if (hasAnyErrors)
                hasErrors = true;

            if (self.otherErrors())
                hasErrors = true;

            self.hasErrors(hasErrors);
        }

        self.cancel = function () {
            window.location.hash = "#/" + self.controllerName;
        };

        var unloadBase = self.unload;
        self.unload = function () {
            unloadBase();
            self.unbindEvents();
        };

        self.unbindEvents = function () {
        };

        return self;
    }

    return editPageBase;

});