define([
        "knockout",
        "./modalbase",
         "Scripts/models/controls/fields/spinner",
         "Scripts/models/controls/fields/dropdown"
], function (ko, modalbase, spinner, dropdown) {

    function orderItemModal(parent) {
        var self = new modalbase(parent);

        self.EDIT_TITLE = "Редактирование товарной позиции";
        self.NEW_TITLE = "Добавление товарной позиции";
        self.title = ko.observable("");

        self.rowPosition = null;
        self.productItemId = 0;
        self.fields = { };
        self.fields.product = new dropdown("Товар", 0, [], "Выберите товар из выпадающего списка");
        self.fields.amount = new spinner("Количество", 1, 1, 1, 2000);

        self.updatePrices = function (value) {
            var productId = value;
            if (!productId) {
                self.fields.itemPrice(0);
                self.fields.totalPrice(0);
                return;
            }
            var selectedProduct = $.grep(self.fields.product.avaliableValues(), function (x) { return x.Id == productId; })[0];
            self.fields.itemPrice(selectedProduct.SellPrice || 0);
            self.fields.totalPrice((self.fields.itemPrice() * self.fields.amount.value()) || 0);
        }

        self.fields.product.value.subscribe(function (value) {
            self.updatePrices(value);
        });

        self.fields.amount.value.subscribe(function (value) {
            self.fields.totalPrice((self.fields.itemPrice() * value) || 0);
        });


        self.fields.itemPrice = ko.observable(0);
        self.fields.totalPrice = ko.observable(0);

        var showBase = self.show;
        self.show = function () {
            self.updatePrices(self.fields.product.value());
            showBase();
        }

        
        var hideBase = self.hide;
        self.hide = function () {
            self.fields.product.value("");
            self.fields.amount.value(1);
            self.fields.itemPrice(0);
            self.fields.totalPrice(0);
            self.productItemId = 0;
            hideBase();
        };

        self.save = function () {
            self.onSave();
            self.hide();
        }


        self.onSave = function () { };

        return self;
    }

    return orderItemModal;

});