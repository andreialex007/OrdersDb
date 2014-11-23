define([
        "knockout",
        "./textField"
], function (ko, textField) {

    function rangeSlider(title, min, max, minValue, maxValue, step) {
        var self = new textField(title, "", "");
        self.min = ko.observable(min);
        self.max = ko.observable(max);
        self.step = ko.observable(step || 1);
        self._minValue = ko.observable(minValue || min);
        self.minValue = ko.computed({
            read: function () {
                return self._minValue();
            },
            write: function (value) {
                value = Math.round(value / self.step()) * self.step();
                if (value > self.maxValue())
                    value = self.maxValue();
                if (value < self.min())
                    value = self.min();
                self._minValue(value);
            }
        });

        self._maxValue = ko.observable(maxValue || max);
        self.maxValue = ko.computed({
            read: function () {
                return self._maxValue();
            },
            write: function (value) {
                value = Math.round(value / self.step()) * self.step();
                if (value < self.minValue())
                    value = self.minValue();
                if (value > self.max())
                    value = self.max();
                self._maxValue(value);
            }
        });
        return self;
    }

    return rangeSlider;

});