define([
        "knockout",
        "./textField"
], function (ko, textField) {

    function dropdown(title, value, avaliableValues, optionCaption) {
        var self = new textField(title, value, optionCaption);
        self.avaliableValues = ko.observableArray(avaliableValues || []);
        self.text = function () {
            var found = $.grep(self.avaliableValues(), function (element) {
                return element.Value == self.value();
            });
            if (found.length == 1)
                return found[0].Text;
            return "";
        };
        return self;
    }

    return dropdown;

});