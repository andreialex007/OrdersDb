define([
        "knockout"
], function (ko) {

    function node(root, id, name, children, childrenAmount) {
        var self = {};
        self.root = root;
        self.id = ko.observable(id);
        self.name = ko.observable(name);
        self.isOpen = ko.observable(false);
        self.hovered = ko.observable(false);
        self.selected = ko.observable(false);
        self.childrenAmount = ko.observable(childrenAmount || 0);
        self.toggle = function () {
            self.isOpen(!self.isOpen());
            if (self.isOpen()) {
                self.root.onOpen(self);
            }
        };
        self.add = function () {
            self.isOpen(true);
            self.root.add(self);
        };
        self.remove = function (item) {
            self.children.remove(item);
            self.root.onRemove(item);
        };
        self.select = function () {
            self.root.deselectAll();
            self.selected(true);
            self.root.onSelect(self);
        };
        self.deselectAll = function () {
            self.selected(false);
            $(self.children()).each(function (index, element) {
                element.deselectAll();
            });
        };
        self.getCagetoryById = function (targetId) {
            for (var i = 0; i < self.children().length ; i++) {
                var currentNode = self.children()[i];
                if (currentNode.id() == targetId)
                    return currentNode;

                var childCategory = currentNode.getCagetoryById(targetId);
                if (childCategory)
                    return childCategory;
            }
            return null;
        };
        self.mouseEnter = function () {
            self.hovered(true);
        };
        self.mouseLeave = function () {
            self.hovered(false);
        };

        self.children = ko.observableArray(children || []);
        return self;
    }
    return node;
});