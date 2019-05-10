/******/ (function(modules) { // webpackBootstrap
/******/ 	// The module cache
/******/ 	var installedModules = {};
/******/
/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {
/******/
/******/ 		// Check if module is in cache
/******/ 		if(installedModules[moduleId])
/******/ 			return installedModules[moduleId].exports;
/******/
/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = installedModules[moduleId] = {
/******/ 			exports: {},
/******/ 			id: moduleId,
/******/ 			loaded: false
/******/ 		};
/******/
/******/ 		// Execute the module function
/******/ 		modules[moduleId].call(module.exports, module, module.exports, __webpack_require__);
/******/
/******/ 		// Flag the module as loaded
/******/ 		module.loaded = true;
/******/
/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}
/******/
/******/
/******/ 	// expose the modules object (__webpack_modules__)
/******/ 	__webpack_require__.m = modules;
/******/
/******/ 	// expose the module cache
/******/ 	__webpack_require__.c = installedModules;
/******/
/******/ 	// __webpack_public_path__
/******/ 	__webpack_require__.p = "";
/******/
/******/ 	// Load entry module and return exports
/******/ 	return __webpack_require__(0);
/******/ })
/************************************************************************/
/******/ ([
/* 0 */
/***/ function(module, exports, __webpack_require__) {

	"use strict";
	__webpack_require__(1);
	// TODO: break up Iris.js
	// import './Iris';
	// import './GridConfigFunctions.js';
	// import './IrisFilter.js';


/***/ },
/* 1 */
/***/ function(module, exports, __webpack_require__) {

	"use strict";
	function __export(m) {
	    for (var p in m) if (!exports.hasOwnProperty(p)) exports[p] = m[p];
	}
	__export(__webpack_require__(2));


/***/ },
/* 2 */
/***/ function(module, exports) {

	"use strict";
	class ReportListController {
	    constructor() {
	        this.keepSessionAlive = false;
	        this.keepSessionAliveUrl = null;
	        this._init();
	    }
	    static getInstance() {
	        if (!this._instance) {
	            this._instance = new ReportListController();
	        }
	        return this._instance;
	    }
	    /**
	     * Sets grid with target element
	     * @param target
	     */
	    setGrid(target) {
	        let self = this;
	        this.grid = $(target).data('kendoGrid');
	        $("#ReportListGrid .k-grid-header").hide(); //hide the header
	        $("#ReportListGrid tr.k-alt").removeClass("k-alt"); //hide the alternate color changing
	        $("#ReportListGrid td").css('border-style', 'none'); //remove cell borders to make it look like a proper list
	        $('#ReportToolBar').data('kendoToolBar').element.find('#reportsearch').first().val(this.lastSearchString);
	        this.grid.dataSource.bind("error", this.errorLog);
	        self._setupSessionUpdater('/Root/KeepSessionAlive');
	        self.createTools();
	        //Select a row
	        $('#ReportListGrid').data('kendoGrid').tbody.on('mousedown', function (event) {
	            let row = event.target.parentElement["rowIndex"];
	            var replist = $("#ReportListGrid").data("kendoGrid");
	            $.ajax({
	                global: false,
	                type: "POST",
	                url: "/RPTReportListByUser/SelectReport",
	                data: { report: JSON.stringify(replist._data[row]) }
	            });
	        });
	        //Checkbox events
	        $('.fav').on('change', function () {
	            var t = event.currentTarget['checked'];
	            var replist = $("#ReportListGrid").data("kendoGrid");
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
	    searchKeyPressed(e) {
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
	        var replist = $("#ReportListGrid").data("kendoGrid");
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
	            }
	            else {
	                replist.dataSource.read();
	                setTimeout(function () {
	                    toolBar.element.find('#reportsearch').first().val(searchText);
	                }, 500, toolbar, searchText);
	            }
	        });
	    }
	    saveCriteria() {
	        return this;
	    }
	    deleteCriteria() {
	        return this;
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
	    _setupSessionUpdater(actionUrl) {
	        let self = this;
	        self.keepSessionAliveUrl = actionUrl;
	        var container = $("#body");
	        self.keepSessionAlive = true;
	        //container.mousemove(function () { self.keepSessionAlive = true; });
	        //container.keydown(function () { self.keepSessionAlive = true; });
	        self._checkToKeepSessionAlive();
	    }
	    _checkToKeepSessionAlive() {
	        let self = this;
	        setTimeout(function () {
	            self._keepSessionAlive();
	        }, 300000, self);
	    }
	    _keepSessionAlive() {
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
	    /**
	     * Initializes the controller
	     * NOTE: Do NOT include asynchronous calls in this method.
	     *       When Kendo binds to the controller it makes a copy rather
	     *       than using the instance in memory.
	     */
	    _init() {
	        try {
	            let self = this;
	            this._searchTimer = 0;
	        }
	        catch (err) {
	            console.log('System not loaded yet');
	        }
	    }
	}
	Object.defineProperty(exports, "__esModule", { value: true });
	exports.default = ReportListController;
	$(document).ready(() => {
	    try {
	        if (REPORT_LIST !== null && REPORT_LIST) {
	            window['ReportListDataBound'] = () => {
	                ReportListController.getInstance().setGrid('#ReportListGrid');
	            };
	        }
	    }
	    catch (err) {
	        window.console.log(`no grid to load; or grid is set in view model.`);
	    }
	});


/***/ }
/******/ ]);
//# sourceMappingURL=main.js.map