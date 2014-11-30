define([
        "knockout",
        "./modalbase"
], function (ko, modalbase) {

    function orderItemModal(parent) {
        var self = new modalbase(parent);

        return self;
    }

    return orderItemModal;

});