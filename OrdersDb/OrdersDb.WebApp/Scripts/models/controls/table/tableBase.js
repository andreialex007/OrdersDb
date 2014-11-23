define([
        "knockout",
        "Scripts/models/common/modelBase"
], function (ko, modelBase) {

    function tableBase() {
        var self = new modelBase();

        return self;
    }

    return tableBase;

});