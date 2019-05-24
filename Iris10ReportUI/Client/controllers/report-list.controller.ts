declare var REPORT_LIST: boolean;
// libs
import * as _ from 'lodash';

import ReportFilterController from './report-filter.controller';
// app
import { kendoWindowDefaultOptions } from '../utils';

export default class ReportListController {

    private static _instance: ReportListController;




    static getInstance() {
        if (!this._instance) {
            this._instance = new ReportListController();
        }
        return this._instance;
    }

    lastSearchString: string;
    /**
     * To avoid kendo mvvm binding madness this must be static
     */
    private static _gridConfigSelector: kendo.ui.DropDownList;
    private static _gridFilterSelector: kendo.ui.DropDownList;
    private static _gridAdminToolSelector: kendo.ui.DropDownList;
    private static _gridColumnMenu: kendo.ui.Menu;
    toolFunctionModel: kendo.data.ObservableObject;
    grid: kendo.ui.Grid;
    private _searchTimer: number;
    private notificationWindow: kendo.ui.Window;
    private _reportViewWindow: kendo.ui.Window;
    keepSessionAlive = false;
    keepSessionAliveUrl = null;


    /**
     * Sets grid with target element
     * @param target
     */
    setGrid(target: string) {
        let self = this;
        this.grid = $(target).data('kendoGrid');
        let container = $('<div id="ReportWindowContainer"></div>');
        $('body').append(container);
        $("#ReportListGrid .k-grid-header").hide(); //hide the header
        $("#ReportListGrid tr.k-alt").removeClass("k-alt"); //hide the alternate color changing
        $("#ReportListGrid td").css('border-style', 'none'); //remove cell borders to make it look like a proper list
        $('#ReportToolBar').data('kendoToolBar').element.find('#reportsearch').first().val(this.lastSearchString);
        $("#FinishFilter").bind('click', self.finishFilter);
        this.grid.dataSource.bind("error", this.errorLog);
        self._setupSessionUpdater('/Root/KeepSessionAlive');
        self.createTools();
        //Select a row
        $('#ReportListGrid').data('kendoGrid').tbody.on('mousedown', function (this: HTMLElement, event: JQueryEventObject) {
            let row: any = event.target.parentElement["rowIndex"];
            var replist: any = $("#ReportListGrid").data("kendoGrid");
            $.ajax({
                global: false,
                type: "POST",
                url: "/RPTReportListByUser/SelectReport",
                data: { report: JSON.stringify(replist._data[row]) }
            }).done((data) => {
                ReportFilterController.getInstance().activateFilter();
            });
        });

        //Checkbox events
        $('.fav').on('change', function () {
            var t = event.currentTarget['checked'];
            var replist: any = $("#ReportListGrid").data("kendoGrid");
            var index = event.currentTarget["parentElement"].parentElement.rowIndex;
            $.ajax({
                global: false,
                type: "POST",
                url: "/RPTReportListByUser/UpdateFavorite",
                data: { fav: JSON.stringify(replist._data[index]), check: t }
            }).done((data) => {
                replist.dataSource.read();
                });
        
        });

        return this;
    }

    createTools() {
        let self = this;

        this.toolFunctionModel = kendo.observable({
            
            searchKeyPressed: self.searchKeyPressed.bind(self)
           
            
        });
       
        kendo.bind($('#ReportToolBar'), this.toolFunctionModel);
    }

   
    searchKeyPressed(e: KeyboardEvent) {
        let timer = 1000;
        let self = this;
        clearTimeout(this._searchTimer);
        this._searchTimer = setTimeout(function () {
            self.gridSearch();
        }, timer, self);
        if (e.keyCode === 13) {
            clearTimeout(self._searchTimer);
            self.gridSearch();
        }
    }

    gridSearch() {
        const toolBar = $('#ReportToolBar').data('kendoToolBar');
        const query = toolBar.element.find('#reportsearch').first().val().toUpperCase();
        let searchText = toolBar.element.find('#reportsearch').first().val();
        var replist: any = $("#ReportListGrid").data("kendoGrid");
        let self = this;
        self.lastSearchString = searchText;
        $.ajax({
            global: false,
            type: "Post",
            url: "/ReportSearch/ReportQuery",
            data: { searchString: query }
        }).done((data) => {
            // All good.
            if (data !== "ready") {
                console.log(`${data}`);
            } else {
                replist.dataSource.read();
                setTimeout(function () {
                    toolBar.element.find('#reportsearch').first().val(searchText);
                }, 500, toolbar, searchText);
            }
        });
    }


    saveCriteria()
    {
        return this;
    }

    deleteCriteria()
    {
        return this;
    }

    finishFilter() {
        var self = ReportListController.getInstance();
        self._setupReportViewWindow().then(() => {
            kendo.bind($('#ReportWindowContainer'), self);
            self._reportViewWindow.center().open();

       
        });
    }

    
    errorLog(e) {
        if (e.errors) {
            console.log("error happened");
        }
    }

    onNotifications() {
        this.notificationWindow.open();
    }

    /**
     * Session keepalive functions
     */
    private _setupSessionUpdater(actionUrl) {
        let self = this;
        self.keepSessionAliveUrl = actionUrl;
        var container = $("#body");
        self.keepSessionAlive = true;
        //container.mousemove(function () { self.keepSessionAlive = true; });
        //container.keydown(function () { self.keepSessionAlive = true; });
        self._checkToKeepSessionAlive();
    }

    private _checkToKeepSessionAlive() {
        let self = this;
        setTimeout(function () {
            self._keepSessionAlive();
        }, 300000, self);
    }

    private _keepSessionAlive() {
        let self = this;
        if (self.keepSessionAlive && self.keepSessionAliveUrl != null) {
            $.ajax({
                global: false,
                type: "POST",
                url: self.keepSessionAliveUrl
            }).done((data) => {
                self.keepSessionAlive = true;
            });
        }
        self._checkToKeepSessionAlive();
    }

    private constructor() {
        this._init();
    }

    /**
     * Initializes the controller
     * NOTE: Do NOT include asynchronous calls in this method.
     *       When Kendo binds to the controller it makes a copy rather
     *       than using the instance in memory.
     */
    private _init() {
        try {
            let self = this;
      
            this._searchTimer = 0;
            
            
        } catch (err) {
            console.log('System not loaded yet');
        }
        
    }

    private _setupReportViewWindow() {
        let deferred = $.Deferred<void>();
        this._reportViewWindow = $('<div id="reportViewWindow"></div>').kendoWindow(kendoWindowDefaultOptions({
            appendTo: '#ReportWindowContainer',
            content: '/ReportFilterCriteria/FinishFilter',
            title: 'Report Viewer',
            height: 900,
            width: 900,
            refresh: () => {
                deferred.resolve();
            }
        })).data('kendoWindow');
        return deferred.promise();
    }

 
}

$(document).ready(() => {
    try {
        if (REPORT_LIST !== null && REPORT_LIST) {
            window['ReportListDataBound'] = () => {
                ReportListController.getInstance().setGrid('#ReportListGrid');
            }
        }
        
    } catch (err) {
        window.console.log(`no grid to load; or grid is set in view model.`);
    }
}); 

