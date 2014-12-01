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

        self.fields.product.value.subscribe(function (value) {
            var productId = value;
            if (!productId)
                return;
            var selectedProduct = $.grep(self.fields.product.avaliableValues(), function (x) { return x.Id == productId; })[0];
            self.fields.itemPrice(selectedProduct.SellPrice);
            self.fields.totalPrice(self.fields.itemPrice() * self.fields.amount.value());
        });

        self.fields.amount.value.subscribe(function (value) {
            self.fields.totalPrice(self.fields.itemPrice() * value);
        });


        self.fields.itemPrice = ko.observable();
        self.fields.totalPrice = ko.observable();

        self.save = function () {
            self.onSave();
            self.hide();
        }

        var hideBase = self.hide;
        self.hide = function() {
            self.fields.product.value(0);
            self.fields.amount.value(1);
            hideBase();
        };

        self.onSave = function () { };

        return self;
    }

    return orderItemModal;

});