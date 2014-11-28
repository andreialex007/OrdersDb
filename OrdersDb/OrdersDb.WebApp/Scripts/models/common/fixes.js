define(["knockout", 'jquery', 'Metronic/global/plugins/jquery.cokie.min'], function (ko) {

    window.onTableDataLoaded = function () {
        $(".scroll-fix").each(function (index, element) {
            var $tableControl = $(element).closest(".table-control");
            var calculatedWidth = $tableControl.find(".table-head").width() - $tableControl.find(".table-body").width();
            console.log("calculatedWidth=" + calculatedWidth);
            $(element).css({ width: calculatedWidth + "px" });
        });
    };

    (function ($) {
        $.fn.repeatButton = function (func) {
            var pauseMS = 200;
            var repeatMS = 50;

            return this.each(function () {

                var $this = $(this);

                var intervalId;
                var timeoutId;

                $this.mousedown(function () {
                    func();
                    timeoutId = setTimeout(startFunc, pauseMS);
                }).mouseup(function () {
                    clearInterval(intervalId);
                    clearTimeout(timeoutId);
                }).mouseout(function () {
                    clearInterval(intervalId);
                    clearTimeout(timeoutId);
                });

                function startFunc() {
                    intervalId = setInterval(func, repeatMS);
                }

            });
        };
    })(jQuery);

    window.utils = {
        commaListToArray: function (value) {
            return (value || "").split(',').filter(function (n) { return n ? true : false; });
        },
        idNameToTextValue: function (arr) {
            return $.map(arr, function (item) {
                return { Value: item.Id, Text: item.Name };
            });
        }
    };

    ko.subscribable.fn.subscribeChanged = function (callback) {
        var oldValue;
        this.subscribe(function (_oldValue) {
            oldValue = _oldValue;
        }, this, 'beforeChange');

        this.subscribe(function (newValue) {
            callback(newValue, oldValue);
        });
    };

    if (!$.cookie("Permissions"))
        return;

    var permissions = $.parseJSON($.cookie("Permissions"));
    $("[data-pagename]").each(function (index, element) {
        var pageName = $(element).data("pagename");
        var readAllowed = $.inArray(pageName, permissions.Reads) != -1;
        var updateAllowed = $.inArray(pageName, permissions.Updates) != -1;
        var addAllowed = $.inArray(pageName, permissions.Adds) != -1;
        var deleteAllowed = $.inArray(pageName, permissions.Deletes) != -1;
        if (readAllowed == false) {
            $(element).hide();
        } else {
            if (!updateAllowed)
                $(element).find("[data-access='update']").css({ "display": "none" });
            if (!deleteAllowed)
                $(element).find("[data-access='delete']").css({ "display": "none" });
            if (!addAllowed)
                $(element).find("[data-access='add']").css({ "display": "none" });
        }
    });

    trim = (function () {
        "use strict";
        function escapeRegex(string) {
            return string.replace(/[\[\](){}?*+\^$\\.|\-]/g, "\\$&");
        }

        return function trim(str, characters, flags) {
            flags = flags || "g";
            if (typeof str !== "string" || typeof characters !== "string" || typeof flags !== "string") {
                throw new TypeError("argument must be string");
            }

            if (!/^[gi]*$/.test(flags)) {
                throw new TypeError("Invalid flags supplied '" + flags.match(new RegExp("[^gi]*")) + "'");
            }

            characters = escapeRegex(characters);

            return str.replace(new RegExp("^[" + characters + "]+|[" + characters + "]+$", flags), '');
        };
    }());
});


