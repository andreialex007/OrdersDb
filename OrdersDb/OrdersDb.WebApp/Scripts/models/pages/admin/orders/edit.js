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
          "Scripts/models/controls/fields/dropdown",
          "Scripts/models/controls/modals/orderitemmodal"
], function (ko, mapping, sammy, editPageBase, regionDropDown, textField, spinner, table, column, row, cell, dropdown, orderItemModal) {
    function cityPage(parent) {
        var self = new editPageBase(parent);
        self.EDIT_TITLE("Редактирование заказа");
        self.NEW_TITLE("Создание заказа");

        self.fields.Code = new textField("Код заказа", "", "Код будет сгенерирован автоматически", "", true);
        self.fields.Client = new dropdown("Заказчик", 0, [], "Выберите заказчика из выпадающего списка");

        self.table = new table([
            new column("Id", "#", "50px"),
            new column("Product.Name", "Товар"),
            new column("ProductBuyPrice", "Цена продажи товара", "100px"),
            new column("ProductSellPrice", "Цена покупки товара", "100px"),
            new column("Amount", "Количество", "100px"),
            new column("BuyPrice", "Общая цена продажи", "100px"),
            new column("SellPrice", "Общая цена покупки", "100px")
        ]);

        self.orderItemModal = new orderItemModal(self);

        self.table.load = function () {
            var sortColumn = self.table.sortColumn();
            var isDesc = self.table.isDesc();
            var sortColumnIndex = self.table.columns().indexOf(sortColumn);
            var rows = self.table.rows();
            var sortFunction = function (x) { return x.cells()[sortColumnIndex].value(); };
            var rowsOrdered = isDesc
                ? Enumerable.From(rows).OrderBy(sortFunction).ToArray()
                : Enumerable.From(rows).OrderByDescending(sortFunction).ToArray();
            self.table.rows(rowsOrdered);
        };

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

        self.totalBuyPrice = ko.computed({
            read: function () {
                return Enumerable.From(self.table.rows()).Sum(function (x) { return x.cells()[5].value(); });
            }
        });


        self.totalSellPrice = ko.computed({
            read: function () {
                return Enumerable.From(self.table.rows()).Sum(function (x) { return x.cells()[6].value(); });
            }
        });

        self.checked = ko.computed({
            read: function () {
                return $.grep(self.table.rows(), function (el) {
                    return el.isChecked();
                }).length;
            }
        });

        self.addNew = function () {
            console.log("addNew");
            self.orderItemModal.show();
        }

        self.deleteSelected = function () {
            console.log("deleteSelected");
            self.orderItemModal.hide();
        }

        self.clearList = function () {
            console.log("clearList");
        }


        self.fromJSON = function (json) {
            self.fields.Code.value(json.Code);

            self.fields.Client.avaliableValues(utils.idNameToTextValue(json.Clients));
            self.fields.Client.value(json.ClientId);

            var orderItems = json.OrderItems;
            var rows = $(orderItems).map(function (i, x) {
                return new row(self.table, x.ProductId, [
                    new cell(x.ProductId),
                    new cell(x.ProductName),
                    new cell(x.ProductSellPrice),
                    new cell(x.ProductBuyPrice),
                    new cell(x.Amount),
                    new cell(x.SellPrice),
                    new cell(x.BuyPrice)
                ]);
            });
            self.table.rows(rows);

            self.table.sortColumn(self.table.columns()[0]);
            self.table.load();
        };
        self.toJSON = function () {
            var json = { Id: self.Id() };

            return json;
        };
        return self;
    }
    return cityPage;
});



