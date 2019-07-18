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
            
          
                
                   
                        if (field.Contains("_Key"))
                        {
                            return Json("Dropdown", JsonRequestBehavior.AllowGet);
                        }
                       else if (field.Contains("Active"))
                        {
                            return Json("Dropdown2", JsonRequestBehavior.AllowGet);
                        }
                        else if (field.Contains("Date"))
                        {
                            return Json("Date", JsonRequestBehavior.AllowGet);
                        }
                        else 
                        {
                            return Json("Text", JsonRequestBehavior.AllowGet);
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
                    model.Value1 = model.DropdownValues.Value;
                }

                if(model.DropdownValues3 != null)
                {
                    model.Value2 = model.DropdownValues3.Value;
                }
                displaylist.Add(model);
            }
            
               
            
            HttpRuntime.Cache["SelectedReportCriteria"] = displaylist;

            return null;
        }

        [HttpPost]
        public ActionResult UpdateFilterCache(int position, bool removeEdit)
        {
            //if removeAdd == true then remove else edit
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
            var wheres = new List<SqlWhere>();
            wheres.Add(new SqlWhere(null, null, "RPTReportAvailableFilterList", "ColumnName", field, null, SqlWhereComparison.SqlComparer.Equal, SqlWhereAndOrOptions.SqlWhereAndOr.And));
            RPTReportAvailableFilterList getList = _coreService.LoadModel<RPTReportAvailableFilterList>(wheres, conName : Session["ConString"].ToString()).FirstOrDefault();
            object lookupList = serializer.DeserializeObject(getList.LookupList);
            IList FilterLookupValueList = new List<ReportSelectListViewModel>();
            var configLines = (IList) lookupList;
            foreach (var configLine in configLines)
            {
                var myList = (Dictionary<string, object>) configLine;
              
                    var ugc = new ReportSelectListViewModel
                    {
                 LKey = myList["LKey"].ToString(),
                 LVal = myList["LVal"].ToString()
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
                SaveReportFilter(filterName);
            return Json("saved", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveReportFilter(string filterName, bool filterReplace = false)
        {
            string db = Session["ConString"].ToString();
            bool filterExists = false;
            GridFilterModel gridFilter = new GridFilterModel();
            gridFilter = GetGridFilter(filterName);
            if (filterReplace == true && gridFilter != null)
            {               
                _coreService.SprocDelete(gridFilter, db);
            }

            try
            {
                gridFilter = new GridFilterModel
                {
                    //PageName_Key = _page.PageKey(Session["ModelType"].GetType().Name.Replace("Model", "")),
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
        public ActionResult FinishFilter()
        {
            Type reportType;
             Assembly a = Assembly.Load("ReportLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");

            foreach(TypeInfo ti in a.DefinedTypes)
            {
                if(ti.Name.Contains(Session["ReportModelName"].ToString()))
                {
                    reportType = ti; 

                    break;
                }
            }
            var wheres = new List<SqlWhere>();

            foreach (var row in displaylist)
            {
                wheres.Add(new SqlWhere(row.OpenGroup, row.CloseGroup, Session["ReportModelName"].ToString(), row.ColumnName, row.Value1, row.Value2, row.ComparisonOperator, GetAndOrSyntax(row.AndOr)));
            }

            var reportSqlString = _coreService.TelerikSqlString(Session["ReportModelName"].ToString(), wheres );
            //reportType.GetConstructor(new Type[1]).Invoke(reportSqlString);
            return null;    
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
      

        [HttpGet]
        public ActionResult ReportValueField(string field)
        {
            string model = field.Split('.')[0] + "Model";
            string prop = field.Split('.')[1];
            Type curModel = null;
            Assembly a = Assembly.Load("IrisModels, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
            foreach (TypeInfo ti in a.DefinedTypes)
            {
                if (ti.FullName.StartsWith("IrisModels.Models") && !ti.Name.StartsWith("<") && ti.Name.Equals(model, StringComparison.OrdinalIgnoreCase))
                {
                    curModel = ti;
                }
            }

            if (curModel != null)
            {
                PropertyInfo[] blankModel = curModel.GetProperties();
                foreach (PropertyInfo p in blankModel)
                {
                    if (p.Name.Equals(prop, StringComparison.OrdinalIgnoreCase))
                    {
                        if (p.PropertyType.FullName.Contains("Date"))
                            return Json("Date", JsonRequestBehavior.AllowGet);
                        if (p.PropertyType.FullName.Contains("Decimal") || p.PropertyType.FullName.Contains("Integer"))
                            return Json("Number", JsonRequestBehavior.AllowGet);
                        if (p.PropertyType.FullName.Contains("Boolean"))
                            return Json("Dropdown2", JsonRequestBehavior.AllowGet);
                        else
                            return Json("Dropdown", JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return null;
        }

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
            switch (type)


            {
                case "result":
                    var wheres = new List<SqlWhere>();
                    Type tableType = null;
                    foreach (var row in model)
                    {
                        if (tableType == null)
                        {
                            Assembly a = Assembly.Load("IrisModels, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
                            foreach (TypeInfo ti in a.DefinedTypes)
                            {
                                if (ti.Name.Replace("Model", "") == Session["ReportModelName"].ToString())
                                {
                                    tableType = ti;
                                }
                            }

                        }

                        wheres.Add(new SqlWhere(row.OpenGroup, row.CloseGroup, Session["ReportModelName"].ToString(), row.ColumnName, row.Value1, row.Value2, row.ComparisonOperator, GetAndOrSyntax(row.AndOr)));
                        //SaveCriteriaList(row);
                    }
                    //var valueKey = _coreService.LoadModel(tableType, wheres);

                    records = _coreService.RecordCount(tableType, wheres);
                    
                    if (records > 0)
                        return Json("valid", JsonRequestBehavior.AllowGet);
                    else
                        return Json("This generates no results, do you want to continue with this filter?", JsonRequestBehavior.AllowGet);
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }

        public sealed class ReportFilterReference
        {
            public string Key { get; set; }
            public string Description { get; set; }

            public object BaseData { get; set; }
        }

        private void createFilterList(int gridfilterkey)
        {
            filterlist.Clear(); //create a clean list
            foreach (var item in displaylist)
            {
                GridFilterWhereModel realGridRow = (item);
               
                item.InList = "";
                if(realGridRow.AndOr == null  || realGridRow.AndOr == "")
                {
                    realGridRow.AndOr = "And";
                }
         
          
                realGridRow.GridFilter_Key = gridfilterkey;
                realGridRow.CreatedByUser_Key = (int) Session["CurrentUserKey"];
                realGridRow.UpdatedByUser_Key = (int) Session["CurrentUserKey"];
                realGridRow.Tenant_Key = (int) Session["CurrentTenantKey"];
                filterlist.Add(realGridRow);
            }
        }

        private void createDisplayList(int gridfilterkey)
        {
            var count = 0;
            displaylist.Clear(); //create a clean list
            foreach (var item in filterlist)
            {
                GridFilterWhereModel realGridRow = (item);

                item.InList = "";
                if (realGridRow.AndOr == null || realGridRow.AndOr == "")
                {
                    realGridRow.AndOr = "And";
                }


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
            IEnumerable<GridFilterWhereModel> filterWheres = _coreService.LoadModel<GridFilterWhereModel>(wheres, conName: db);
            return filterWheres;
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