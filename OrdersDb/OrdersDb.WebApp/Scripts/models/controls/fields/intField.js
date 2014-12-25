define([
        "knockout",
        "./textField"
], function (ko, textField) {

    function intField(title, value) {
        var self = new textField(title, value, CommonResources.Please_Enter_The_Number);
        return self;
    }

    return intField;

});