define([
        "knockout",
        "Scripts/models/pages/base/page"
], function (ko, pageBase) {

    function newPageBase(editPage) {
        var self = editPage;
        self.loadEntity = function () {
        };
        return self;
    }

    return newPageBase;

});