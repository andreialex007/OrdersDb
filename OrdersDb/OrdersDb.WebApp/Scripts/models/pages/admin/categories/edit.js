define([
        "knockout",
        "knockout.mapping",
        "sammy",
        "Scripts/models/pages/base/edit"
], function (ko, mapping, sammy, editPageBase) {

    function editPage(parent) {
        var self = new editPageBase(parent);

        var loadBase = self.load;
        self.load = function () {
            var b = self;
            console.log("edit element");
            loadBase();
        };
        return self;
    }

    return editPage;

});



