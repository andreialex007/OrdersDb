define([
        "knockout",
        "Scripts/models/pages/base/page",
        "Scripts/models/controls/table/table"
], function (ko, pageBase, table) {

    function listPageBase(parent) {
        var self = new pageBase(parent);
        self.table = new table();

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

        var loadBase = self.load;
        self.load = function () {
            loadBase();
            self.table.onLoad();
            self.table.onDelete = function (row) {
                $.ajax({
                    url: self.controllerName + "/delete",
                    type: "POST",
                    dataType: "JSON",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ id: row.id() }),
                    success: function (json) {
                        loadBase();
                        self.table.onLoad();
                    }
                });
            }
        };

        self.total = ko.observable(0);

        self.getSearchParams = function () {

        };

        self.table.onScrollLoad = function () {

            if (self.total() == self.displayed()) {
                self.table.onScrollLoadCompleted();
                return;
            }

            var params = self.getSearchParams();
            params.OrderBy = self.table.sortColumn().id();
            params.IsAsc = !self.table.isDesc();
            params.Take = self.table.takeAppend;
            params.Skip = self.table.rows().length;

            $.ajax({
                url: self.searchUrl(),
                type: "POST",
                dataType: "JSON",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ parameters: params, append: true }),
                success: function (json) {
                    console.time('someFunction');
                    var rows = [];
                    for (var i = 0; i < json.List.length; i++) {
                        rows.push(self.toRow(json.List[i]));
                    }
                    self.table.rows(self.table.rows().concat(rows));
                    console.timeEnd('someFunction');
                    self.table.onScrollLoadCompleted();
                }
            });
        };

        self.table.onLoad = function () {
            var params = self.getSearchParams();
            params.OrderBy = self.table.sortColumn().id();
            params.IsAsc = !self.table.isDesc();
            params.Take = self.table.take;
            params.Skip = 0;

            $.ajax({
                url: self.searchUrl(),
                type: "POST",
                dataType: "JSON",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(params),
                success: function (json) {
                    var rows = [];
                    for (var i = 0; i < json.List.length; i++) {
                        rows.push(self.toRow(json.List[i]));
                    }
                    self.table.rows(rows);
                    self.total(json.Total);
                    self.table.onLoadCompleted();
                }
            });
        };

        self.timeout = null;
        self.initialized = false;
        self.previousSearchParams = "";
        self.init = function () {
            self.onSearch = ko.computed({
                read: function () {
                    clearTimeout(self.timeout);
                    self.timeout = setTimeout(function () {
                        var serializedSearchParams = ko.toJSON(self.getSearchParams());

                        if (!self.initialized) {
                            self.previousSearchParams = serializedSearchParams;
                            self.initialized = true;
                            return "";
                        }

                        if (serializedSearchParams != self.previousSearchParams) {
                            self.previousSearchParams = serializedSearchParams;
                            self.table.load();
                        }
                    }, 500);

                    return self.getSearchParams();
                }
            });
        };

        self.add = function () {
            window.location.hash = "#/" + self.controllerName + "/new";
        };

        self.deleteItems = function () {
            debugger;
            var toDelete = $.grep(self.table.rows(), function (x) { return x.isChecked() == true; });
            var idsToDelete = $.map(toDelete, function (x) { return x.id(); });

            $.ajax({
                url: self.controllerName + "/DeleteMany",
                type: "POST",
                dataType: "JSON",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ ids: idsToDelete }),
                success: function (json) {
                    loadBase();
                    self.table.onLoad();
                }
            });

        }

        return self;
    }

    return listPageBase;

});
