define([
        "knockout",
        "./node"
], function (ko, node) {

    function tree() {
        var self = {};

        self._nodes = ko.observableArray([]);

        self._textFilter = ko.observable("");
        self.textFilter = ko.computed({
            read: function () {
                return self._textFilter();
            },
            write: function (value) {
                $(self.nodes()).each(function (i, x) { x.isvisible(true); });
                var notFound = $.grep(self.nodes(), function (x) { return x.name().toLowerCase().indexOf(value.toLowerCase()) == -1; });
                $(notFound).each(function (i, x) { x.isvisible(false); });
                self._textFilter(value);
            }
        });
        self.nodes = ko.observableArray([]);
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