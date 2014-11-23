require([
        'Metronic/core',
        'Metronic/global/scripts/metronic',
        'Metronic/admin/layout/scripts/layout',
        'Metronic/admin/layout/scripts/quick-sidebar'
], function (core, metronic, layout, quickSidebar) {
    $(document).ready(function () {
        Metronic.init(); // init metronic core componets
        Layout.init(); // init layout
        QuickSidebar.init(); // init quick sidebar
    });
});