define([ "knockout" ],function(ko) {
    
    function modelBase(parent) {
        var self = {};
        self.parent = parent;
        return self;
    }

    return modelBase;
})