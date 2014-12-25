define([
        "knockout",
        "./textField"
], function (ko, textField) {

    function intListField(title, value) {
        var self = new textField(title, value, CommonResources.Numbers_List_CommaSeparated);
        self.mask = { type: 'Regex', options: { regex: '^[0-9,\,]*$' } };
        return self;
    }

    return intListField;

});