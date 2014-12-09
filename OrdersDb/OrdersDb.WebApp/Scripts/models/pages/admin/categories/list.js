define([
        "knockout",
        "knockout.mapping",
        "sammy",
        "Scripts/models/pages/base/page",
        "Scripts/models/controls/tree/tree",
        "Scripts/models/controls/tree/node",
        "./list/descriptionTab",
        "./list/imageTab",
        "./list/informationTab"
], function (ko, mapping, sammy, pageBase, tree, node, descriptionTab, imageTab, informationTab) {

    function listPage(parent) {
        var self = new pageBase(parent);
        self.title("Список категорий");

        self.categoriesLoading = ko.observable(false);
        self.searchText = ko.observable("");


        self.tree = new tree();
        self.previouslyOpenedTabIndex = 0;
        self.descriptionTab = new descriptionTab(self);
        self.imageTab = new imageTab(self);
        self.informationTab = new informationTab(self);
        self.tabs = [self.descriptionTab, self.imageTab, self.informationTab];
        self.tree.onSelect = function (item) {
            self.getById(item.id(), function (json) {
                $(self.tabs).each(function (index, element) {
                    element.fromJSON(json);
                });
                self.tabclick(self.tabs[self.previouslyOpenedTabIndex]);
            });
        };

        self.searchTextThrottled = ko.computed(self.searchText).extend({ throttle: 400 });
        self.searchTextThrottled.subscribe(function (value) {
            self.tree.textFilter(value);
            self.load();
        });

        var loadBase = self.load;
        self.load = function () {
            loadBase();

            self.categoriesLoading(true);
            self.tree.removeAllNodes();

            $.ajax({
                url: self.controllerName + "/Search/",
                type: "POST",
                dataType: "JSON",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ ParentCategoryFilterEnabled: self.searchText() == "", OrderBy: "Name", Name: self.searchText() }),
                success: function (json) {
                    var nodes = $.map(json.List, function (element, index) {
                        return new node(self.tree, element.Id, element.Name, [], element.CategoriesAmount);
                    });

                    self.categoriesLoading(false);
                    self.tree.nodes(nodes);
                    var firstNode = self.tree.nodes()[0];
                    if (firstNode)
                        firstNode.select();
                }
            });
        };

        self.isEmptyResult = ko.computed({
            read: function () {
                return self.tree.nodes().length == 0;
            }
        });

        self.tabclick = function (tab) {
            $(self.tabs).each(function (index, element) {
                element.visible(false);
            });
            tab.visible(true);
            self.previouslyOpenedTabIndex = self.tabs.indexOf(tab);
        };

        self.descriptionTab.save = function () {
            var json = self.descriptionTab.toJSON();
            $.ajax({
                url: self.controllerName + "/SaveInfo/",
                type: "POST",
                dataType: "JSON",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(json),
                success: function (data) {
                    var category = self.tree.getCagetoryById(json.Id);
                    if (category) {
                        category.name(json.Name);
                    }
                }
            });
        };

        self.tree.add = function (parentNode) {
            if (parentNode == self.tree)
                parentNode = null;
            debugger;
            var postData = {};
            if (parentNode) {
                postData.parentCategoryId = parentNode.id();
            }
            $.ajax({
                url: self.controllerName + "/AddNewCategory/",
                type: "POST",
                dataType: "JSON",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(postData),
                success: function (data) {
                    debugger;
                    if (parentNode) {
                        parentNode.children.unshift(new node(self.tree, data.Id, data.Name));
                    } else {
                        self.tree.added(data.Id, data.Name);
                    }
                }
            });
        };

        self.tree.onOpen = function (element) {
            $.ajax({
                url: self.controllerName + "/Search/",
                type: "POST",
                dataType: "JSON",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ ParentCategoryFilterEnabled: true, ParentCategoryId: element.id() }),
                success: function (json) {
                    var nodes = $.map(json.List, function (element, index) {
                        return new node(self.tree, element.Id, element.Name, [], element.CategoriesAmount);
                    });
                    element.children.removeAll();
                    element.children(nodes);
                }
            });
        };

        self.tree.onRemove = function (itemToRemove) {
            var id = itemToRemove.id();
            $.ajax({
                url: self.controllerName + "/Delete/",
                type: "POST",
                dataType: "JSON",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (json) {
                    debugger;
                }
            });
        };

        return self;
    }

    return listPage;
});