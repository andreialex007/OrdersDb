define([
        "knockout",
        "knockout.mapping",
        "sammy",
        "Scripts/models/pages/base/namedList",
        "Scripts/models/controls/table/table",
        "Scripts/models/controls/table/column",
        "Scripts/models/controls/table/row",
        "Scripts/models/controls/table/cell",
        "Scripts/models/controls/fields/dropdown",
        "Scripts/models/controls/fields/textField",
        "Scripts/models/controls/fields/intField",
        "Scripts/models/controls/fields/intListField"
], function (ko, mapping, sammy, listPageBase) {

    function listPage(parent) {
        var self = new listPageBase(parent);
        self.title("Список ролей");

        self.init();
        return self;
    }

    return listPage;
});