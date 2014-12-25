define([
        "knockout",
        "Scripts/models/common/modelBase"
], function (ko, modelBase) {

    function pageBase(parent) {
        var self = new modelBase(parent);
        self.SEARCH_MIN_LENGTH = 3;
        self.title = ko.observable("");
        self.visible = ko.observable(false);
        self.readonly = ko.observable(false);
        self.permissions = {
            Delete: ko.observable(true),
            Update: ko.observable(true),
            Add: ko.observable(true)
        };
        self.searchUrl = function () {
            return self.controllerName + "/Search/";
        };
        self.itemUrl = function () {
            return self.controllerName + "/GetById/";
        };
        self.getById = function (id, onLoadedFunc) {
            $.ajax({
                url: self.itemUrl(),
                type: "POST",
                dataType: "JSON",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (json) {
                    if (onLoadedFunc) {
                        onLoadedFunc(json);
                    }
                }
            });
        };

        self.name = "";
        self.load = function () {
            self.visible(true);
        };

        self.unload = function () {
            self.visible(false);
        };
        return self;
    }

    return pageBase;

});