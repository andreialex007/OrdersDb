define([
        "knockout",
        'Metronic/global/plugins/jquery-ui/jquery-ui-1.10.3.custom.min',
        'Metronic/global/plugins/select2/select2',
        'Metronic/global/plugins/bootstrap-select/bootstrap-select',
        'Scripts/libs/jquery.maskedinput',
        'Scripts/libs/jquery.inputmask.bundle',
        'Scripts/libs/jquery.numeric',
        'Scripts/libs/jquery.fileupload'
], function (ko) {

    ko.bindingHandlers.clickHold = {
        init: function (element, valueAccessor) {
            var func = ko.unwrap(valueAccessor());
            $(element).repeatButton(function () {
                func();
            });
        }
    };

    ko.bindingHandlers.switch = {
        init: function (element, valueAccessor) {
            var func = ko.unwrap(valueAccessor());
            $(element).repeatButton(function () {
                func();
            });
        }
    };

    ko.bindingHandlers.select2 = {
        init: function (element, valueAccessor, bindings) {
            var value = valueAccessor();
            var unwrapped = ko.utils.unwrapObservable(bindings().value);
        },
        update: function (element, valueAccessor, bindings) {
            var unwrapped = ko.utils.unwrapObservable(bindings().value);
            $(element).select2("destroy");
            $(element).select2({ placeholder: "test" });
        }
    };

    ko.bindingHandlers.bootstrapSelect = {
        init: function (element, valueAccessor) {
            //            var options = valueAccessor();
            $(element).selectpicker({
                iconBase: 'fa',
                tickIcon: 'fa-check'
            });
        }
    };

    ko.bindingHandlers.numeric = {
        init: function (element, valueAccessor) {
            $(element).numeric();
        }
    };


    ko.bindingHandlers.scroll = {
        init: function (element, valueAccessor) {
            var func = ko.unwrap(valueAccessor());
            $(element).scroll(function () {
                func(Math.round(100 * element.scrollTop / (element.scrollHeight - element.clientHeight)));
            });
        }
    };

    ko.bindingHandlers.inputMask = {
        init: function (element, valueAccessor) {
            var value = ko.unwrap(valueAccessor());
            if (!value)
                return;

            if (value.type && value.options) {
                $(element).inputmask(value.type, value.options);
            } else if (value.options) {
                $(element).inputmask(value.options);
            }
        }
    };

    ko.bindingHandlers.mask = {
        init: function (element, valueAccessor) {
            $(element).inputmask(valueAccessor(), { placeholder: "" });
        }
    };

    ko.bindingHandlers.datePicker = {
        init: function (element) {
            var button = $(element).next();
            if (button) {
                button.click(function () {
                    button.datepicker("show");
                });
            }
            var dateFormat = $.cookie('lang') === "en" ? "mm.dd.yy" : "dd.mm.yy";
            $(element).datepicker({
                onSelect: function () {
                    $(element).trigger("change");
                },
                dateFormat: dateFormat
            });
        }
    };

    ko.bindingHandlers.fadeAndRemove = {
        init: function (element, valueAccessor) {
            var value = valueAccessor();
            $(element).click(function (event) {
                var delay = 500;
                $(element).closest("tr").fadeOut(delay);
                setTimeout(function () {
                    value();
                }, delay);
                event.stopPropagation();
            });
        }
    };

    ko.bindingHandlers.fadeVisible = {
        init: function (element, valueAccessor) {
            var value = valueAccessor();
            $(element).toggle(ko.unwrap(value));
        },
        update: function (element, valueAccessor) {
            var value = valueAccessor();
            ko.unwrap(value) ? $(element).show("fast") : $(element).hide("fast");
        }
    };

    ko.bindingHandlers.readonly = {
        init: function (element, valueAccessor) {
            var value = ko.utils.unwrapObservable(valueAccessor());
            $(element).find(":input, :checkbox").attr("disabled", value);
        },
        update: function (element, valueAccessor) {
            var value = ko.utils.unwrapObservable(valueAccessor());
            $(element).find(":input, :checkbox").attr("disabled", value);
        }
    };



    ko.bindingHandlers.slider = {
        init: function (element, valueAccessor) {
            var valueAcc = valueAccessor();
            var min = ko.unwrap(valueAcc.min);
            var max = ko.unwrap(valueAcc.max);
            var step = ko.unwrap(valueAcc.step);
            var minValue = ko.unwrap(valueAcc.values[0]);
            var maxValue = ko.unwrap(valueAcc.values[1]);
            $(element).slider({
                range: true,
                max: max,
                min: min,
                step: step || 1,
                values: [minValue, maxValue],
                slide: function (event, ui) {
                    valueAcc.values[0](ui.values[0]);
                    valueAcc.values[1](ui.values[1]);
                }
            });
        },
        update: function (element, valueAccessor) {
            var valueAcc = valueAccessor();
            var minValue = ko.unwrap(valueAcc.values[0]);
            var maxValue = ko.unwrap(valueAcc.values[1]);
            $(element).slider('values', [minValue, maxValue]);
        }
    };


    ko.bindingHandlers.fileUpload = {
        init: function (element, valueAccessor) {
            var unwrapped = ko.unwrap(valueAccessor());
            var options = unwrapped.options;
            var fileuploadsubmit = unwrapped.fileuploadsubmit || function () { };
            $(element).fileupload(options);
            $(element).bind('fileuploadsubmit', fileuploadsubmit);
        }
    };


})