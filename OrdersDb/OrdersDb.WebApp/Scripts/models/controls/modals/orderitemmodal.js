define([
        "knockout",
        "./modalbase",
         "Scripts/models/controls/fields/spinner",
         "Scripts/models/controls/fields/dropdown"
], function (ko, modalbase, spinner, dropdown) {

    function orderItemModal(parent) {
        var self = new modalbase(parent);
        self.fields = {};
        self.fields.product = new dropdown("Товар", 0, [], "Выберите товар из выпадающего списка");
        self.fields.amount = new spinner("Количество", 1, 1, 1, 2000);

        return self;
    }

    return orderItemModal;

});