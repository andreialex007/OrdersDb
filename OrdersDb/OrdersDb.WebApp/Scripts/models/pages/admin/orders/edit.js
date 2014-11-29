define(["knockout",
        "knockout.mapping",
        "sammy",
        "Scripts/models/pages/base/edit",
        "Scripts/models/controls/dropdowns/region/dropdown",
         "Scripts/models/controls/fields/textField",
         "Scripts/models/controls/fields/spinner",
          "Scripts/models/controls/table/table",
          "Scripts/models/controls/table/column",
          "Scripts/models/controls/table/row",
          "Scripts/models/controls/table/cell",
          "Scripts/models/controls/fields/dropdown"
], function (ko, mapping, sammy, editPageBase, regionDropDown, textField, spinner, table, column, row, cell, dropdown) {
    function cityPage(parent) {
        var self = new editPageBase(parent);
        self.EDIT_TITLE("Редактирование заказа");
        self.NEW_TITLE("Создание заказа");

        self.fields.Code = new textField("Код заказа", "", "Код будет сгенерирован автоматически", "", true);
        self.fields.Client = new dropdown("Заказчик", 0, [], "Выберите заказчика из выпадающего списка");

        self.table = new table([
            new column("Product.Name", "Товар"),
            new column("BuyPrice", "Цена продажи"),
            new column("SellPrice", "Цена покупки"),
            new column("Amount", "Количество")
        ]);

        self.anyChecked = ko.computed({
            read: function () {
                return $.grep(self.table.rows(), function (el) {
                    return el.isChecked();
                }).length != 0;
            }
        });
        self.displayed = ko.computed({
            read: function () {
                return self.table.rows().length;
            }
        });
        self.checked = ko.computed({
            read: function () {
                return $.grep(self.table.rows(), function (el) {
                    return el.isChecked();
                }).length;
            }
        });


        self.fromJSON = function (json) {
            self.fields.Code.value(json.Code);

            self.fields.Client.avaliableValues(utils.idNameToTextValue(json.Clients));
            self.fields.Client.value(json.ClientId);

            var orderItems = json.OrderItems;
            var rows = $(orderItems).map(function (i, x) {
                return new row(self.table, x.ProductId, [
                    new cell(x.ProductName),
                    new cell(x.SellPrice),
                    new cell(x.BuyPrice),
                    new cell(x.Amount)
                ]);
            });
            self.table.rows(rows);
        };
        self.toJSON = function () {
            var json = { Id: self.Id() };

            return json;
        };
        return self;
    }
    return cityPage;
});



