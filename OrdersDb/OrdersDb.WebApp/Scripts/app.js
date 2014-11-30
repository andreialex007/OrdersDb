require([
        "knockout",
        "sammy",
        "Scripts/models/common/modelBase",
        "Scripts/models/pages/admin/dirsList",
        "Scripts/models/pages/account/account",
        "Scripts/models/pages/base/new",

        'Metronic/init',
        "Scripts/models/common/fixes",
        "Scripts/models/common/customBinders",
        "Scripts/libs/linq"
], function (ko, sammy, modelBase, adminDirectories, accountPage, newPage) {

    //Функция главного приложения

    function app() {
        var self = new modelBase();
        self.pages = {};
        self.pagesArr = function () {
            return $.map(self.pages, function (el) { return el; });
        };

        $(adminDirectories).each(function (i, element) {
            self.pages[element.name] = element.func(self);
            self.pages[element.name].controllerName = element.dir;
            if (element.name.match(/edit$/) != null) {
                var newPageObject = new newPage(element.func(self));
                var newPageName = element.name.replace(/edit$/, "") + "new";
                self.pages[newPageName] = newPageObject;
                self.pages[newPageName].controllerName = element.dir;
            }
        });
        
        //Загружка определенной страницы
        self.loadPage = function (pageName, params) {
            setTimeout(function () {
                //Выгружаем все страницы
                for (var i in self.pages) {
                    self.pages[i].unload();
                }

                var fullPageName = self.getPageByUrlParams(pageName, params.id);
                var page = self.pages[fullPageName];
                if (!page)
                    return;
                page.load(params);
            }, 10);
        };

        //Получает страницу для редактирования, создания, или изменения исходя из параметров юрл
        self.getPageByUrlParams = function (pageName, id) {
            if (!id)
                return pageName + "list";
            if (id == "new")
                return pageName + "new";
            return pageName + "edit";
        };

        //#region Инициализация сэми

        self.initializeSammy = function () {
            self.sammy = sammy('#main', function () {
                this.get('#/:page(/)?', function (context) {
                    self.loadPage(context.params.page, context.params);
                });

                this.get('#/:page/:id(/)?', function (context) {
                    self.loadPage(context.params.page, context.params);
                });

                this.get('#/', function (context) {
                    //root
                });
            });

            self.sammy.run("#/cities");
        };


        if (window.location.pathname.toLowerCase() == "/login") {
            self.account = new accountPage();
            ko.applyBindings(self);
        } else {
            ko.applyBindings(self);
            self.initializeSammy();
        }

        //#endregion

        return self;
    }

    app();

});