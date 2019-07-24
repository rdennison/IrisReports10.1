"use strict";
// libs
const _ = require('lodash');
// app
const utils_1 = require('../utils');
class ReportFilterController {
    constructor() {
        this._init();
    }
    static getInstance() {
        if (!this._instance) {
            this._instance = new ReportFilterController();
        }
        return this._instance;
    }
    setGrid(target) {
        return this;
    }
    activateFilter() {
        $.ajax({
            global: false,
            type: "POST",
            url: "/ReportFilterCriteria/ActivateFilter"
        });
    }
    filterConfiguration(controller, row, operator = "", val1 = "", val2 = "") {
        let Configuration = { row: row, operatorType: operator, valueoneType: val1, valuetwoType: val2, valueList: "" };
        if (controller._filterConfig.length > row) {
            controller._filterConfig[row].valueoneType = val1;
            controller._filterConfig[row].valuetwoType = val2;
        }
        else {
            controller._filterConfig.push(Configuration);
        }
    }
    editCell(e) { }
    bindGridCells() {
        var self = this;
        var grid = $('#ReportFilterCriteriaGrid').data('kendoGrid');
        grid.dataSource.data().forEach((row, index) => {
            self.filterchangeEvent(self, 'ReportFieldList', JSON.parse(grid.dataSource.data()[0]["InList"])["ColumnName"], "", index, '/ReportFilterCriteria/ValueField', function () {
                self.filterchangeEvent(self, 'operatordropdownListoptions', row["ComparisonOperator"], "", index, '/ReportFilterCriteria/ValueField', function () { });
            });
        });
    }
    deleteDropdowneditors(controller, row) {
        delete controller._grid.dataSource.view()[row]["DropdownValues"];
        delete controller._grid.dataSource.view()[row]["DropdownValues2"];
        delete controller._grid.dataSource.view()[row]["DropdownValues3"];
    }
    filterclickEvent(controller, clicked, row) {
        var curIndex = 0;
        if (clicked.cellIndex !== undefined)
            curIndex = clicked.cellIndex;
        else {
            try {
                curIndex = clicked.index();
            }
            catch (ex) { }
        }
        if (curIndex !== 0) {
            var curCell = controller._grid.tbody.find(">tr:eq(" + row + ") >td:eq(" + curIndex + ")");
            curCell.addClass('k-state-focused');
            controller._grid.select(curCell);
            controller._grid.editCell(curCell);
        }
        switch (curIndex) {
            case 4:
                if (controller._descriptionvalue !== "" && controller._descriptionvalue !== undefined)
                    $("#Group1").data('kendoDropDownList').wrapper.show();
                break;
            case 6:
                try {
                    if (controller._descriptionvalue !== "" && controller._descriptionvalue !== undefined) {
                        $("#" + controller._filterConfig[row].operatorType).data('kendoComboBox').wrapper.show();
                        ReportFilterController.getInstance().comboSelection(controller._grid);
                    }
                }
                catch (ex) { }
                break;
            case 7:
                try {
                    if (controller._filterConfig[row].valueoneType === "DropdownValues" || controller._filterConfig[row].valueoneType === "DropdownValues2") {
                        $("#" + controller._filterConfig[row].valueoneType).getKendoComboBox().dataSource.data(controller._filterConfig[row].valueList);
                        $("#" + controller._filterConfig[row].valueoneType).getKendoComboBox().dataSource.query();
                        $("#" + controller._filterConfig[row].valueoneType).data('kendoComboBox').wrapper.show();
                        ReportFilterController.getInstance().comboSelection(controller._grid);
                    }
                    else if (controller._filterConfig[row].valueoneType === "TextBox") {
                        $("#" + controller._filterConfig[row].valueoneType).data('kendoMaskedTextBox').wrapper.show();
                    }
                    else if (controller._filterConfig[row].valueoneType === "NumberBox") {
                        $("#" + controller._filterConfig[row].valueoneType).data('kendoNumericTextBox').wrapper.show();
                    }
                    else if (controller._filterConfig[row].valueoneType === "DateValues") {
                        $("#" + controller._filterConfig[row].valueoneType).data('kendoDatePicker').wrapper.show();
                    }
                    $("#" + controller._filterConfig[row].operatorType).data('kendoComboBox').wrapper.show();
                }
                catch (ex) { }
                break;
            case 8:
                try {
                    if (controller._filterConfig[row].valuetwoType === "DropdownValues3") {
                        $("#" + controller._filterConfig[row].valuetwoType).getKendoComboBox().dataSource.data(controller._filterConfig[row].valueList);
                        $("#" + controller._filterConfig[row].valuetwoType).getKendoComboBox().dataSource.query();
                        $("#" + controller._filterConfig[row].valuetwoType).data('kendoComboBox').wrapper.show();
                        ReportFilterController.getInstance().comboSelection(controller._grid);
                    }
                    else if (controller._filterConfig[row].valuetwoType === "TextBox2") {
                        $("#" + controller._filterConfig[row].valuetwoType).data('kendoMaskedTextBox').wrapper.show();
                    }
                    else if (controller._filterConfig[row].valuetwoType === "NumberBox2") {
                        $("#" + controller._filterConfig[row].valuetwoType).data('kendoNumericTextBox').wrapper.show();
                    }
                    else if (controller._filterConfig[row].valuetwoType === "DateValues2") {
                        $("#" + controller._filterConfig[row].valuetwoType).data('kendoDatePicker').wrapper.show();
                    }
                }
                catch (ex) { }
                break;
            case 9:
                if (controller._descriptionvalue !== "" && controller._descriptionvalue !== undefined)
                    $("#Group2").data('kendoDropDownList').wrapper.show();
                break;
            default:
                break;
        }
    }
    filterchangeEvent(controller, dropdown, selectValue, textVal, row, url, _callback) {
        let filter = ReportFilterController.getInstance();
        controller._grid.current(controller._grid.select());
        var cellIndex;
        try {
            cellIndex = controller._grid.current().index();
        }
        catch (ex) { }
        switch (dropdown) {
            case 'ReportFieldList':
                $.ajax({
                    global: false,
                    type: "GET",
                    url: url,
                    data: { field: selectValue }
                }).done((data) => {
                    let EditorType = data;
                    controller._descriptionvalue = selectValue;
                    switch (EditorType) {
                        case "Dropdown":
                            filter.filterConfiguration(controller, row, "operatordropdownListoptions", "DropdownValues", "");
                            filter.foreignKeyValueDropdown(controller, selectValue, row);
                            if ($('#ReportFieldList').data('kendoComboBox') !== undefined)
                                controller._grid.dataSource.view()[row]["ColumnName"] = $('#ReportFieldList').data('kendoComboBox').text();
                            controller._addBlankRow(controller);
                            _callback();
                            break;
                        case "Date":
                            filter.filterConfiguration(controller, row, "operatordropdownListoptions", "DateValues", "");
                            if ($('#ReportFieldList').data('kendoComboBox') !== undefined)
                                controller._grid.dataSource.view()[row]["ColumnName"] = $('#ReportFieldList').data('kendoComboBox').text();
                            controller.deleteDropdowneditors(controller, row);
                            _callback();
                            break;
                        case "Boolean":
                            filter.filterConfiguration(controller, row, "operatordropdownListoptions", "DropdownValues2", "");
                            if ($('#ReportFieldList').data('kendoComboBox') !== undefined)
                                controller._grid.dataSource.view()[row]["ColumnName"] = $('#ReportFieldList').data('kendoComboBox').text();
                            controller.deleteDropdowneditors(controller, row);
                            _callback();
                            break;
                        case "Text":
                            filter.filterConfiguration(controller, row, "operatordropdownListoptions", "TextBox", "");
                            if ($('#ReportFieldList').data('kendoComboBox') !== undefined)
                                controller._grid.dataSource.view()[row]["ColumnName"] = $('#ReportFieldList').data('kendoComboBox').text();
                            controller.deleteDropdowneditors(controller, row);
                            _callback();
                            break;
                        case "Number":
                            filter.filterConfiguration(controller, row, "operatordropdownListoptions", "NumberBox", "");
                            if ($('#ReportFieldList').data('kendoComboBox') !== undefined)
                                controller._grid.dataSource.view()[row]["ColumnName"] = $('#ReportFieldList').data('kendoComboBox').text();
                            controller.deleteDropdowneditors(controller, row);
                            _callback();
                            break;
                    }
                });
                break;
            case 'Operator2':
                if ($('#Operator2').data('kendoDropDownList') !== undefined)
                    controller._grid.dataSource.view()[row]["AndOr"] = $('#Operator2').data('kendoDropDownList').text();
                break;
            case 'DropdownValues':
                if ($('#DropdownValues').data('kendoComboBox') !== undefined)
                    controller._grid.dataSource.view()[row]["Value1"] = $('#DropdownValues').data('kendoComboBox').text();
                break;
            case 'DropdownValues2':
                if ($('#DropdownValues2').data('kendoComboBox') !== undefined)
                    controller._grid.dataSource.view()[row]["Value1"] = $('#DropdownValues2').data('kendoComboBox').text();
                break;
            case 'TextBox':
                if ($('#TextBox').data('kendoMaskedTextBox') !== undefined)
                    controller._grid.dataSource.view()[row]["Value1"] = $('#TextBox').data('kendoMaskedTextBox').value();
                break;
            case 'NumberBox':
                if ($('#NumberBox').data('kendoNumericTextBox'))
                    controller._grid.dataSource.view()[row]["Value1"] = $('#NumberBox').data('kendoNumericTextBox').value();
                break;
            case 'DateValues':
                if ($('#DateValues').data('kendoDatePicker') !== undefined)
                    controller._grid.dataSource.view()[row]["Value1"] = $('#DateValues').data('kendoDatePicker').value().toLocaleDateString("en-US");
                break;
            case 'DropdownValues3':
                if ($('#DropdownValues3').data('kendoComboBox') !== undefined)
                    controller._grid.dataSource.view()[row]["Value2"] = $('#DropdownValues3').data('kendoComboBox').text();
                break;
            case 'TextBox2':
                if ($('#TextBox2').data('kendoMaskedTextBox') !== undefined)
                    controller._grid.dataSource.view()[row]["Value2"] = $('#TextBox2').data('kendoMaskedTextBox').value();
                break;
            case 'NumberBox2':
                if ($('#NumberBox2').data('kendoNumericTextBox') !== undefined)
                    controller._grid.dataSource.view()[row]["Value2"] = $('#NumberBox2').data('kendoNumericTextBox').value();
                break;
            case 'DateValues2':
                if ($('#DateValues2').data('kendoDatePicker') !== undefined)
                    controller._grid.dataSource.view()[row]["Value2"] = $('#DateValues2').data('kendoDatePicker').value().toLocaleDateString("en-US");
                break;
            case 'Group1':
                if ($('#Group1').data('kendoDropDownList') !== undefined)
                    controller._grid.dataSource.view()[row]["OpenGroup"] = $('#Group1').data('kendoDropDownList').value();
                break;
            case 'Group2':
                if ($('#Group2').data('kendoDropDownList').value() !== undefined)
                    controller._grid.dataSource.view()[row]["CloseGroup"] = $('#Group2').data('kendoDropDownList').value();
                break;
            case 'operatordropdownListoptions':
                if ($('#operatordropdownListoptions').data('kendoComboBox') !== undefined)
                    controller._grid.dataSource.view()[row]["ComparisonOperator"] = $('#operatordropdownListoptions').data('kendoComboBox').value();
                if (selectValue === "Between") {
                    $.ajax({
                        global: false,
                        type: "GET",
                        url: url,
                        data: { field: controller._descriptionvalue }
                    }).done((data) => {
                        let EditorType = data;
                        switch (EditorType) {
                            case "Dropdown":
                                filter.filterConfiguration(controller, row, "", "DropdownValues", "DropdownValues3");
                                break;
                            case "Date":
                                filter.filterConfiguration(controller, row, "", "DateValues", "DateValues2");
                                break;
                            case "Number":
                                filter.filterConfiguration(controller, row, "", "NumberBox", "NumberBox2");
                                break;
                            case "Text":
                                filter.filterConfiguration(controller, row, "", "TextBox", "TextBox2");
                                break;
                            default: break;
                        }
                    });
                }
                else if (selectValue === "StartsWith" || selectValue === "EndsWith" || selectValue === "Like" || selectValue === "NotLike" || selectValue === "Contains") {
                    controller.deleteDropdowneditors(controller, row);
                    filter.filterConfiguration(controller, row, "", "TextBox", "");
                }
                else {
                    if (dropdown.match("dropdown"))
                        filter.filterConfiguration(controller, row, "", "DropdownValues", "");
                    if (dropdown.match("text"))
                        filter.filterConfiguration(controller, row, "", "TextBox", "");
                    if (controller._grid.dataSource.view()[row]["ColumnName"] !== null)
                        if (controller._grid.dataSource.view()[row]["ColumnName"].match("Date"))
                            filter.filterConfiguration(controller, row, "", "DateValues", "");
                    if (dropdown.match("number"))
                        filter.filterConfiguration(controller, row, "", "NumberBox", "");
                    if (dropdown.match("True"))
                        filter.filterConfiguration(controller, row, "", "DropdownValues2", "");
                }
                if ($('#' + controller._filterConfig[row].operatorType).data('kendoComboBox') !== undefined)
                    controller._grid.dataSource.view()[row]["ComparisonOperator"] = $('#' + controller._filterConfig[row].operatorType).data('kendoComboBox').text();
                controller._grid.refresh();
                break;
        }
    }
    comboSelection(grid) {
        let comboBox = grid.select().children().closest('.k-combobox');
        var combo;
        if (comboBox.length >= 1) {
            let x = $(comboBox[0]).find("input:not(:visible)");
            combo = $(x).data('kendoComboBox');
        }
        setTimeout(function () {
            combo.input.select();
        }, 50, combo);
    }
    loadReportFilter(filterName) {
        var controller = this;
        var grid = $("#ReportFilterCriteriaGrid").data('kendoGrid');
        $.ajax({
            global: false,
            type: "GET",
            url: '/ReportFilterCriteria/LoadReportFilter',
            data: { filter: filterName }
        }).done((data) => {
            grid.dataSource.read().done(() => {
                controller.bindGridCells();
            });
        });
    }
    loadDisplayList() {
        var grid = $("#ReportFilterCriteriaGrid").data("kendoGrid");
        $.ajax({
            global: false,
            type: "POST",
            url: '/ReportFilterCriteria/SaveCriteriaList',
            data: { modelListString: JSON.stringify(grid.dataSource.view().toJSON()) }
        });
    }
    save() {
        var self = this;
        var grid = $("#ReportFilterCriteriaGrid").data("kendoGrid");
        $.ajax({
            global: false,
            type: "POST",
            url: "/ReportFilterCriteria/FilterValidation",
            data: { type: "result", rowData: JSON.stringify(grid.dataSource.view().toJSON()) }
        }).done((data) => {
            //if (data === "valid") {
            self.loadDisplayList();
            this._setupFilterSaveWindow().then(() => {
                kendo.bind($('#filterSaveWindow'), this);
                self._filterSaveWindow.center().open();
                $("#FilterNameList").data('kendoComboBox').value($("#ReportFilterGridNameList").data('kendoComboBox').text());
                $("#ReportFilterCancel").on('click', function () {
                    self._filterSaveWindow.close();
                });
                $("#ReportFilterSave").on('click', function () {
                    $.ajax({
                        global: false,
                        type: "GET",
                        url: "/ReportFilterCriteria/SaveValidation",
                        data: { filterName: $("#FilterNameList").data('kendoComboBox').text() }
                    }).done((data) => {
                        if (data === "overWrite") {
                            self._setupFilterOverwriteWindow().then(() => {
                                kendo.bind($('#ReportFilterCriteriaGridContainer'), self);
                                self._overwriteFilterWindow.center().open();
                                $("#OverWriteYes").on('click', function () {
                                    $.ajax({
                                        global: false,
                                        type: "POST",
                                        url: "/ReportFilterCriteria/SaveReportFilter",
                                        data: { filterName: $("#FilterNameList").data('kendoComboBox').text(), filterReplace: true }
                                    }).done((data) => {
                                        self._overwriteFilterWindow.close();
                                        self.reloadSavedFilterList();
                                        self._filterSaveWindow.close();
                                    });
                                });
                                $("#OverWriteCancel").on('click', function () {
                                    self._overwriteFilterWindow.close();
                                });
                            });
                        }
                        else {
                            $.ajax({
                                global: false,
                                type: "POST",
                                url: "/ReportFilterCriteria/SaveReportFilter",
                                data: { filterName: $("#FilterNameList").data('kendoComboBox').text(), filterReplace: true }
                            }).done((data) => {
                                self.reloadSavedFilterList();
                                self._filterSaveWindow.close();
                            });
                        }
                    });
                });
            });
            //}
            //else {
            //    console.log(data);
            //}
        });
    }
    finishFilter() {
        var grid = $("#ReportFilterCriteriaGrid").data("kendoGrid");
        $.ajax({
            global: false,
            type: "POST",
            url: "/ReportFilterCriteria/PopulateGridDisplayList",
            data: { gridrows: JSON.stringify(grid.dataSource.view().toJSON()) }
        }).done((data) => {
            this._setupReportViewWindow();
            this._reportViewWindow.center().open();
            //$('#reportError').bind("Error", function () => {
            //});
            //    $('#reportPageCount').
        });
    }
    overWrite() {
        this._overwriteFilterWindow.center().open();
    }
    foreignKeyValueDropdown(controller, selectedValue, currentRow) {
        $.ajax({
            global: false,
            type: "GET",
            url: '/ReportFilterCriteria/ForeignKeyValue',
            data: { field: selectedValue }
        }).done((data) => {
            controller._filterConfig[currentRow].valueList = data;
        });
    }
    reloadSavedFilterList() {
        var previousName = $("#FilterNameList").data('kendoComboBox').text();
        $("#ReportFilterGridNameList").remove();
        $.ajax({
            global: false,
            type: "GET",
            url: "/ReportMain/ReportNameDropdown"
        }).done((data) => {
            $("#filterNameListDropdown").html(data);
            $("#ReportFilterGridNameList").data('kendoComboBox').select((item) => { return item.Text === previousName; });
        });
    }
    clear() {
        var grid = $("#ReportFilterCriteriaGrid").data("kendoGrid");
        $.ajax({
            global: false,
            type: "GET",
            url: '/ReportFilterCriteria/ClearFilter',
            data: {}
        }).done((data) => {
            grid.dataSource.read();
        });
    }
    deleteRow() {
        var numToDel = $('#ReportFilterCriteriaGrid').data('kendoGrid').dataSource.data()[$($('#ReportFilterCriteriaGrid td .k-checkbox:checked').closest('tr')[0]).index()].Position;
        $.ajax({
            global: false,
            type: "POST",
            url: '/ReportFilterCriteria/UpdateFilterCache',
            data: { position: numToDel, removeEdit: true }
        }).done((data) => {
            $('#ReportFilterCriteriaGrid').data('kendoGrid').dataSource.read();
        });
    }
    _addBlankRow(controller) {
        let self = this;
        self._grid = $("#ReportFilterCriteriaGrid").data('kendoGrid');
        let filter = ReportFilterController.getInstance();
        let lastrow = null;
        if (controller._grid.dataSource.view().length === 0) {
            lastrow = null;
        }
        else {
            lastrow = controller._grid.dataSource.view()[controller._grid.dataSource.view().length - 1];
        }
        let temp = _.extend({}, lastrow);
        let newRow = controller._grid.dataSource.view().length;
        if (lastrow !== null) {
            temp.forEach((item, c) => {
                if (c.endsWith("Key"))
                    temp[c] = 0;
                else if (c.endsWith("Position")) {
                    temp[c] = newRow;
                }
                else
                    temp[c] = null;
            });
        }
        controller._grid.dataSource.insert(newRow, temp).set("dirty", true);
    }
    _init() {
        let container = $('<div id="ReportFilterCriteriaGridContainer"></div>');
        $('body').append(container);
        this._filterConfig = [];
        let controller = this;
        if ($('#ReportFilterCriteriaGrid').data('kendoGrid') !== undefined) {
            controller.clear();
        }
        $('#ReportLower').on('click', function (event) {
            controller._grid = $('#ReportFilterCriteriaGrid').data('kendoGrid');
            if (event.target.id === "Clear") {
                controller.clear();
            }
            else if (event.target.id === "Save") {
                controller.save();
            }
            else if (event.target.id === "Delete") {
                controller.deleteRow();
            }
            else if (event.target.id === "FinishFilter") {
                controller.finishFilter();
            }
            else {
                controller.filterclickEvent(controller, event.target, controller._grid.select().parent().index());
            }
        });
        $('#ReportLower').on('change', function (event) {
            if (event.target.id === "ReportFilterGridNameList") {
                controller.loadReportFilter(event.target['value']);
            }
            var DescText = event.target['text'];
            var DescVal = event.target['value'];
            controller._grid = $('#ReportFilterCriteriaGrid').data('kendoGrid');
            if ($('#ReportFieldList').data('kendoComboBox') !== undefined) {
                $('#ReportFieldList').data('kendoComboBox').dataSource.data().forEach((item, i) => {
                    if (item['Text'].toLocaleUpperCase().match(event.target['value'].toLocaleUpperCase()) || item['Value'].toLocaleUpperCase().match(event.target['value'].toLocaleUpperCase())) {
                        DescText = item['Text'];
                        DescVal = item['Value'];
                    }
                });
            }
            controller.filterchangeEvent(controller, event.target.id, DescVal, DescText, controller._grid.select().parent().index(), '/ReportFilterCriteria/ValueField', function () { });
        });
    }
    _setupFilterSaveWindow() {
        $("#filterSaveWindow").remove(); //If this tag exists remove it
        let deferred = $.Deferred();
        this._filterSaveWindow = $('<div id="filterSaveWindow"></div>').kendoWindow(utils_1.kendoWindowDefaultOptions({
            appendTo: '#ReportFilterCriteriaGridContainer',
            content: '/ReportFilterCriteria/SaveFilterWindow',
            title: 'Save',
            height: 200,
            width: 500,
            refresh: () => {
                deferred.resolve();
            }
        })).data('kendoWindow');
        return deferred.promise();
    }
    _setupFilterOverwriteWindow() {
        $("#OverwriteWindow").remove();
        let self = this;
        let deferred = $.Deferred();
        this._overwriteFilterWindow = $('<div id="OverwriteWindow"></div>').kendoWindow(utils_1.kendoWindowDefaultOptions({
            appendTo: '#ReportFilterCriteriaGridContainer',
            content: '/ReportFilterCriteria/OverwriteFilterWindow',
            title: 'Overwrite',
            resizable: false,
            height: 150,
            width: 400,
            refresh: () => {
                deferred.resolve();
            }
        })).data('kendoWindow');
        return deferred.promise();
    }
    _setupReportViewWindow() {
        $("#reportViewWindow").remove();
        $("#reportViewArea").remove();
        let deferred = $.Deferred();
        this._reportViewWindow = $('<div id="reportViewWindow"></div>').kendoWindow(utils_1.kendoWindowDefaultOptions({
            appendTo: '#ReportWindowContainer',
            content: '/ReportFilterCriteria/FinishFilter',
            title: 'Report Viewer',
            height: 900,
            width: 900,
            refresh: () => {
                deferred.resolve();
            }
        })).data(('kendoWindow'));
        return deferred.promise();
    }
}
Object.defineProperty(exports, "__esModule", { value: true });
exports.default = ReportFilterController;
$(document).ready(() => {
    try {
        if (REPORT_GRID !== null && REPORT_GRID) {
            window['ReportFilterCriteriaGridEdit'] = () => {
                ReportFilterController.getInstance().editCell(event);
            };
            window['ReportFilterCriteriaGridDataBound'] = () => {
                ReportFilterController.getInstance().setGrid('#ReportListGrid');
            };
        }
    }
    catch (err) {
        window.console.log(`no grid to load; or grid is set in view model.`);
    }
});
//# sourceMappingURL=report-filter.controller.js.map