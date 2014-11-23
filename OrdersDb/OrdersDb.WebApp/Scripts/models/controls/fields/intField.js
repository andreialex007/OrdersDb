define([
        "knockout",
        "./textField"
], function (ko, textField) {

    function intField(title, value) {
        var self = new textField(title, value, "Введите число");
        return self;
    }

    return intField;

});