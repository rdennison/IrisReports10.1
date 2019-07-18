
declare var REPORT_GRID;

// libs
import * as _ from 'lodash';

// app

import { kendoWindowDefaultOptions } from '../utils';
import ReportListController from './report-list.controller';

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
    private _filterSaveWindow: kendo.ui.Window;
    private _filterConfirmRemoveWindow: kendo.ui.Window;
    private _overwriteFilterWindow: kendo.ui.Window;
    private _editMode: boolean = true;
    entryValid = true;
   


    activateFilter() {
        var self = this;

        $.ajax({
            global: false,
            type: "POST",
            url: "/ReportFilterCriteria/ActivateFilter",
            data: {  }
        }).done((data) => {
           
   
            
         
        });

    }

    filterConfiguration(controller, row, operator = "", val1 = "", val2 = "") {
        let Configuration: FilterRowConfig = { row: row, operatorType: operator, valueoneType: val1, valuetwoType: val2, valueList: "" };

        if (controller._filterConfig.length > row) {
          
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

    //editCell(e) {

    //$(e.target)

    //}



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
            case 4:
                if (controller._descriptionvalue !== "" && controller._descriptionvalue !== undefined) {
            $("#Group1").data('kendoDropDownList').wrapper.show();

        }
                break;
            case 0:
                break;
            case 6:
                try {
                    if (controller._descriptionvalue !== "" && controller._descriptionvalue !== undefined)  {
                        $("#" + controller._filterConfig[row].operatorType).data('kendoComboBox').wrapper.show();
                        ReportFilterController.getInstance().comboSelection(controller._grid);
                    }
                   
                } catch (ex) { }
                break;
            case 7:
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
            case 8:
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
            case 9:
                if (controller._descriptionvalue !== "" && controller._descriptionvalue !== undefined) {
                    $("#Group2").data('kendoDropDownList').wrapper.show();
            
                }
                break;
            //case 10:
            //    $("#AndOr").data('kendoDropDownList').wrapper.show();
            //    break;
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
            case 'ReportFieldList':
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
                            controller._grid.dataSource.view()[row]["ColumnName"] = $('#ReportFieldList').data('kendoComboBox').text();
                            controller._addBlankRow(controller);
               
                            break;
                        
                    }
                });
                break;
            case 'Operator2':
                controller._grid.dataSource.view()[row]["AndOr"] = $('#Operator2').data('kendoDropDownList').text();
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
            case 'Group1':
                controller._grid.dataSource.view()[row]["OpenGroup"] = $('#Group1').data('kendoDropDownList').value();
                break;
            case 'Group2':
                controller._grid.dataSource.view()[row]["CloseGroup"] = $('#Group2').data('kendoDropDownList').value();
                break;
            case 'operatordropdownListoptions':
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
                    if (controller._grid.dataSource.view()[row]["ColumnName"].match("Date"))
                        filter.filterConfiguration(controller, row, "", "DateValues", "");
                    if (dropdown.match("number"))
                        filter.filterConfiguration(controller, row, "", "NumberBox", "");
                    if (dropdown.match("True"))
                        filter.filterConfiguration(controller, row, "", "DropdownValues2", "");

                }
                controller._grid.dataSource.view()[row]["ComparisonOperator"] = $('#' + controller._filterConfig[row].operatorType).data('kendoComboBox').text();
                //controller._grid.dataSource.view()[row]["Value1"] = "";
                controller._grid.dataSource.view()[row]["Value2"] = "";
                controller._grid.refresh();
                let curCell = controller._grid.tbody.find(">tr:eq(" + row + ") >td:eq(" + cellIndex + ")").next();
                controller._grid.current(curCell);
                curCell.addClass('k-state-focused');
                controller._grid.select(curCell);
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

   

    loadReportFilter(filterName : number) {
        var controller = this;
        var grid = $("#ReportFilterCriteriaGrid").data('kendoGrid');
        $.ajax({
            global: false,
            type: "GET",
            url: '/ReportFilterCriteria/LoadReportFilter',
            data: { filter: filterName }
        }).done((data) => {
            grid.dataSource.read().done(() => {
                grid.dataSource.data().forEach((item, data) => {
                    $(item["Value1"]).val("test").trigger("change");
                });
                //$("#ReportFilterCriteriaGrid").data('kendoGrid').dataSource.data().forEach((item, data) => {
                //    controller.filterConfiguration(controller, item["Position"], "operatordropdownListoptions", "DropdownValues", "");
                //    controller.foreignKeyValueDropdown(controller, item["ColumnName"], item["Position"]);
                //    controller._grid.dataSource.view()[item["Position"]]["ColumnName"] = $('#ReportFieldList').data('kendoComboBox').text();
                //    controller._grid.dataSource.view()[item["Position"]]["ComparisonOperator"] = $('#' + controller._filterConfig[item["Position"]].operatorType).data('kendoComboBox').text();
                //    controller.filterchangeEvent(controller, "operatordropdownListoptions", item["ComparisonOperator"], item["ComparisonOperator"], controller._grid.select().parent().index(), '/ReportFilterCriteria/ValueField');

                //    try {
                //        if (controller._filterConfig[item["Position"]].valueoneType === "DropdownValues" || controller._filterConfig[item["Position"]].valueoneType === "DropdownValues2") {
                //            (<any>$("#" + controller._filterConfig[item["Position"]].valueoneType)).getKendoComboBox().dataSource.data(controller._filterConfig[item["Position"]].valueList);
                //            (<any>$("#" + controller._filterConfig[item["Position"]].valueoneType)).getKendoComboBox().dataSource.query();
                //            $("#" + controller._filterConfig[item["Position"]].valueoneType).data('kendoComboBox').wrapper.show();
                //            ReportFilterController.getInstance().comboSelection(controller._grid);
                //        } else if (controller._filterConfig[item["Position"]].valueoneType === "TextBox") {
                //            $("#" + controller._filterConfig[item["Position"]].valueoneType).data('kendoMaskedTextBox').wrapper.show();
                //        } else if (controller._filterConfig[item["Position"]].valueoneType === "NumberBox") {
                //            $("#" + controller._filterConfig[item["Position"]].valueoneType).data('kendoNumericTextBox').wrapper.show();
                //        } else if (controller._filterConfig[item["Position"]].valueoneType === "DateValues") {
                //            $("#" + controller._filterConfig[item["Position"]].valueoneType).data('kendoDatePicker').wrapper.show();
                //        }
                //        $("#" + controller._filterConfig[item["Position"]].operatorType).data('kendoComboBox').wrapper.show();


                //    } catch (ex) { }

                //    try {
                //        if (controller._filterConfig[item["Position"]].valuetwoType === "DropdownValues3") {
                //            (<any>$("#" + controller._filterConfig[item["Position"]].valuetwoType)).getKendoComboBox().dataSource.data(controller._filterConfig[item["Position"]].valueList);
                //            (<any>$("#" + controller._filterConfig[item["Position"]].valuetwoType)).getKendoComboBox().dataSource.query();
                //            $("#" + controller._filterConfig[item["Position"]].valuetwoType).data('kendoComboBox').wrapper.show();
                //            ReportFilterController.getInstance().comboSelection(controller._grid);
                //        } else if (controller._filterConfig[item["Position"]].valuetwoType === "TextBox2") {
                //            $("#" + controller._filterConfig[item["Position"]].valuetwoType).data('kendoMaskedTextBox').wrapper.show();
                //        } else if (controller._filterConfig[item["Position"]].valuetwoType === "NumberBox2") {
                //            $("#" + controller._filterConfig[item["Position"]].valuetwoType).data('kendoNumericTextBox').wrapper.show();
                //        } else if (controller._filterConfig[item["Position"]].valuetwoType === "DateValues2") {
                //            $("#" + controller._filterConfig[item["Position"]].valuetwoType).data('kendoDatePicker').wrapper.show();
                //        }
                //    } catch (ex) { }
                //});
            
                      

            });

         
        }); 

    }

    loadDisplayList() {
        var grid = $("#ReportFilterCriteriaGrid").data("kendoGrid");
        $.ajax({
            global: false,
            type: "POST",
            url: '/ReportFilterCriteria/SaveCriteriaList',
            data: { modelListString: JSON.stringify(grid.dataSource.view().toJSON())}
        }).done((data) => {
           

        }); 


    }

    
    save() {
        var self = this;
        var grid = $("#ReportFilterCriteriaGrid").data("kendoGrid");
        //grid.dataSource.remove(grid.dataSource.data()[grid.dataSource.data().length - 1]);
        
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
                        self._filterSaveWindow.center().close();

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
                                            self._overwriteFilterWindow.center().close();
                                            self.reloadSavedFilterList();
                                            self._filterSaveWindow.center().close();
                                        });

                                    });
                                    $("#OverWriteCancel").on('click', function () {
                                        self._overwriteFilterWindow.center().close();

                                    });
                                });

                            }
                            else {
                                self.reloadSavedFilterList();
                                self._filterSaveWindow.center().close();
                            }

                        });
                    }
                    );


                })
            //}

            //else {
            //    console.log(data);
            //}

            });       

    }

    finishFilter() {

        var currentFilter = "";
        var self = this;
        var grid = $("#ReportFilterCriteriaGrid").data("kendoGrid");


        $.ajax({
            global: false,
            type: "POST",
            url: "/ReportFilterCriteria/FinishFilter",
            data: {  }
        }).done((data) => {

            alert(data);

        });


     console.log('made it here')
    }




    overWrite() {
        this._overwriteFilterWindow.center().open();
        this.filterName = $('#FilterNameList').data('kendoComboBox').text();
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
            url: "/ReportMain/ReportNameDropdown",

        }).done((data) => {
            $("#filterNameListDropdown").html(data);
            $("#ReportFilterGridNameList").data('kendoComboBox').select((item) => { return item.Text === previousName; });
        });
        //var previousName = $("#FilterNameList").data('kendoComboBox').text();
        //$.ajax({
        //    global: false,
        //    type: "GET",
        //    url: '/ReportFilterCriteria/ReloadList',
        //    data: {}
        //}).done((data) => {
        //    $("#ReportFilterGridNameList").data('kendoComboBox').list = data;
        //    $("#ReportFilterGridNameList").data('kendoComboBox').select((item) => { return item.Text === previousName;});
        //});

    }
    clear() {
        var grid = $("#ReportFilterCriteriaGrid").data("kendoGrid");
        $.ajax({
            global: false,
            type: "GET",
            url: '/ReportFilterCriteria/ClearFilter',
            data: {  }
        }).done((data) => {
            grid.dataSource.read();
        });
        //for (var i = 0; i < data.length; i++) {
        //    document.getElementById("filtertextdisplay").innerHTML += data[i]["Filter"] + " ";
        //}
    }

    deleteRow() {
        var numToDel = $('#ReportFilterCriteriaGrid').data('kendoGrid').dataSource.data()[$($('#ReportFilterCriteriaGrid td .k-checkbox:checked').closest('tr')[0]).index()].Position;
        //var rows = [];
        //for (var i = 0; i < numToDel; i++) {
        //    rows.push($('#ReportFilterCriteriaGrid').data('kendoGrid').dataSource.data()[$($('#ReportFilterCriteriaGrid td .k-checkbox:checked').closest('tr')[i]).index()]);
        //}
        //for (var b = 0; b < rows.length; b++) {
        //    $('#ReportFilterCriteriaGrid').data('kendoGrid').dataSource.data().remove(rows[b]);
        //}
        $.ajax({
            global: false,
            type: "POST",
            url: '/ReportFilterCriteria/UpdateFilterCache',
            data: { position: numToDel, removeEdit: true }
        }).done((data) => {
            $('#ReportFilterCriteriaGrid').data('kendoGrid').dataSource.read();
        });
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
                if (c.endsWith("Key"))
                    temp[c] = 0;
                else if (c.endsWith("Position"))
                {
                    temp[c] = newRow;
                }
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
    

        let controller = this;
        if ($('#ReportFilterCriteriaGrid').data('kendoGrid') !== undefined)
        {
            controller.clear();
        }
        $('#ReportLower').on('click', function (this: HTMLElement, event: JQueryEventObject) {
            controller._grid = $('#ReportFilterCriteriaGrid').data('kendoGrid');
            if (event.target.id === "Clear") { controller.clear(); }
            else if (event.target.id === "Save") { controller.save(); }
            else if (event.target.id === "Delete") { controller.deleteRow(); }
            else if (event.target.id === "FinishFilter") { controller.finishFilter(); }
           



            else {
                controller.filterclickEvent(controller, event.target, controller._grid.select().parent().index());
            }
        });
        $('#ReportLower').on('change', function (this: HTMLElement, event: JQueryEventObject) {
            if (event.target.id === "ReportFilterGridNameList") { controller.loadReportFilter(event.target['value']); }
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
            controller.filterchangeEvent(controller, event.target.id, DescVal, DescText, controller._grid.select().parent().index(), '/ReportFilterCriteria/ValueField');
        });

        //$('#ReportLower .k-grid').data('kendoGrid').columns.forEach((item1, i) => {
        //    item1.template = "#= customTemplate(data.type,data.editor) #";

        //});
    }

//    customTemplate(type, value) {
//    if (value == null)
//        return "";

//    switch (type) {
//        case "date":
//            return kendo.toString(kendo.parseDate(value), 'yyyy/MM/dd');
//        default:
//            return value;
//    }
//}



    private _setupFilterSaveWindow() {
        $("#filterSaveWindow").remove(); //If this tag exists remove it
        let deferred = $.Deferred<void>();
        this._filterSaveWindow = $('<div id="filterSaveWindow"></div>').kendoWindow(kendoWindowDefaultOptions({
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

    private _setupFilterOverwriteWindow() {
        $("#OverwriteWindow").remove(); 
        let self = this;
        let deferred = $.Deferred<void>();
        this._overwriteFilterWindow = $('<div id="OverwriteWindow"></div>').kendoWindow(kendoWindowDefaultOptions({
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

      private _setupReportViewWindow() {
        $("#reportViewWindow").remove();
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
        if (REPORT_GRID !== null && REPORT_GRID) {

            window['ReportFilterCriteriaGridEdit'] = () => {
            //    ReportFilterController.getInstance().editCell(event);



            }
        }
       
        
    } catch (err) {
        window.console.log(`no grid to load; or grid is set in view model.`);
    }
});

