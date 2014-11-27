define([
        "knockout",
        "./node"
], function (ko, node) {

    function tree() {
        var self = {};

        self.nodes = ko.observableArray([]);

        self._textFilter = ko.observable("");
        self.textFilter = ko.computed({
            read: function () {
                return self._textFilter();
            },
            write: function (value) {
                self._textFilter(value);
            }
        });

        self.removeAllNodes = function () {
            self.nodes.removeAll();
        }

        self.onSelect = function (item) {

        };

        self.deselectAll = function () {
            $(self.nodes()).each(function (index, element) {
                element.deselectAll();
            });
        };

        self.onOpen = function () {

        };

        self.getCagetoryById = function (id) {
            for (var i = 0; i < self.nodes().length ; i++) {
                var currentNode = self.nodes()[i];
                if (currentNode.id() == id)
                    return currentNode;

                var childCategory = currentNode.getCagetoryById(id);
                if (childCategory)
                    return childCategory;
            }
            return null;
        };

        self.remove = function (item) {
            self.nodes.remove(item);
            self.onRemove(item);
        };
        self.onRemove = function () {

        };
        self.add = function () {

        };
        self.added = function (id, name) {
            self.nodes.unshift(new node(self, id, name));
        };
        return self;
    }
    return tree;
});