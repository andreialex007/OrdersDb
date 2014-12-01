define([
        "knockout",
        "./modalbase",
         "Scripts/models/controls/fields/spinner",
         "Scripts/models/controls/fields/dropdown"
], function (ko, modalbase, spinner, dropdown) {

    function orderItemModal(parent) {
        var self = new modalbase(parent);

        self.productItemId = 0;
        self.fields = {};
        self.fields.product = new dropdown("Товар", 0, [], "Выберите товар из выпадающего списка");
        self.fields.amount = new spinner("Количество", 1, 1, 1, 2000);

        self.fields.itemPrice = ko.computed({
            read: function () {
                if (!self.fields.product.value())
                    return 0;
                return parseFloat(self.fields.product.value());
            }
        });

        self.fields.totalPrice = ko.computed({
            read: function () {
                if (!self.fields.product.value())
                    return 0;
                return parseFloat(self.fields.product.value()) * parseFloat(self.fields.amount.value());
            }
        });

        var showBase = self.show;
        self.show = function () {
            if (self.productItemId == 0) {
                self.fields.product.value(0);
                self.fields.amount.value(1);
            }
            showBase();
        }

        return self;
    }

    return orderItemModal;

});