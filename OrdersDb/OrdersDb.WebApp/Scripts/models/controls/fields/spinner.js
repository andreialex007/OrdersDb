define([
        "knockout",
        "./intField"
], function (ko, intField) {

    function spinner(title, value, step, min, max) {
        var self = new intField(title, value, "");
        self.mask = "999999999";
        self.step = step || 1;
        self.max = max;
        self.min = min;
        self.up = function () {
            self.value(self.value() + self.step);
            self.fixRange();
        };
        self.down = function () {
            self.value(self.value() - self.step);
            self.fixRange();
        };
        self.changed = function () {
            self.fixRange();
        };
        self.fixRange = function () {
            self.value(Math.round(self.value() / self.step) * self.step);
            if (self.value() > self.max)
                self.value(self.max);
            if (self.value() < self.min)
                self.value(self.min);
        };
        return self;
    }

    return spinner;

});