define([
        "knockout",
        "Scripts/models/common/modelBase"
], function (ko, modelBase) {

    function modalBase(parent) {
        var self = new modelBase(parent);
        self.visible(false);
        self.hide = function () {
            self.visible(false);
        }
        self.show = function () {
            self.visible(true);
        }

        return self;
    }

    return modalBase;

});