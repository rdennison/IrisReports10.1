using Kendo.Mvc.Extensions;
using System.Web.Mvc;
using IrisAttributes;
using CoreDomain;
using IrisModels.Models;
using System.Configuration;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Web.Script.Serialization;
using Iris10ReportUI.Models;
using System.Text;
using System.Reflection;
using Telerik.Reporting.Processing;
using Telerik.Reporting;
using System.Data.SqlClient;
using SqlComponents;
using System.ComponentModel.DataAnnotations;
using Iris10ReportUI.Attributes;
using System.Web;
using Iris10ReportUI.Helpers;
using ReportLibrary;
using System.Collections;
using Iris10ReportUI.GridBuilder.Extensions;
using System.Threading.Tasks;
using System.Data;
using Iris10ReportUI.GridBuilder.Interface;

namespace Iris10ReportUI.Controllers
{
    public class ReportFilterCriteriaController : Controller
    {
        private readonly CoreService _coreService = new CoreService();
        private readonly PageNameHelper _page = new PageNameHelper();
        private readonly List<ReportFilterGridDisplayViewModel> displayFilterInfo = new List<ReportFilterGridDisplayViewModel>();
        private static readonly List<GridFilterWhereModel> displaylist = new List<GridFilterWhereModel>();
        private static readonly List<GridFilterWhereModel> filterlist = new List<GridFilterWhereModel>();
        private readonly JavaScriptSerializer serializer = new JavaScriptSerializer();
        
        [HttpPost]
        public ActionResult ActivateFilter()
        {
            List<SqlWhere> reportKey = new List<SqlWhere>();
            reportKey.Add(new SqlWhere(null, null, "ReportAvailableFilter", "Report_Key", Session["Report_Key"], null, SqlWhereComparison.SqlComparer.Equal, SqlWhereAndOrOptions.SqlWhereAndOr.And));
            IEnumerable<ReportAvailableFilterModel> model = _coreService.LoadModel<ReportAvailableFilterModel>(reportKey, conName : Session["conString"].ToString());
            ReportDescriptionViewModel descriptionViewModel = (ReportDescriptionViewModel) Session["ReportFilter"];
            RemoveGridData();
            return Json(descriptionViewModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveFilterWindow()
        {
            return PartialView("~/Views/SaveFilterView/SaveFilterPartialView.cshtml");
        }

        public ActionResult OverwriteFilterWindow()
        {
            return PartialView("~/Views/SaveFilterView/OverwriteFilterPartialView.cshtml");
        }

        [HttpGet]
        public ActionResult ValueField(string field)
        {
            Dictionary<string, string> types = (Dictionary<string, string>) Session["ColumnTypes"];

            if (types[field] == "Int64" && field.Contains("_Key"))
            {
                return Json("Dropdown", JsonRequestBehavior.AllowGet);
            }
            else if (types[field] == "Boolean")
            {
                return Json("Boolean", JsonRequestBehavior.AllowGet);
            }
            else if (types[field] == "DateTime")
            {
                return Json("Date", JsonRequestBehavior.AllowGet);
            }
            else if(types[field] == "String")
            {
                return Json("Text", JsonRequestBehavior.AllowGet);
            }
            else if (types[field] == "Numeric")
            {
                return Json("Number", JsonRequestBehavior.AllowGet);
            }
            return null;
        }
        
        [HttpPost]
        public ActionResult SaveCriteriaList(string modelListString)
        {
            RemoveGridData();
            List<GridFilterWhereModel> modelList = (List<GridFilterWhereModel>)serializer.Deserialize(modelListString, typeof(List<GridFilterWhereModel>));

            foreach(var model in modelList)
            {
                if(string.IsNullOrEmpty(model.ColumnName))
                {
                    break;
                }
                if (model.DropdownValues != null)
                {
                    model.Value1 = model.DropdownValues.Text;
                }

                if(model.DropdownValues3 != null)
                {
                    model.Value2 = model.DropdownValues3.Text;
                }
                model.InList = serializer.Serialize(model);
                displaylist.Add(model);
            }
            HttpRuntime.Cache["SelectedReportCriteria"] = displaylist;
            return null;
        }

        [HttpPost]
        public ActionResult UpdateFilterCache(int position, bool removeEdit)
        {
            if (removeEdit)
            {
                displaylist.RemoveAt(position);
            }
            HttpRuntime.Cache["FilterList"] = displaylist;
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ForeignKeyValue(string field)
        {
            List<SqlWhere> wheres = new List<SqlWhere>();
            wheres.Add(new SqlWhere(null, null, "RPTReportAvailableFilterList", "ColumnName", field, null, SqlWhereComparison.SqlComparer.Equal, SqlWhereAndOrOptions.SqlWhereAndOr.And));
            RPTReportAvailableFilterList getList = _coreService.LoadModel<RPTReportAvailableFilterList>(wheres, conName : Session["ConString"].ToString()).FirstOrDefault();
            IList FilterLookupValueList = new List<ReportSelectListViewModel>();
            
            if (getList == null)
                return Json(new SelectList(FilterLookupValueList, "LKey", "LVal"), JsonRequestBehavior.AllowGet);
            
            foreach (var configLine in (IList)serializer.DeserializeObject(getList.LookupList))
            {
                var ugc = new ReportSelectListViewModel
                {
                        LKey = ((Dictionary<string, object>) configLine)["LKey"].ToString(),
                        LVal = ((Dictionary<string, object>) configLine)["LVal"].ToString()
                };
                FilterLookupValueList.Add(ugc);
            }
            return Json(new SelectList(FilterLookupValueList, "LKey", "LVal"), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ClearFilter()
        {
            RemoveGridData();
            return Json("" ,JsonRequestBehavior.AllowGet);
        }

        public void RemoveGridData()
        {
            displaylist.Clear();
            filterlist.Clear();
            HttpRuntime.Cache["FilterList"] = displaylist;
            Session["GridFilterWheres"] = null;
        }

        [HttpGet]
        public ActionResult ReloadList()
        {
            

            List<SqlWhere> wheres = new List<SqlWhere>();
            wheres.Add(new SqlWhere(null, null, "GridFilter", "Report_Key", Session["Report_Key"], null, SqlWhereComparison.SqlComparer.Equal, SqlWhereAndOrOptions.SqlWhereAndOr.And));
            wheres.Add(new SqlWhere(null, null, "GridFilter", "CreatedByUser_Key", Session["CurrentUserKey"], null, SqlWhereComparison.SqlComparer.Equal, SqlWhereAndOrOptions.SqlWhereAndOr.And));
            wheres.Add(new SqlWhere(null, null, "GridFilter", "PageName_Key", 88, null, SqlWhereComparison.SqlComparer.Equal, SqlWhereAndOrOptions.SqlWhereAndOr.And));
            IEnumerable<GridFilterModel> reportGridFilter = _coreService.LoadModel<GridFilterModel>(wheres, conName: Session["ConString"].ToString());

            return Json((new SelectList(reportGridFilter, "GridFilter_Key", "Name")), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult SaveValidation(string filterName)
        {
            GridFilterModel gridFilter = GetGridFilter(filterName);
            if (gridFilter != null)
            {
                IEnumerable<GridFilterWhereModel> filterWheres = GetGridFilterWheres(gridFilter.GridFilter_Key);
                if (filterWheres.Count() > 0)
                    return Json("overWrite", JsonRequestBehavior.AllowGet);
                else
                    return Json("overWrite", JsonRequestBehavior.AllowGet);
            }
            else
                return Json("saved", JsonRequestBehavior.AllowGet);
            //SaveReportFilter(filterName);

        }

        [HttpPost]
        public ActionResult SaveReportFilter(string filterName, bool filterReplace = false)
        {
            string db = Session["ConString"].ToString();
            bool filterExists = false;
            GridFilterModel gridFilter = new GridFilterModel();
            gridFilter = GetGridFilter(filterName);
            if (filterReplace == true && gridFilter != null)         
                _coreService.SprocDelete(gridFilter, db);
            try
            {
                gridFilter = new GridFilterModel
                {
                    PageName_Key = 88,
                    Name = filterName,
                    CreatedByUser_Key = (int) Session["CurrentUserKey"],
                    UpdatedByUser_Key = (int) Session["CurrentUserKey"],
                    Report_Key = (int) Session["Report_Key"],
                    Tenant_Key = (int) Session["CurrentTenantKey"]
                };
                _coreService.SprocInsert(gridFilter, db);
                gridFilter = GetGridFilter(filterName);
                createFilterList(gridFilter.GridFilter_Key);

                foreach (var row in filterlist)
                {
                    _coreService.SprocInsert(row, db);
                }
                return Json(filterName);
            }
            catch (Exception e)
            {
                return Json(e);
            }
        }

        [HttpPost]
        public ActionResult RemoveFilter(string filterName)
        {
            string db = Session["ConString"].ToString();
            GridFilterModel gridFilter = GetGridFilter(filterName);           
            _coreService.SprocDelete(gridFilter, db);
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult LoadReportFilter(int filter)
        {

            RemoveGridData();
            IEnumerable<GridFilterWhereModel> filterWheres = GetGridFilterWheres(filter);
            filterlist.AddRange(filterWheres);
            createDisplayList(filter);
            HttpRuntime.Cache["ReportFilterList"] = displaylist;
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult PopulateGridDisplayList(string gridrows)
        {
            RemoveGridData();
            List<GridFilterWhereModel> gridData = (List<GridFilterWhereModel>) serializer.Deserialize(gridrows, typeof(List<GridFilterWhereModel>));
            foreach(var row in gridData)
            {
                if (string.IsNullOrEmpty(row.ColumnName))
                {
                    break;
                }
                if (row.DropdownValues != null)
                {
                    row.Value1 = row.DropdownValues.Text;
                }

                if (row.DropdownValues3 != null)
                {
                    row.Value2 = row.DropdownValues3.Text;
                }
                row.InList = serializer.Serialize(row);
                displaylist.Add(row);
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult FinishFilter()
        {
            if (filterlist.Count == 0)
                createFilterList();
            
            Session["ReportTypeSource"] = ReportFactory.GetReport(filterlist);
            return PartialView("~/Views/Reports/ReportsPartialView.cshtml");    
        }

        private string GenerateReportProperties(string[] items)
        {
            //Assembly a = Assembly.Load("IrisModels, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
            string i = "";
            foreach (string item in items)
            {
                if (item.Contains("."))
                {
                    //var temp = "";
                    if (!item.EndsWith("_Key"))
                    {
                        if (i == "")
                            i += item;
                        else
                            i += "," + item;

                    }

     
                }
            }

            return i;
        }
        
        #region +++++++Filter Support Functions+++++++
        [HttpPost]
        public ActionResult FilterValidation(string type, string rowData = null)
        {
            IList<GridFilterWhereModel> model = new List<GridFilterWhereModel>();
            JavaScriptSerializer serializer = new JavaScriptSerializer();


            if (rowData == null && rowData == "[]")
            {
                return Json("Needs data", JsonRequestBehavior.AllowGet);
            }
           

            model = (List<GridFilterWhereModel>)serializer.Deserialize(rowData, typeof(List<GridFilterWhereModel>));
            foreach(var m in model)
            {
                if (m.ColumnName != null)
                {
                    if (m.DropdownValues != null)
                    {
                        m.Value1 = m.DropdownValues.Value;
                    }
                    if (m.DropdownValues2 != null)
                    {
                        m.Value1 = m.DropdownValues2.Value;
                    }
                    if (m.DropdownValues3 != null)
                    {
                        m.Value2 = m.DropdownValues3.Value;
                    }
                }

                else
                {
                    model.Remove(m);

                    break;
                }
            }
            var Group1 = 0;
            var Group2 = 0;

           
            foreach (GridFilterWhereModel m in model)
            {
                //if (rowData != "" && rowData != null)       

                if (m.OpenGroup != null)
                {
                    Group1++;
                }

                if (m.CloseGroup != null)
                {
                    Group2++;
                }

                    var Message = "";


                if (m.ComparisonOperator == null || m.ComparisonOperator == "")
                    {
                        Message += "Comparison Operator ";
                    }
                    if (m.Value1 == null || m.Value1== "")
                    {
                        Message += "VALUE1 ";
                    }

                    if (m.ComparisonOperator == "BETWEEN")
                    {
                        if (m.Value2 == null || m.Value2 == "")

                            Message += "VALUE2 ";
                    }


                    if (Message != "")
                    {
                        ModelState.AddModelError("Errors", "Choose" + Message + "to continue");
                        return Json(ViewData.ModelState["Errors"].Errors[0].ErrorMessage, JsonRequestBehavior.AllowGet);
                    }
                

            }

            if (Group1 != Group2)
            {
                ModelState.AddModelError("Errors", "You are missing a paranthese, check the missing parenthese and continue.");

                return Json(ViewData.ModelState["Errors"].Errors[0].ErrorMessage, JsonRequestBehavior.AllowGet);
            }
            var records = 0;
            //switch (type)


            //{
            //    case "result":
            //        var wheres = new List<SqlWhere>();
            //        Type tableType = null;
            //        foreach (var row in model)
            //        {
            //            if (tableType == null)
            //            {
            //                Assembly a = Assembly.Load("IrisModels, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
            //                foreach (TypeInfo ti in a.DefinedTypes)
            //                {
            //                    if (ti.Name.Replace("Model", "") == Session["ReportModelName"].ToString())
            //                    {
            //                        tableType = ti;
            //                    }
            //                }

            //            }

            //            wheres.Add(new SqlWhere(row.OpenGroup, row.CloseGroup, Session["ReportModelName"].ToString(), row.ColumnName, row.Value1, row.Value2, row.ComparisonOperator, GetAndOrSyntax(row.AndOr)));
            //            //SaveCriteriaList(row);
            //        }
            //        //var valueKey = _coreService.LoadModel(tableType, wheres);

            //        records = _coreService.RecordCount(tableType, wheres);
                    
            //        if (records > 0)
            //            return Json("valid", JsonRequestBehavior.AllowGet);
            //        else
            //            return Json("This generates no results, do you want to continue with this filter?", JsonRequestBehavior.AllowGet);
            //}

            return Json("", JsonRequestBehavior.AllowGet);
        }
        
        private void createFilterList(int gridfilterkey = 0)
        {
            filterlist.Clear(); //create a clean list
            foreach (var item in displaylist)
            {
                GridFilterWhereModel realGridRow = (item);
               
                item.InList = serializer.Serialize(item);
                if (item.AndOr == null || item.AndOr == "")
                {
                    realGridRow.AndOr = "And";
                }
                else
                    realGridRow.AndOr = item.AndOr;

                if (realGridRow.ReportFieldList != null)
                    realGridRow.ColumnName = item.ReportFieldList.Value;
                if (item.DropdownValues != null)
                    realGridRow.Value1 = item.DropdownValues.Value;
                if (item.DropdownValues2 != null)
                    realGridRow.Value1 = item.DropdownValues2.Value;
                if (item.DropdownValues3 != null)
                    realGridRow.Value2 = item.DropdownValues3.Value;

                realGridRow.ColumnName = item.ReportFieldList.Value;
                realGridRow.GridFilter_Key = gridfilterkey;
                realGridRow.CreatedByUser_Key = (int) Session["CurrentUserKey"];
                realGridRow.UpdatedByUser_Key = (int) Session["CurrentUserKey"];
                realGridRow.Tenant_Key = (int) Session["CurrentTenantKey"];
                filterlist.Add(realGridRow);
            }
        }

        private void createDisplayList(int gridfilterkey)
        {
            displaylist.Clear(); //create a clean list
            foreach (var item in filterlist)
            {
                GridFilterWhereModel realGridRow = (GridFilterWhereModel)serializer.Deserialize(item.InList,typeof(GridFilterWhereModel));

                realGridRow.InList = serializer.Serialize(item);
                if (realGridRow.AndOr == null || realGridRow.AndOr == "")
                {
                    realGridRow.AndOr = "And";
                }
                if(realGridRow.ReportFieldList != null)
                    realGridRow.ColumnName = realGridRow.ReportFieldList.Text;
                if (realGridRow.DropdownValues != null)
                    realGridRow.Value1 = realGridRow.DropdownValues.Text;
                if (realGridRow.DropdownValues2 != null)
                    realGridRow.Value1 = realGridRow.DropdownValues2.Text;
                if (realGridRow.DropdownValues3 != null)
                    realGridRow.Value2 = realGridRow.DropdownValues3.Text;
                realGridRow.GridFilter_Key = gridfilterkey;
                realGridRow.CreatedByUser_Key = (int) Session["CurrentUserKey"];
                realGridRow.UpdatedByUser_Key = (int) Session["CurrentUserKey"];
                realGridRow.Tenant_Key = (int) Session["CurrentTenantKey"];
                realGridRow.Position = item.Position;
                displaylist.Add(realGridRow);
            }
        }

        private GridFilterModel GetGridFilter(string filterName)
        {
            var wheres = new List<SqlWhere>();
            wheres.Add(new SqlWhere(null, null, "GridFilter", "Name", filterName, null, SqlWhereComparison.SqlComparer.Equal, SqlWhereAndOrOptions.SqlWhereAndOr.And));
            //wheres.Add(new SqlWhere(null, null, "GridFilter", "PageName_Key", 82(Session["ModelType"].GetType().Name.Replace("Model", "")), null, SqlWhereComparison.SqlComparer.Equal, SqlWhereAndOrOptions.SqlWhereAndOr.And));
            wheres.Add(new SqlWhere(null, null, "GridFilter", "PageName_Key", 88, null, SqlWhereComparison.SqlComparer.Equal, SqlWhereAndOrOptions.SqlWhereAndOr.And));
            //wheres.Add(new SqlWhere(null, null, null, null, null, null, SqlWhereComparison.SqlComparer.Equal, SqlWhereAndOrOptions.SqlWhereAndOr.And));


            GridFilterModel gridFilter = _coreService.LoadModel<GridFilterModel>(wheres, conName: Session["ConString"].ToString()).FirstOrDefault();
            return gridFilter;
        }

        private IEnumerable<GridFilterWhereModel> GetGridFilterWheres(int filterKey)
        {
            string db = Session["ConString"].ToString();
            var wheres = new List<SqlWhere>();
            wheres.Add(new SqlWhere(null, null, "GridFilterWhere", "GridFilter_Key", filterKey, null, SqlWhereComparison.SqlComparer.Equal, SqlWhereAndOrOptions.SqlWhereAndOr.And));
            return _coreService.LoadModel<GridFilterWhereModel>(wheres, conName: db);
        }

        private SqlWhereAndOrOptions.SqlWhereAndOr GetAndOrSyntax(string AndOr = null, SqlWhereAndOrOptions.SqlWhereAndOr AndOr2 = SqlWhereAndOrOptions.SqlWhereAndOr.And)
        {
            if (AndOr != null)
            {
                if (AndOr == "Or")
                {
                    return SqlWhereAndOrOptions.SqlWhereAndOr.Or;
                }
                else if (AndOr == "And")
                {
                    return SqlWhereAndOrOptions.SqlWhereAndOr.And;
                }
            }
            else
            {
                return AndOr2;
            }
            return SqlWhereAndOrOptions.SqlWhereAndOr.Or;
        }
        #endregion
    }
}