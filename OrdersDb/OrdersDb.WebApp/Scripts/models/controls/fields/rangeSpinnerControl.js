define([
        "knockout",
        "./rangeSlider",
        "./spinner"
], function (ko, rangeSlider, spinner) {

    function rangeSpinnerControl(title, min, max, step) {
        var self = this;
        self.STEP = step || 10000;
        self.rangeSlider = new rangeSlider(title, min, max, min, max, self.STEP);
        self.min = new spinner(CommonResources.Min, min, self.STEP, min, max);
        self.max = new spinner(CommonResources.Max, max, self.STEP, min, max);

        self.min.value = self.rangeSlider.minValue;
        self.max.value = self.rangeSlider.maxValue;

        return self;
    }


    return rangeSpinnerControl;

});