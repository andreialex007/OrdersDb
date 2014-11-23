define([
        "knockout",
        "Scripts/models/common/modelBase"
], function (ko, modelBase) {

    function searchArea(parent) {
        var self = new modelBase(parent);

        return self;
    }

    return searchArea;

});