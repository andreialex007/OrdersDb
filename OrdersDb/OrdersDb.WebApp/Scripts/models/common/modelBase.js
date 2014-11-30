define(["knockout"], function (ko) {

    function modelBase(parent) {
        var self = {};
        self.parent = parent;
        self.visible = ko.observable(true);
        return self;
    }

    return modelBase;
})