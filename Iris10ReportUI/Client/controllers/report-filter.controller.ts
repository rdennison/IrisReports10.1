import * as _ from 'lodash';

// app

import { kendoWindowDefaultOptions } from '../utils';

interface FilterRowConfig {
    row: number;
    operatorType: string;
    valueoneType: string;
    valuetwoType: string;
    valueList: string;
}

export default class ReportFilterController {

    private static _instance: ReportFilterController;

    static getInstance() {
        if (!this._instance) {
            this._instance = new ReportFilterController();
        }
        return this._instance;
    }
    private static _filterSelector: kendo.ui.DropDownList;

    filterName = '';
    curPositionforedit = -1;
    private _grid: kendo.ui.Grid;
    private _parent: ReportFilterController;
    private _filterWindow: kendo.ui.Window;
    private _filterMessageWindow: kendo.ui.Window;
    private _filterConfig: FilterRowConfig[];
    private _descriptionvalue: string;
    private _messageType: string;


    setFilter(filter: string, userSelected = false) {
        if (!userSelected) {
            let dataSource = ReportFilterController._filterSelector.dataSource;
            if (!_.find(dataSource.data(), { Text: filter })) {
                dataSource.add({ Text: filter, Value: filter });
            }
            ReportFilterController._filterSelector.select((item) => {
                return item.Text === filter;
            });
        }
        this.filterName = filter;
    }

    setParent(controller: ReportFilterController) {
        this._parent = controller;
        return this;
    }

    openWindow() {
        this._filterWindow.center().open();
    }



    filterConfiguration(controller, row, operator = "", val1 = "", val2 = "") {
        let Configuration: FilterRowConfig = { row: row, operatorType: operator, valueoneType: val1, valuetwoType: val2, valueList: "" };

        if (controller._filterConfig.length > row) {
            if (operator !== "")
                controller._filterConfig[row].operatorType = operator;
            if (val1 !== "")
                controller._filterConfig[row].valueoneType = val1;
            //if (val2 !== "")
            controller._filterConfig[row].valuetwoType = val2;
        }
        else {
            controller._filterConfig.push(Configuration);
        }
    }


    foreignKeyValueDropdown(selectedValue, type) {
        $.ajax({
            global: false,
            type: "GET",
            url: 'ForeignKeyValue',
            data: { field: selectedValue }
        }).done((data) => {
            if (type === 0)
                $("#DdValue1").data('kendoComboBox').dataSource.data(data);
            if (type === 1)
                $("#DdValue2").data('kendoComboBox').dataSource.data(data);
            if (type === 2)
                $("#TfValue1").data('kendoComboBox').dataSource.data(data);
        });
    }

    //Deletes selected rows out of the grid and the cache
    destroyRow(e) {
        var numToDel = $('#FilterWindow td .k-checkbox:checked').length;
        var rows = [];
        for (var i = 0; i < numToDel; i++) {
            rows.push($('#FilterWindow .k-grid').data('kendoGrid').dataSource.data()[$($('#FilterWindow td .k-checkbox:checked').closest('tr')[i]).index()]);
        }
        for (var b = 0; b < rows.length; b++) {
            $('#FilterWindow .k-grid').data('kendoGrid').dataSource.data().remove(rows[b]);
        }
        $.ajax({
            global: false,
            type: "POST",
            url: '/Filter/UpdateFilterCache',
            data: { data: JSON.stringify(rows), removeEdit: true }
        });
    }

    //edits selected rows, one at a time
    editRow(e) {
        var numToEdit = $('#FilterWindow td .k-checkbox:checked').length;
        var rows = [];
        for (var i = 0; i < numToEdit; i++) {
            rows.push($('#FilterWindow .k-grid').data('kendoGrid').dataSource.data()[$($('#FilterWindow td .k-checkbox:checked').closest('tr')[i]).index()]);
        }
        $('#FilterWindow td .k-checkbox:checked').prop("checked", false);
        $("#ReportFilterCriteriaGrid").data('kendoGrid').clearSelection();
        $("#ReportFilterCriteriaGrid").data('kendoGrid').select($("#ReportFilterCriteriaGrid").data('kendoGrid').tbody.children()[rows[0].Position]);
        this.curPositionforedit = rows[0].Position;
        this._hideValues();
        this._hideOps();
        $('#DescriptionDropdown').data('kendoComboBox').select((item) => {
            return item.Text === rows[0].ColumnName;
        });
        $('#AndOrDd').data('kendoDropDownList').select((item) => {
            return item.Text === rows[0].AndOr;
        });
        this.filterchangeEvent(this, "DescriptionDropdown", JSON.parse(rows[0].InList).ColumnName, rows[0].ColumnName, 0, 'ValueField');
        $('#Group1').data('kendoMaskedTextBox').value(rows[0].OpenGroup);
        $('#Group2').data('kendoMaskedTextBox').value(rows[0].CloseGroup);
        $('#EditFilterRow').data('kendoButton').wrapper.hide();
        $('#CancelEdit').data('kendoButton').wrapper.show();
    }

    applyFilterRow() {
        var self = this;
        var group1 = $("#Group1").data('kendoMaskedTextBox').value();
        var group2 = $("#Group2").data('kendoMaskedTextBox').value();
        var descriptiontext = $("#DescriptionDropdown").data('kendoComboBox').text();
        var description = $("#DescriptionDropdown").data('kendoComboBox').value();
        var andor = $("#AndOrDd").data('kendoDropDownList').value();
        var operator = $("#" + this._filterConfig[0].operatorType).data('kendoComboBox').text();
        var value1, value1text, value2, value2text;

        switch (this._filterConfig[0].valueoneType) {
            case "DdValue1":
                value1 = $('#DdValue1').data('kendoComboBox').value();
                value1text = $('#DdValue1').data('kendoComboBox').text();
                break;
            case "TfValue1":
                value1 = $('#TfValue1').data('kendoComboBox').value();
                value1text = $('#DdValue1').data('kendoComboBox').text();
                break;
            case "DateValue1":
                value1 = new Date($('#DateValue1').data('kendoDatePicker').value()).toLocaleDateString();
                value1text = value1;
                break;
            case "NumberValue1":
                value1 = $('#NumberValue1').data('kendoNumericTextBox').value();
                value1text = value1;
                break;
            case "TextValue1":
                value1 = $('#TextValue1').data('kendoMaskedTextBox').value();
                value1text = value1;
                break;
        }
        switch (this._filterConfig[0].valuetwoType) {
            case "DdValue2":
                value2 = $('#DdValue2').data('kendoComboBox').value();
                value2text = $('#DdValue2').data('kendoComboBox').text();
                break;
            case "DateValue2":
                value2 = new Date($('#DateValue2').data('kendoDatePicker').value()).toLocaleDateString();
                value2text = value2;
                break;
            case "NumberValue2":
                value2 = $('#NumberValue2').data('kendoNumericTextBox').value();
                value2text = value2;
                break;
            case "TextValue2":
                value2 = $('#TextValue2').data('kendoMaskedTextBox').value();
                value2text = value2;
                break;
        }

        $.ajax({
            global: false,
            type: "GET",
            url: 'SetGrid',
            data: { group1: group1, description: description, operator1: operator, val1: value1, val2: value2, group2: group2, operator2: andor, descriptiontext: descriptiontext, value1text: value1text, value2text: value2text, position: self.curPositionforedit }
        }).done((data) => {
            $("#ReportFilterCriteriaGrid").data('kendoGrid').dataSource.read();
            this._hideValues();
            this._hideOps();
            this._clearDescription();
            console.log('stop here');
            self.curPositionforedit = -1;
            $('#EditFilterRow').data('kendoButton').wrapper.show();
            $('#CancelEdit').data('kendoButton').wrapper.hide();
        });
    }

    finishFilter(filter, filterfrommain = false) {
        var currentFilter = "";
        var self = this;
        if (!filterfrommain)
            currentFilter = $('#AvailableFilterList').data('kendoComboBox').text();
        else
            currentFilter = filter;

        $.ajax({
            global: false,
            type: "POST",
            url: '/Filter/FinishFilter',
            data: { filterName: currentFilter, frompage: filterfrommain }
        }).done((data) => {
            if (!filterfrommain && data === "valid") {
                $("#ReportFilterCriteriaGrid").data('kendoGrid').dataSource.read();
                ReportFilterController.getInstance()._grid.dataSource.read();
                self._filterWindow.close();
                this._hideValues();
                this._hideOps();
            } else if (data !== "valid") {
                $("#filterWarningMessage").empty();
                $("#filterWarningMessage").append(data);
                self._filterMessageWindow.center().open();
                this._messageType = "apply";
            } else {
                ReportFilterController.getInstance()._grid.dataSource.read();
            }
        });
    }

    askSave() {
        $.ajax({
            global: false,
            type: "GET",
            url: '/ReportFilterCriteria/SaveValidation',
            data: { filterName: $('#AvailableFilterList').data('kendoComboBox').text() }
        }).done((data) => {
            if (data !== "saved") {
                $("#filterWarningMessage").empty();
                $("#filterWarningMessage").append(data);
                this._filterMessageWindow.center().open();
                this._messageType = "save";

            }

        });
    }

    saveFilter() {
        $.ajax({
            global: false,
            type: "POST",
            url: '/ReportFilterCriteria/SaveFilter',
            data: { filterName: $('#AvailableFilterList').data('kendoComboBox').text() }
        }).done((data) => {
            
        });
    }

    askDelete() {
        $("#filterWarningMessage").empty();
        $("#filterWarningMessage").append("Are you sure you want to remove ", $('#AvailableFilterList').data('kendoComboBox').text());
        this._filterMessageWindow.center().open();
        this._messageType = "delete";
    }

    deleteFilter() {
        var removeItem = $('#AvailableFilterList').data('kendoComboBox').text();
        $.ajax({
            global: false,
            type: "POST",
            url: '/ReportFilterCriteria/RemoveFilter',
            data: { filterName: removeItem }
        }).done((data) => {
            let filterWinDropDown = $('#AvailableFilterList').data('kendoComboBox').dataSource.data();
            let mainScreenFilter = $('#AvailableUserFilters').data('kendoDropDownList').dataSource.data();
            $("#ReportFilterCriteriaGrid").data('kendoGrid').dataSource.read();
            this._hideValues();
            this._hideOps();
            for (var i = 0; i < filterWinDropDown.length; i++) {
                if (filterWinDropDown[i].Text == removeItem)
                    $('#AvailableFilterList').data('kendoComboBox').dataSource.remove(filterWinDropDown[i]);
            }
            $('#AvailableFilterList').data('kendoDropDownList').select((item) => {
                return item.Text === "--Select--";
            });
            if ($('#AvailableUserFilters') !== undefined) {
                for (var i = 0; i < mainScreenFilter.length; i++) {
                    if (mainScreenFilter[i].Text == removeItem)
                        $('#AvailableUserFilters').data('kendoDropDownList').dataSource.remove(mainScreenFilter[i]);
                }
                $('#AvailableUserFilters').data('kendoDropDownList').select((item) => {
                    return item.Text === "--Select--";
                });
            }
        });
    }

    clearFilter() {
        if (this.curPositionforedit != -1) {
            this.curPositionforedit = -1;
            this._hideValues();
            this._hideOps();
            this._clearDescription();
        }
        $.ajax({
            global: false,
            type: "GET",
            url: '/ReportFilterCriteria/ClearFilter',
            data: {}
        }).done((data) => {
            $("#ReportFilterCriteriaGrid").data('kendoGrid').dataSource.read();
        });
    }

    //cancelEdit() {
    //    if (this.curPositionforedit != -1) {
    //        this.curPositionforedit = -1;
    //        this._hideValues();
    //        this._hideOps();
    //        this._clearDescription();
    //    }
    //    $("#ReportFilterCriteriaGrid").data('kendoGrid').clearSelection();
    //    $('#EditFilterRow').data('kendoButton').wrapper.show();
    //    $('#CancelEdit').data('kendoButton').wrapper.hide();
    //}

    filterchangeEvent(controller, dropdown, selectValue, textVal, row, url) {
        //let filter = GridFilterController.getInstance();
        switch (dropdown) {
            case 'DescriptionDropdown':
                controller._hideOps();
                controller._hideValues();
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
                            $("#ddListoptions").data('kendoComboBox').wrapper.show();
                            $("#DdValue1").data('kendoComboBox').wrapper.show();
                            controller.foreignKeyValueDropdown(selectValue, 0);
                            controller.filterConfiguration(controller, 0, "ddListoptions", "DdValue1", "");
                            break;
                        case "Date":
                            $("#dListoptions").data('kendoComboBox').wrapper.show();
                            $("#DateValue1").data('kendoDatePicker').wrapper.show();
                            controller.filterConfiguration(controller, 0, "dListoptions", "DateValue1", "");
                            break;
                        case "Text":
                            $("#tListoptions").data('kendoComboBox').wrapper.show();
                            $("#TextValue1").data('kendoMaskedTextBox').wrapper.show();
                            controller.filterConfiguration(controller, 0, "tListoptions", "TextValue1", "");
                            break;
                        case "Number":
                            $("#nListoptions").data('kendoComboBox').wrapper.show();
                            $("#NumberValue1").data('kendoNumericTextBox').wrapper.show();
                            controller.filterConfiguration(controller, 0, "nListoptions", "NumberValue1", "");
                            break;
                        case "Dropdown2":
                            $("#tfListoptions").data('kendoComboBox').wrapper.show();
                            $("#TfValue1").data('kendoComboBox').wrapper.show();
                            controller.foreignKeyValueDropdown(selectValue, 2);
                            controller.filterConfiguration(controller, 0, "tfListoptions", "TfValue1", "");
                            break;
                        default: break;
                    }
                });
                break;
            case 'AndOrDd':
                break;
            case 'ddListoptions':
            case 'dListoptions':
            case 'nListoptions':
            case 'tListoptions':
            case 'tfListoptions':
                controller._hideValues(); //new op choosen so show new values
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
                                $("#DdValue1").data('kendoComboBox').wrapper.show();
                                $("#DdValue2").data('kendoComboBox').wrapper.show();
                                controller.foreignKeyValueDropdown(controller._descriptionvalue, 1); //first one was already filled so we can ignore it
                                controller.filterConfiguration(controller, 0, "", "DdValue1", "DdValue2");
                                break;
                            case "Date":
                                $("#DateValue1").data('kendoDatePicker').wrapper.show();
                                $("#DateValue2").data('kendoDatePicker').wrapper.show();
                                controller.filterConfiguration(controller, 0, "", "DateValue1", "DateValue2");
                                break;
                            case "Number":
                                $("#NumberValue1").data('kendoNumericTextBox').wrapper.show();
                                $("#NumberValue2").data('kendoNumericTextBox').wrapper.show();
                                controller.filterConfiguration(controller, 0, "", "NumberValue1", "NumberValue2");
                                break;
                            case "Text":
                                $("#TextValue1").data('kendoMaskedTextBox').wrapper.show();
                                $("#TextValue2").data('kendoMaskedTextBox').wrapper.show();
                                controller.filterConfiguration(controller, 0, "", "TextValue1", "TextValue2");
                                break;
                            default: break;
                        }
                    });
                }
                else if (selectValue === "StartsWith" || selectValue === "EndsWith" || selectValue === "Like" || selectValue === "NotLike" || selectValue === "Contains") {
                    controller._hideValues();
                    $("#TextValue1").data('kendoMaskedTextBox').wrapper.show();
                    controller.filterConfiguration(controller, 0, "", "TextValue1", "");
                }
                else {
                    if (dropdown == "ddListoptions") {
                        $("#DdValue1").data('kendoComboBox').wrapper.show();
                        controller.filterConfiguration(controller, 0, "", "DdValue1", "");
                    }
                    if (dropdown == "dListoptions") {
                        $("#DateValue1").data('kendoDatePicker').wrapper.show();
                        controller.filterConfiguration(controller, 0, "", "DateValue1", "");
                    }
                    if (dropdown == "nListoptions") {
                        $("#NumberValue1").data('kendoNumericTextBox').wrapper.show();
                        controller.filterConfiguration(controller, 0, "", "NumberValue1", "");
                    }
                    if (dropdown == "tListoptions") {
                        $("#TextValue1").data('kendoMaskedTextBox').wrapper.show();
                        controller.filterConfiguration(controller, 0, "", "TextValue1", "");
                    }
                    if (dropdown == "tfListoptions") {
                        $("#TfValue1").data('kendoComboBox').wrapper.show();
                        controller.filterConfiguration(controller, 0, "", "TfValue1", "");
                    }
                }
                break;
        }
    }

    filterYes() {
        switch (this._messageType) {
            case "apply":
                this._filterMessageWindow.close();
                this._filterWindow.close();
                ReportFilterController.getInstance()._grid.dataSource.read();
                break;
            case "delete":
                this._filterMessageWindow.close();
                this.deleteFilter();
                break;
            case "save":
                this._filterMessageWindow.close();
                this.saveFilter();
                break;
        }
    }

    filterNo() {
        this._filterMessageWindow.close();
    }

    private _hideOps() {
        $("#ddListoptions").data('kendoComboBox').select(0);
        $("#ddListoptions").data('kendoComboBox').wrapper.hide();
        $("#dListoptions").data('kendoComboBox').select(0);
        $("#dListoptions").data('kendoComboBox').wrapper.hide();
        $("#nListoptions").data('kendoComboBox').select(0);
        $("#nListoptions").data('kendoComboBox').wrapper.hide();
        $("#tListoptions").data('kendoComboBox').select(0);
        $("#tListoptions").data('kendoComboBox').wrapper.hide();
        $("#tfListoptions").data('kendoComboBox').select(0);
        $("#tfListoptions").data('kendoComboBox').wrapper.hide();
    }

    private _clearDescription() {
        $('#DescriptionDropdown').data('kendoComboBox').value("");
    }

    private _hideValues() {
        $("#DdValue1").data('kendoComboBox').value('');
        $("#DdValue1").data('kendoComboBox').wrapper.hide();
        $("#TfValue1").data('kendoComboBox').value('');
        $("#TfValue1").data('kendoComboBox').wrapper.hide();
        $("#TextValue1").data('kendoMaskedTextBox').value('');
        $("#TextValue1").data('kendoMaskedTextBox').wrapper.hide();
        $("#NumberValue1").data('kendoNumericTextBox').value('');
        $("#NumberValue1").data('kendoNumericTextBox').wrapper.hide();
        $("#DateValue1").data('kendoDatePicker').value(new Date(Date.now()))
        $("#DateValue1").data('kendoDatePicker').wrapper.hide();
        $("#DdValue2").data('kendoComboBox').value('');
        $("#DdValue2").data('kendoComboBox').wrapper.hide();
        $("#TextValue2").data('kendoMaskedTextBox').value('');
        $("#TextValue2").data('kendoMaskedTextBox').wrapper.hide();
        $("#NumberValue2").data('kendoNumericTextBox').value('');
        $("#NumberValue2").data('kendoNumericTextBox').wrapper.hide();
        $("#DateValue2").data('kendoDatePicker').value(new Date(Date.now()))
        $("#DateValue2").data('kendoDatePicker').wrapper.hide();
        $("#Group1").data('kendoMaskedTextBox').value('');
        $("#Group2").data('kendoMaskedTextBox').value('');
    }

    private constructor() {
        this._init();
    }

    private _init() {
        let container = $('<div id="FilterWindowContainer"></div>');
        let self = this;
        $('body').append(container);
        this._filterConfig = [];
        

        //    this._setupFilterWindow().then(() => {
        //        this._setupFilterMessageWindow();
        //        this._grid = $('#FilterWindow .k-grid').data('kendoGrid');
        //        kendo.bind($('#FilterWindowContainer'), this);

        //        $('#allchecked').change(function (ev) {
        //            ev.stopPropagation();
        //            var checked = ev.target['checked'];
        //            $('.k-checkbox').each(function (idx, item) {
        //                if (checked) {

        //                    if (!($(item).closest('tr').is('.k-state-selected'))) {
        //                        $(item).click();
        //                    } else {
        //                        if (!item['checked'])
        //                            item['checked'] = true; //for some reason kendo selects the first row without checking the box.  This sets it to checked for display and calculation purposes.
        //                    }
        //                } else {
        //                    if ($(item).closest('tr').is('.k-state-selected')) {
        //                        $(item).click();
        //                    }
        //                }
        //            });
        //        });

        //        let controller = this;

        //        $('#FilterWindow').on('change', function (this: HTMLElement, event: JQueryEventObject) {
        //            var DescVal = event.target['value'];
        //            controller.filterchangeEvent(controller, event.target.id, DescVal, "", controller._grid.select().parent().index(), 'ValueField');
        //        });
        //    });
        //}

        //private _setupFilterWindow() {
        //    let deferred = $.Deferred<void>();
        //    this._filterWindow = $('<div id="FilterWindow"></div>').kendoWindow(kendoWindowDefaultOptions({
        //        appendTo: '#FilterWindowContainer',
        //        content: '/Toolbar/FilterWindow',
        //        title: 'IRIS Grid Filter',
        //        resizable: true,
        //        scrollable: true,
        //        height: 820,
        //        width: 850,
        //        error: function (e) {
        //            alert("Error: " + e.status)
        //        },
        //        refresh: () => {
        //            deferred.resolve();
        //        }
        //    })).data('kendoWindow');

        //    return deferred.promise();
        //}

        //private _setupFilterMessageWindow() {
        //    $("#filterMessageWindow").remove(); //If this tag exists remove it
        //    let deferred = $.Deferred<void>();
        //    this._filterMessageWindow = $('<div id="filterMessageWindow"></div>').kendoWindow(kendoWindowDefaultOptions({
        //        appendTo: '#FilterWindowContainer',
        //        content: '/Filter/FilterMessageWindow',
        //        title: 'Warning',
        //        height: 200,
        //        width: 500,
        //        refresh: () => {
        //            deferred.resolve();
        //        }
        //    })).data('kendoWindow');
        //    return deferred.promise();
        //}
    }
}