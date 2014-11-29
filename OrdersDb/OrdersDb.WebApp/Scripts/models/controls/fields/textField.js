define(["knockout"], function (ko) {

    function textField(title, value, placeholder, mask, disabled) {
        var self = {};
        self.title = ko.observable(title);
        self.value = ko.observable(value);
        self.valueThrottle = ko.computed(self.value).extend({ throttle: 700 });
        self.disabled = ko.observable(disabled || false);
        self.placeholder = ko.observable(placeholder || "Введите текст");
        self.errorsText = ko.observable("");
        self.mask = mask;
        self.change = function () {
            console.log("changed");
            return true;
        };
        self.appendError = function (text) {
            var newErrorText = self.errorsText() + ", " + text;
            self.errorsText(trim(newErrorText, ", "));
        };
        self.hasErrors = ko.computed({
            read: function () {
                if (self.errorsText())
                    return true;
                return false;
            },
            write: function (val) {
                if (val == false)
                    self.errorsText("");
            }
        });
        return self;
    }

    return textField;

});