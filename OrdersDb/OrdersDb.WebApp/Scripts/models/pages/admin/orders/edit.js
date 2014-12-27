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
        self.EDIT_TITLE(CommonResources.Edit_Order);
        self.NEW_TITLE(CommonResources.Create_Order);

        self.CodeId = null;
        self.fields.Code = new textField(CommonResources.Code, "", CommonResources.Code_Will_be_generated_automatically, "", true);
        self.fields.Client = new dropdown(EntitiesResources.Client, 0, [], CommonResources.Please_select_a_client_from_dropdown);

        self.table = new table([
            new column("Id", "#", "50px"),
            new column("Product.Name", EntitiesResources.Product_Name),
            new column("ProductBuyPrice", EntitiesResources.Product_BuyPrice, "100px"),
            new column("ProductSellPrice", EntitiesResources.Product_SellPrice, "100px"),
            new column("Amount", CommonResources.Amount, "100px"),
            new column("BuyPrice", CommonResources.Total_Buy_Price, "100px"),
            new column("SellPrice", CommonResources.Total_Sell_Price, "100px")
        ]);

        self.orderItemModal = new orderItemModal(self);
        self.orderItemModal.onSave = function () {
            var productItemId = self.orderItemModal.productItemId;
            var productId = self.orderItemModal.fields.product.value();
            var product = $.grep(self.orderItemModal.fields.product.avaliableValues(), function (x) { return x.Id == productId; })[0];
            var amount = self.orderItemModal.fields.amount.value();

            if (self.orderItemModal.rowPosition != null) {
                var oldRow = self.table.rows()[self.orderItemModal.rowPosition];
                oldRow.cells()[0].value(productId);
                oldRow.cells()[1].value(product.Name);
                oldRow.cells()[2].value(product.SellPrice);
                oldRow.cells()[3].value(product.BuyPrice);
                oldRow.cells()[4].value(amount);
                oldRow.cells()[5].value(amount * product.SellPrice);
                oldRow.cells()[6].value(amount * product.BuyPrice);
            } else {
                var rowItem = new row(self.table, productItemId, [
                    new cell(productId),
                    new cell(product.Name),
                    new cell(product.SellPrice),
                    new cell(product.BuyPrice),
                    new cell(amount),
                    new cell(amount * product.SellPrice),
                    new cell(amount * product.BuyPrice)
                ]);
                var newRows = self.table.rows();
                newRows.push(rowItem);
                self.table.rows(newRows);
            }
        }

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

        self.loadEntityData = function (params) {
            self.getById(self.Id(), function (json) {
                self.fromJSON(json);
            });
        };

        self.anyChecked = ko.computed({
            read: function () {
                return $.grep(self.table.rows(), function (el) {
                    return el.isChecked();
                }).length != 0;
            }
        });

        self.isEmptyTable = ko.computed({
            read: function () {
                return self.table.rows().length == 0;
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
            self.orderItemModal.rowPosition = null;
            self.orderItemModal.show();
        }

        self.deleteSelected = function () {
            var newItems = $.grep(self.table.rows(), function (el) {
                return !el.isChecked();
            });
            self.table.rows(newItems);
        }

        self.clearList = function () {
            self.table.rows.removeAll();
        }

        self.table.onEdit = function (item) {
            var productItemId = item.id();
            var productId = item.cells()[0].value();
            self.orderItemModal.rowPosition = self.table.rows().indexOf(item);
            self.orderItemModal.fields.product.value(productId);
            self.orderItemModal.fields.amount.value(item.cells()[4].value());
            self.orderItemModal.productItemId = productItemId;
            self.orderItemModal.show();
        }

        self.table.onDelete = function (item) {
            self.table.rows.remove(item);
        }

        self.fromJSON = function (json) {
            self.fields.Code.value(json.Code);
            self.CodeId = json.CodeId;
            self.orderItemModal.fields.product.avaliableValues(json.Products);
            self.fields.Client.avaliableValues(utils.idNameToTextValue(json.Clients));
            self.fields.Client.value(json.ClientId);

            var orderItems = json.OrderItems;
            var rows = $(orderItems).map(function (i, x) {
                return new row(self.table, x.Id, [
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
            if (self.CodeId) {
                json.CodeId = self.CodeId;
                json.Code = {
                    Id: self.CodeId,
                    Value: self.fields.Code.value()
                };
            }
            json.ClientId = self.fields.Client.value();
            json.OrderItems = $.map(self.table.rows(), function (x) {
                return {
                    Id: x.id(),
                    ProductId: x.cells()[0].value(),
                    Product: {
                        Id: x.cells()[0].value(),
                        Name: x.cells()[1].value()
                    },
                    Amount: x.cells()[4].value(),
                    OrderId: self.Id()
                };
            });

            return json;
        };
        return self;
    }
    return cityPage;
});



