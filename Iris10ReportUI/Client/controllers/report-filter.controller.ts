declare var REPORT_GRID

// libs
import * as _ from 'lodash';

// app

import { kendoWindowDefaultOptions } from '../utils';
//import GridController from './grid.controller';

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

    static get filterSelector() {
        if (!ReportFilterController._filterSelector) {
            ReportFilterController._filterSelector = $('#AvailableUserFilters2').data('kendoDropDownList');
        }
        return ReportFilterController._filterSelector;
    }

    filterName = '';
    private _grid: kendo.ui.Grid;
    private _parent: ReportFilterController;
    private _ReportFilterCriteriaGrid: kendo.ui.Window;
    private _reportViewWindow: kendo.ui.Window;
    private _filterConfig: FilterRowConfig[];
    private _descriptionvalue: string;
    private _filterConfirmWindow: kendo.ui.Window;
    private _filterConfirmRemoveWindow: kendo.ui.Window;
    private _filterOverwriteWindow: kendo.ui.Window;
    private _editMode: boolean = true;

    /**
    * Close the modals
    */
    private _closeModals() {

        this._filterConfirmWindow.close();
        this._filterConfirmRemoveWindow.close();
        this._filterOverwriteWindow.close();

    }

    setFilter(filter: string, userSelected = false) {
        if (!userSelected) {
            let dataSource = ReportFilterController.filterSelector.dataSource;
            if (!_.find(dataSource.data(), { Text: filter })) {
                dataSource.add({ Text: filter, Value: filter });
            }
            ReportFilterController.filterSelector.select((item) => {
                return item.Text === filter;
            });
        }
        this.filterName = filter;
        this.switchFilter(filter);
    }

    setParent(controller: ReportFilterController) {
        console.log(`currently in setParent`);
        this._parent = controller;
        return this;
    }

    cancel() {
        this._closeModals();
    }

    switchFilter(gridFilterKey: string) {
        this.filterName = gridFilterKey;

    }

    recordSelect(e, self) {
        var uid = e.target.parentElement.parentElement.dataset.uid;
        var rw = self._grid.tbody.find('tr[data-uid="' + uid + '"]');
        self._grid.select(rw);
        return false;
    }

    activateFilter() {
        var self = this;

        $.ajax({
            global: false,
            type: "GET",
            url: "/ReportFilterCriteria/ActivateFilter",
            data: {  }
        }).done((data) => {
            if (data.length >= 1) {
                data.forEach((item, c) => {
                    self._addBlankRow(self);

                    $('#ReportFilterCriteriaGrid').data('kendoGrid').dataSource.data()[0]["ColumnName"] = item["ColumnName"];
                
                });

            }
         
        });

    }

   

    //apply() {
    //    let tempFilter = [];
    //    var grid = $('#ReportFilterCriteriaGrid').data('kendoGrid');
    //    var filterName = $('#AvailableUserFilters2').data('kendoDropDownList').text();
    //    for (var i = 0; i < grid.dataSource.view().length; i++) {
    //        if (grid.dataSource.view()[i]["Description"] !== null && grid.dataSource.view()[i]["Description"] !== "") {
    //            tempFilter.push(JSON.stringify(grid.dataSource.view()[i]));
    //        }
    //    }
    //    $.ajax({
    //        global: false,
    //        type: "POST",
    //        url: "/Filter/ApplyFilter",
    //        data: { filterString: tempFilter }
    //    }).done((data) => {
    //        var ErrorMessage = data;
    //        if (ErrorMessage.IsError !== null && ErrorMessage.IsError === true) {
    //            alert(ErrorMessage.Message);
    //        }


    //        else {
    //            this._ReportFilterCriteriaGrid.center().close();
    //            this._parent.grid.dataSource.read();

    //            for (var i = 0; i < GridController.gridFilterSelector.items().length; i++) {
    //                if (filterName === GridController.gridFilterSelector.items()[i].innerText) {

    //                    GridController.gridFilterSelector.select((item) => {
    //                        return item.Text === filterName;
    //                    });

    //                }
    //            }




    //            //if (valuetwo !== "")
    //            //    document.getElementById("filtertextdisplay").innerHTML = description + " " + operator + " " + valueone + " and " + valuetwo;
    //            //else
    //            //    document.getElementById("filtertextdisplay").innerHTML = description + " " + operator + " " + valueone;
    //            document.getElementById("filtertextdisplay").innerHTML = "";
    //            for (var i = 0; i < data.length; i++) {
    //                document.getElementById("filtertextdisplay").innerHTML += data[i]["Filter"] + " ";
    //            }




    //        }

    //    });

    //    return GridController.gridFilterSelector;
    //}

    //confirmFilterDelete() {
    //    this._filterConfirmRemoveWindow.center().open();
    //}

    //removeFilter() {
    //    let self = this;
    //    var grid = $("#ReportFilterCriteriaGrid").data("kendoGrid");
    //    const removeItem = $('#AvailableUserFilters2').data('kendoDropDownList').text();

    //    $.ajax({
    //        global: false,
    //        type: "POST",
    //        url: "/Filter/RemoveFilter",
    //        data: { filterName: removeItem },
    //    }).done((data) => {
    //        let filterWinDropDown = GridFilterController.filterSelector.dataSource.data();
    //        let mainScreenFilter = GridController.gridFilterSelector.dataSource.data();
    //        var ErrorMessage = data;
    //        if (ErrorMessage.IsError !== null && ErrorMessage.IsError === true) {
    //            alert(ErrorMessage.Message);
    //        }
    //        else {
    //            for (var i = 0; i < filterWinDropDown.length; i++) {
    //                if (filterWinDropDown[i].Text == removeItem)
    //                    GridFilterController.filterSelector.dataSource.remove(filterWinDropDown[i]);
    //            }

    //            for (var i = 0; i < mainScreenFilter.length; i++) {
    //                if (mainScreenFilter[i].Text == removeItem)
    //                    GridController.gridFilterSelector.dataSource.remove(mainScreenFilter[i]);
    //            }
    //            $('#AvailableUserFilters2').data('kendoDropDownList').select((item) => {
    //                return item.Text === "--Select--";
    //            });

    //            self._closeModals();
    //            self.clear();
    //        }
    //    });


    //}

    //private _confirmFilterSave() {
    //    this._filterConfirmWindow.center().open();

    //    let saveComboBox = $("#filterName").data('kendoComboBox').dataSource.data();
    //    let filterWinDropDown = GridFilterController.filterSelector.dataSource.data();

    //    for (var i = 0; i < saveComboBox.length; i++) {
    //        $("#filterName").data('kendoComboBox').dataSource.remove(saveComboBox[i]);
    //    }
    //    for (var a = 0; a < filterWinDropDown.length; a++) {
    //        $("#filterName").data('kendoComboBox').dataSource.add({ Text: filterWinDropDown[a].Text, Value: filterWinDropDown[a].Value });
    //    }
    //}

    loadFilterResults(gridFilterKey: string) {
        let mainController = this;
        $.ajax({
            global: false,
            type: "GET",
            url: "/Filter/LoadFilterResults",
            data: { filterName: gridFilterKey }
        }).done((data) => {
            //mainController._parent.grid.dataSource.read();
            //document.getElementById("filtertextdisplay").innerHTML = "";
            //for (var i = 0; i < data.length; i++) {
            //    document.getElementById("filtertextdisplay").innerHTML += data[i]["Filter"] + " ";
            //}
        });
    }

    //Still doesn't load filter criteria consistently, needs to be examined further.


    //save() {
    //    var filterDropdownOverwrite = $('#AvailableUserFilters2').data('kendoDropDownList');

    //    if (filterDropdownOverwrite.select() > 0) {
    //        this.overWrite();
    //    }
    //    else {
    //        this._confirmFilterSave();
    //    }
    //}

    overWrite() {
        this._filterOverwriteWindow.center().open();
        this.filterName = $('#AvailableUserFilters2').data('kendoDropDownList').text();
    }

    confirmed() {
        var self = this;
        let tempFilter = [];
        var grid = $('#ReportFilterCriteriaGrid').data('kendoGrid');
        for (var i = 0; i < grid.dataSource.view().length; i++) {
            if (grid.dataSource.view()[i]["Description"] !== null && grid.dataSource.view()[i]["Description"] !== "") {
                tempFilter.push(JSON.stringify(grid.dataSource.view()[i]));
            }

        }
        $.ajax({
            global: false,
            type: "POST",
            url: "/Filter/SaveFilter",
            data: { filterName: self.filterName, saveFilter: tempFilter }
        }).done((data) => {
            var currentlyExists = false;
            var ErrorMessage = data;
            if (ErrorMessage.IsError !== null && ErrorMessage.IsError === true) {
                alert(ErrorMessage.Message);
            }
            else {
                for (var i = 0; i < ReportFilterController.filterSelector.items().length; i++) {
                    if (data === ReportFilterController.filterSelector.items()[i].innerText) {
                        currentlyExists = true;
                    }
                }

                if (!currentlyExists) {
                    ReportFilterController.filterSelector.dataSource.add({ Text: data, Value: data });

                }

                self.setFilter(data);
                ReportFilterController.filterSelector.select((item) => {
                    return item.Text === data;
                });

                //self._parent.grid.dataSource.read();
                self._closeModals();
                //self._ReportFilterCriteriaGrid.close();
            }
        });

    }

    foreignKeyValueDropdown(controller, selectedValue, currentRow) {
        $.ajax({
            global: false,
            type: "GET",
            url: 'ForeignKeyValue',
            data: { field: selectedValue }
        }).done((data) => {
            controller._filterConfig[currentRow].valueList = data;
        });
    }



    //clear() {
    //    var grid = $("#ReportFilterCriteriaGrid").data("kendoGrid");
    //    grid.dataSource.read();
    //    document.getElementById("filtertextdisplay").innerHTML = "";
    //    //for (var i = 0; i < data.length; i++) {
    //    //    document.getElementById("filtertextdisplay").innerHTML += data[i]["Filter"] + " ";
    //    //}
    //}

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

    //clearGrid() {
    //    let self = GridFilterController.getInstance();
    //    self._grid.dataSource.view().empty();
    //    //document.getElementById("filtertextdisplay").innerHTML = "";
    //    //for (var i = 0; i < data.length; i++) {
    //    //    document.getElementById("filtertextdisplay").innerHTML += data[i]["Filter"] + " ";
    //    //}
    //}

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

    filterclickEvent(controller, clicked, row) {
        var curIndex = 0;
        if (clicked.cellIndex !== undefined)
            curIndex = clicked.cellIndex;
        else {
            try {
                curIndex = clicked.index();
            } catch (ex) { }
        }
        if (curIndex !== 0) {
            var curCell = controller._grid.tbody.find(">tr:eq(" + row + ") >td:eq(" + curIndex + ")");
            curCell.addClass('k-state-focused');
            controller._grid.select(curCell);
            controller._grid.editCell(curCell);
        }
        switch (curIndex) {
            case 1:
                $("#Group1").data('kendoMaskedTextBox').wrapper.show();
                break;
            case 2:
                break;
            case 3:
                try {
                    $("#" + controller._filterConfig[row].operatorType).data('kendoComboBox').wrapper.show();
                    ReportFilterController.getInstance().comboSelection(controller._grid);
                } catch (ex) { }
                break;
            case 4:
                try {
                    if (controller._filterConfig[row].valueoneType === "DropdownValues" || controller._filterConfig[row].valueoneType === "DropdownValues2") {
                        (<any>$("#" + controller._filterConfig[row].valueoneType)).getKendoComboBox().dataSource.data(controller._filterConfig[row].valueList);
                        (<any>$("#" + controller._filterConfig[row].valueoneType)).getKendoComboBox().dataSource.query();
                        $("#" + controller._filterConfig[row].valueoneType).data('kendoComboBox').wrapper.show();
                        ReportFilterController.getInstance().comboSelection(controller._grid);
                    } else if (controller._filterConfig[row].valueoneType === "TextBox") {
                        $("#" + controller._filterConfig[row].valueoneType).data('kendoMaskedTextBox').wrapper.show();
                    } else if (controller._filterConfig[row].valueoneType === "NumberBox") {
                        $("#" + controller._filterConfig[row].valueoneType).data('kendoNumericTextBox').wrapper.show();
                    } else if (controller._filterConfig[row].valueoneType === "DateValues") {
                        $("#" + controller._filterConfig[row].valueoneType).data('kendoDatePicker').wrapper.show();
                    }
                    $("#" + controller._filterConfig[row].operatorType).data('kendoComboBox').wrapper.show();


                } catch (ex) { }
                break;
            case 5:
                try {
                    if (controller._filterConfig[row].valuetwoType === "DropdownValues3") {
                        (<any>$("#" + controller._filterConfig[row].valuetwoType)).getKendoComboBox().dataSource.data(controller._filterConfig[row].valueList);
                        (<any>$("#" + controller._filterConfig[row].valuetwoType)).getKendoComboBox().dataSource.query();
                        $("#" + controller._filterConfig[row].valuetwoType).data('kendoComboBox').wrapper.show();
                        ReportFilterController.getInstance().comboSelection(controller._grid);
                    } else if (controller._filterConfig[row].valuetwoType === "TextBox2") {
                        $("#" + controller._filterConfig[row].valuetwoType).data('kendoMaskedTextBox').wrapper.show();
                    } else if (controller._filterConfig[row].valuetwoType === "NumberBox2") {
                        $("#" + controller._filterConfig[row].valuetwoType).data('kendoNumericTextBox').wrapper.show();
                    } else if (controller._filterConfig[row].valuetwoType === "DateValues2") {
                        $("#" + controller._filterConfig[row].valuetwoType).data('kendoDatePicker').wrapper.show();
                    }
                } catch (ex) { }
                break;
            case 6:
                $("#Group2").data('kendoMaskedTextBox').wrapper.show();
                break;
            case 7:
                $("#Operator2").data('kendoDropDownList').wrapper.show();
                break;
            default:
                break;
        }
    }

    filterchangeEvent(controller, dropdown, selectValue, textVal, row, url) {
        let filter = ReportFilterController.getInstance();
        controller._grid.current(controller._grid.select());
        var cellIndex;
        try {
            cellIndex = controller._grid.current().index();
        }
        catch (ex) { }
        switch (dropdown) {
            case 'FieldList':
                if (controller._grid.dataSource.view()[row]["FieldList_TEMP"] !== undefined)
                    delete controller._grid.dataSource.view()[row]["FieldList_TEMP"];
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
                            break;
                        case "Date":
                            filter.filterConfiguration(controller, row, "operatordateListoptions", "DateValues", "");
                            break;
                        case "Text":
                            filter.filterConfiguration(controller, row, "operatortextListoptions", "TextBox", "");
                            break;
                        case "Number":
                            filter.filterConfiguration(controller, row, "operatornumberListoptions", "NumberBox", "");
                            break;
                        case "Dropdown2":
                            filter.filterConfiguration(controller, row, "operatorTrueFalseoptions", "DropdownValues2", "");
                            filter.foreignKeyValueDropdown(controller, selectValue, row);
                            break;
                        default: break;
                    }
                    if (controller._grid.select().parent().index() === controller._grid.dataSource.view().length - 1) {
                        controller._grid.dataSource.view()[row]["Description"] = textVal;
                        controller._grid.dataSource.view()[row]["RecordOrder"] = controller._grid.select().parent().index();
                        controller._grid.dataSource.view()[row]["Operator2"] = "AND";
                        controller._grid.dataSource.view()[row]["Operator1"] = "";
                        controller._grid.dataSource.view()[row]["Value1"] = "";
                        controller._grid.dataSource.view()[row]["Value2"] = "";
                        controller._grid.dataSource.view()[row]["UseDropdown"] = false;
                        controller._grid.dataSource.view()[row]["UseNonUniqueList"] = false;
                        controller._grid.dataSource.view()[row]["UseLikeOperator"] = false;
                        filter._addBlankRow(controller);
                    } else {
                        controller._grid.dataSource.view()[row]["Description"] = textVal;
                        controller._grid.dataSource.view()[row]["Operator1"] = "";
                        controller._grid.dataSource.view()[row]["Value1"] = "";
                        controller._grid.dataSource.view()[row]["Value2"] = "";
                        controller._grid.refresh();
                        let curCell = controller._grid.current(controller._grid.tbody.find(">tr:eq(" + row + ") >td:eq(" + cellIndex + ")"));
                        curCell.addClass('k-state-focused');
                        controller._grid.select(curCell);
                        controller._grid.editCell(curCell);
                    }
                });
                break;
            case 'Operator2':
                break;
            case 'DropdownValues':
                if (controller._grid.dataSource.view()[row]["DropdownValues_TEMP"] !== undefined)
                    delete controller._grid.dataSource.view()[row]["DropdownValues_TEMP"];
                controller._grid.dataSource.view()[row]["Value1"] = $('#DropdownValues').data('kendoComboBox').text();
                break;
            case 'DropdownValues2':
                if (controller._grid.dataSource.view()[row]["DropdownValues2_TEMP"] !== undefined)
                    delete controller._grid.dataSource.view()[row]["DropdownValues2_TEMP"];
                controller._grid.dataSource.view()[row]["Value1"] = $('#DropdownValues2').data('kendoComboBox').text();
                break;
            case 'TextBox':
                if (controller._grid.dataSource.view()[row]["TextBox_TEMP"] !== undefined)
                    delete controller._grid.dataSource.view()[row]["TextBox_TEMP"];
                controller._grid.dataSource.view()[row]["Value1"] = $('#TextBox').data('kendoMaskedTextBox').value();
                break;
            case 'NumberBox':
                if (controller._grid.dataSource.view()[row]["NumberBox_TEMP"] !== undefined)
                    delete controller._grid.dataSource.view()[row]["NumberBox_TEMP"];
                controller._grid.dataSource.view()[row]["Value1"] = $('#NumberBox').data('kendoNumericTextBox').value();
                break;
            case 'DateValues':
                if (controller._grid.dataSource.view()[row]["DateValues_TEMP"] !== undefined)
                    delete controller._grid.dataSource.view()[row]["DateValues_TEMP"];
                controller._grid.dataSource.view()[row]["Value1"] = $('#DateValues').data('kendoDatePicker').value().toLocaleDateString("en-US");
                break;
            case 'DropdownValues3':
                if (controller._grid.dataSource.view()[row]["DropdownValues3_TEMP"] !== undefined)
                    delete controller._grid.dataSource.view()[row]["DropdownValues3_TEMP"];
                controller._grid.dataSource.view()[row]["Value2"] = $('#DropdownValues3').data('kendoComboBox').text();
                break;
            case 'TextBox2':
                if (controller._grid.dataSource.view()[row]["TextBox2_TEMP"] !== undefined)
                    delete controller._grid.dataSource.view()[row]["TextBox2_TEMP"];
                controller._grid.dataSource.view()[row]["Value2"] = $('#TextBox2').data('kendoMaskedTextBox').value();
                break;
            case 'NumberBox2':
                if (controller._grid.dataSource.view()[row]["NumberBox2_TEMP"] !== undefined)
                    delete controller._grid.dataSource.view()[row]["NumberBox2_TEMP"];
                controller._grid.dataSource.view()[row]["Value2"] = $('#NumberBox2').data('kendoNumericTextBox').value();
                break;
            case 'DateValues2':
                if (controller._grid.dataSource.view()[row]["DateValues2_TEMP"] !== undefined)
                    delete controller._grid.dataSource.view()[row]["DateValues2_TEMP"];
                controller._grid.dataSource.view()[row]["Value2"] = $('#DateValues2').data('kendoDatePicker').value().toLocaleDateString("en-US");
                break;
            case 'operatordropdownListoptions':
            case 'operatordateListoptions':
            case 'operatornumberListoptions':
            case 'operatortextListoptions':
            case 'operatorTrueFalseoptions':

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
                                filter.filterConfiguration(controller, row, "", "", "DropdownValues3");
                                break;
                            case "Date":
                                filter.filterConfiguration(controller, row, "", "", "DateValues2");
                                break;
                            case "Number":
                                filter.filterConfiguration(controller, row, "", "", "NumberBox2");
                                break;
                            case "Text":
                                filter.filterConfiguration(controller, row, "", "", "TextBox2");
                                break;
                            default: break;
                        }
                    });
                }
                else if (selectValue === "StartsWith" || selectValue === "EndsWith" || selectValue === "Like" || selectValue === "NotLike" || selectValue === "Contains") {
                    delete controller._grid.dataSource.view()[row]["DropdownValues"];
                    delete controller._grid.dataSource.view()[row]["DropdownValues2"];
                    delete controller._grid.dataSource.view()[row]["DropdownValues3"];
                    filter.filterConfiguration(controller, row, "", "TextBox", "");
                }
                else {
                    if (dropdown.match("dropdown"))
                        filter.filterConfiguration(controller, row, "", "DropdownValues", "");
                    if (dropdown.match("text"))
                        filter.filterConfiguration(controller, row, "", "TextBox", "");
                    if (controller._grid.dataSource.view()[row]["Description"].match("Date"))
                        filter.filterConfiguration(controller, row, "", "DateValues", "");
                    if (dropdown.match("number"))
                        filter.filterConfiguration(controller, row, "", "NumberBox", "");
                    if (dropdown.match("True"))
                        filter.filterConfiguration(controller, row, "", "DropdownValues2", "");

                }
                controller._grid.dataSource.view()[row]["Operator1"] = $('#' + controller._filterConfig[row].operatorType).data('kendoComboBox').text();
                //controller._grid.dataSource.view()[row]["Value1"] = "";
                //controller._grid.dataSource.view()[row]["Value2"] = "";
                controller._grid.refresh();
                let curCell = controller._grid.tbody.find(">tr:eq(" + row + ") >td:eq(" + cellIndex + ")").next();
                controller._grid.current(curCell);
                curCell.addClass('k-state-focused');
                controller._grid.select(curCell);
                break;
        }
    }

    private _addBlankRow(controller) {
        let self = this;
        self._grid = $("#ReportFilterCriteriaGrid").data('kendoGrid');

        let filter = ReportFilterController.getInstance();
        let lastrow = null;
        if (controller._grid.dataSource.view().length === 0)
        {
            lastrow = null;
        }
        else {
            lastrow = controller._grid.dataSource.view()[controller._grid.dataSource.view().length - 1];

        }
         
        let temp = _.extend({}, lastrow);
        let newRow = controller._grid.dataSource.view().length;
        if (lastrow !== null)
        {
            temp.forEach((item, c) => {
                if (c.endsWith("KeyField") || c.endsWith("RecordOrder"))
                    temp[c] = 0;
                else
                    temp[c] = null;
            });
        }
       
        controller._grid.dataSource.insert(newRow, temp).set("dirty", true);
        //$(".k-grid-recordSelecter").on('click', self.recordSelect); //TODO: Redo this
        //$(".k-grid-recordSelecter").on('click', function (this: HTMLElement, event: JQueryEventObject) {
        //    filter.recordSelect(event, controller);
        //});
    }

    private constructor() {
        this._init();
    }

    private _init() {
        let container = $('<div id="ReportFilterCriteriaGridContainer"></div>');
        let self = this;
        $('body').append(container);
        this._filterConfig = [];
        let value1: string = "";
        let value2: string = "";
    


        //this._setupReportFilterCriteriaGrid().then(() => {
        //    return this._setupConfirmWindow();

        //}).then(() => {
        //    $("#filterName").on('change', function (this: HTMLElement, event: JQueryEventObject) {
        //        let selectValue: string = event.target['value'];
        //        self.filterName = selectValue;
        //    });
        //    return this._setupDeleteConfirmationWindow();

        //}).then(() => {
        //    return this._setupFilterOverwriteWindow();

        //}).then(() => {
        this._grid = $('#ReportFilterCriteriaGrid .k-grid').data('kendoGrid');
        kendo.bind($('#ReportFilterCriteriaGridContainer'), this);

        $('#userFilterList2').appendTo('#filterPartialListDropdown');
        $('#userFilterList2').removeClass('hidden');

        let controller = this;
        let clickDescription: boolean = false;

        $('#ReportFilterCriteriaGrid').on('keydown', function (this: HTMLElement, event: JQueryEventObject) {
            let key: any = event.key;
            let keyCode: any = event.keyCode;
            var row = controller._grid.select().parent().index();

            if (keyCode === 46) {

                if (row === -1)
                    row = $('tr.k-state-selected').index();
                controller._grid.dataSource.view().splice(row, 1);
                controller._grid.dataSource.data().splice(row, 1);
                controller._filterConfig.splice(row, 1);
                controller._filterConfig.forEach((item, i) => {
                    item.row = i;
                });
                controller._grid.refresh();
            }
        });
        $('#ReportFilterCriteriaGrid').data('kendoGrid').table.on('keydown', function (this: HTMLElement, event: JQueryEventObject) {
            if (event.keyCode === kendo.keys.TAB) {
                var current = controller._grid.current();
                var row = current.parent().index();
                setTimeout(function () {
                    $('td.k-state-focused').removeClass('k-state-focused');
                    controller.filterclickEvent(controller, current, row);
                }, 100, controller, current, row);
            }
        });
        $('#ReportFilterCriteriaGrid').on('click', function (this: HTMLElement, event: JQueryEventObject) {
            controller.filterclickEvent(controller, event.target, controller._grid.select().parent().index());
        });
        $('#ReportFilterCriteriaGrid').on('change', function (this: HTMLElement, event: JQueryEventObject) {
            var DescText = event.target['text'];
            var DescVal = event.target['value'];
            if ($('#FieldList').data('kendoComboBox') !== undefined) {
                $('#FieldList').data('kendoComboBox').dataSource.data().forEach((item, i) => {
                    if (item['Text'].toLocaleUpperCase().match(event.target['value'].toLocaleUpperCase()) || item['Value'].toLocaleUpperCase().match(event.target['value'].toLocaleUpperCase())) {
                        DescText = item['Text'];
                        DescVal = item['Value'];
                    }
                });
            }
            controller.filterchangeEvent(controller, event.target.id, DescVal, DescText, controller._grid.select().parent().index(), 'ValueField');
        });
        //});
    }

   


    

}