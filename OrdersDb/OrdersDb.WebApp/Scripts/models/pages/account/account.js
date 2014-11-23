define([
        "knockout",
        "Scripts/models/pages/base/page",
        "Scripts/models/controls/fields/dropdown",
        "Scripts/models/controls/fields/textField",
        "Scripts/models/controls/fields/intField",
        "Scripts/models/controls/fields/intListField",
        "Scripts/models/controls/fields/rangeSpinnerControl"
], function (ko, pageBase, dropdown, textField, intField, intListField) {

    function loginPage() {
        var self = new pageBase(parent);
        self.fields = {};
        self.fields.Name = new textField("", "", "Username");
        self.fields.Password = new textField("", "", "Password");
        self.fieldsArr = function () {
            return $.map(self.fields, function (el) { return el; });
        };
        self.DEFAULT_VALIDATION_SUMMARY = "Login error.";
        self._validationSummary = ko.observable("");
        self.validationSummary = ko.computed({
            read: function () {
                return self._validationSummary() || self.DEFAULT_VALIDATION_SUMMARY;
            }
        });
        self.validationSummaryVisible = ko.computed({
            read: function () {
                return $.grep(self.fieldsArr(), function (element) {
                    return element.hasErrors() == true;
                }).length > 0 || self._validationSummary();
            }
        });
        
        self.rememberMe = ko.observable(false);
        self.login = function () {
            $.ajax({
                url: "/Account/Login",
                type: "POST",
                dataType: "JSON",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ name: self.fields.Name.value(), password: self.fields.Password.value(), isPersistent: self.rememberMe() }),
                success: function (json) {
                    self.showValidationResult(json);
                    if (json.Result == "ok") {
                        document.location.href = "/";
                    }
                }
            });
        };

        self.showValidationResult = function (json) {
            self.clearErrors();
            if (json.HasErrors) {
                self._validationSummary(json.ValidationSummary);
                $(json.Errors).each(function (index, element) {
                    self.fields[element.PropertyName].errorsText(element.ErrorMessage);
                });
            }
        };

        self.clearErrors = function () {
            $(self.fieldsArr()).each(function (index, element) {
                element.hasErrors(false);
            });
        };

        return self;
    }

    return loginPage;

});