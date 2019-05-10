using CoreDomain;
using Iris10ReportUI.Helpers;
using Iris10ReportUI.Models;
using IrisAttributes;
using IrisModels.Models;
using SqlComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

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

        [HttpGet]
        public ActionResult FilterMessageWindow()
        {
            return PartialView("~/Views/Toolbar/FilterConfirmPartialView.cshtml");
        }

        [HttpGet]
        public ActionResult SetGrid(string group1, string description, string operator1, string val1, string val2, string group2, string operator2, string descriptiontext, string value1text, string value2text, int position)
        {
            var inListvalue = new GridFilterWhereModel
            {
                OpenGroup = group1,
                ColumnName = description,
                Value1 = val1,
                Value2 = val2,
                ComparisonOperator = operator1,
                CloseGroup = group2,
                AndOr = operator2,
                TableName = Session["ModelType"].GetType().Name.Replace("Model", ""),
                Position = position != -1 ? position : displaylist.Count
            };
            var filterGridRow = new GridFilterWhereModel
            {
                OpenGroup = group1,
                ColumnName = descriptiontext,
                Value1 = value1text,
                Value2 = value2text,
                ComparisonOperator = operator1,
                CloseGroup = group2,
                AndOr = operator2,
                TableName = Session["ModelType"].GetType().Name.Replace("Model", ""),
                Position = position != -1 ? position : displaylist.Count, //if count is 0 this is in position 0 and so on, fits array structure
                InList = serializer.Serialize(inListvalue)
            };
            if (position != -1)
            {
                displaylist.RemoveAt(position);
                displaylist.Insert(position, filterGridRow);
            }
            else
            {
                displaylist.Add(filterGridRow);
            }
            HttpRuntime.Cache["FilterList"] = displaylist;
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateFilterCache(string data, bool removeEdit)
        {
            //if removeAdd == true then remove else edit
            if (removeEdit)
            {
                int count = 0;
                var removeModels = (IEnumerable<GridFilterWhereModel>) serializer.Deserialize(data, typeof(IEnumerable<GridFilterWhereModel>));
                foreach (var model in removeModels)
                {
                    displaylist.Remove(displaylist.FirstOrDefault(c => c.Position == model.Position));
                }
                foreach (var item in displaylist)
                {
                    item.Position = count;
                    count++;
                }
            }
            HttpRuntime.Cache["FilterList"] = displaylist;
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ForeignKeyValue(string field)
        {
            var booleanDropdown = new ReportFilterDropdownAttribute();
            SelectList modelList = null;
            Type model = Session["ModelType"].GetType();
            PropertyInfo[] blankModel = model.GetProperties();
            string keyString = "";
            if (field.Contains('.'))
            {
                List<object> records = _coreService.ReportDataTables(true, null, null, field, true, Session["CurrentUserKey"].ToString());
                IList<ReportFilterScreenReference> referenceList = new List<ReportFilterScreenReference>();
                IEnumerable<ReportFilterScreenReference> referenceList1 = new List<ReportFilterScreenReference>();
                if (records != null)
                {
                    foreach (var item in records)
                    {
                        var references1 = new ReportFilterScreenReference();
                        references1.Key = item.ToString();
                        references1.Description = item.ToString();
                        referenceList.Add(references1);
                    }
                }
                referenceList1 = referenceList.OrderBy(o => o.Description);
                return Json(new SelectList(referenceList1, "Key", "Description"), JsonRequestBehavior.AllowGet);
            }
            if (field.EndsWith("_Key"))
            {
                foreach (PropertyInfo p in blankModel)
                {
                    if (p.Name == field)
                    {
                        keyString = p.GetCustomAttribute<ForeignKeyAttribute>().ReferenceModelType.Name.Replace("Model", "_Key");
                        modelList = p.GetCustomAttribute<ForeignKeyAttribute>().GetForeignKeyList();
                    }
                }
                if (keyString != "" && modelList != null)
                    return Json(modelList, JsonRequestBehavior.AllowGet);
                else
                    return null;
            }
            else
            {
                return Json(booleanDropdown.TrueFalseList(), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult ClearFilter()
        {
            displaylist.Clear();
            filterlist.Clear();
            HttpRuntime.Cache["FilterList"] = displaylist;
            Session["GridFilterWheres"] = null;
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult SaveValidation(string filterName)
        {
            GridFilterModel gridFilter = GetGridFilter(filterName);
            if (gridFilter != null)
            {
                IEnumerable<GridFilterWhereModel> filterWheres = GetGridFilterWheres(gridFilter.GridFilter_Key);
                if (filterWheres.Count() > 0)
                    return Json(filterName + " exists and has data, do you want to overwrite?", JsonRequestBehavior.AllowGet);
                else
                    return Json(filterName + " exists, do you want to overwrite?", JsonRequestBehavior.AllowGet);
            }
            else
                SaveFilter(filterName);
            return Json("saved", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveFilter(string filterName)
        {
            string db = Session["ConString"].ToString();
            bool filterExists = false;
            GridFilterModel gridFilter = GetGridFilter(filterName);
            if (gridFilter == null)
            {
                gridFilter = new GridFilterModel
                {
                    PageName_Key = _page.PageKey(Session["ModelType"].GetType().Name.Replace("Model", "")),
                    Name = filterName,
                    CreatedByUser_Key = (int) Session["CurrentUserKey"],
                    UpdatedByUser_Key = (int) Session["CurrentUserKey"],
                    Tenant_Key = (int) Session["CurrentTenantKey"]
                };
                _coreService.SprocInsert(gridFilter, db);
                gridFilter = GetGridFilter(filterName);
            }
            else
            {
                filterExists = true;
                gridFilter.UpdatedByUser_Key = (int) Session["CurrentUserKey"];
                _coreService.SprocUpdate(gridFilter, db);
            }
            createFilterList(gridFilter.GridFilter_Key);


            if (filterExists)
            {
                IEnumerable<GridFilterWhereModel> filterWheres = GetGridFilterWheres(gridFilter.GridFilter_Key);
                if (filterWheres.Count() > 0)
                {
                    foreach (var row in filterWheres)
                    {
                        _coreService.SprocDelete(row, db);
                    }
                }
            }
            foreach (var row in filterlist)
            {
                _coreService.SprocInsert(row, db);
            }
            return Json(filterName);
        }

        [HttpPost]
        public ActionResult RemoveFilter(string filterName)
        {
            string db = Session["ConString"].ToString();
            GridFilterModel gridFilter = GetGridFilter(filterName);
            IEnumerable<GridFilterWhereModel> filterWheres = GetGridFilterWheres(gridFilter.GridFilter_Key);
            foreach (var model in filterWheres)
            {
                _coreService.SprocDelete(model, db);
            }
            _coreService.SprocDelete(gridFilter, db);
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult LoadFilter(string filter)
        {
            ClearFilter();
            GridFilterModel gridFilter = GetGridFilter(filter);
            IEnumerable<GridFilterWhereModel> filterWheres = GetGridFilterWheres(gridFilter.GridFilter_Key);
            filterlist.AddRange(filterWheres);
            createDisplayList(gridFilter.GridFilter_Key);
            HttpRuntime.Cache["FilterList"] = displaylist;
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult FinishFilter(string filterName, bool report = false, bool frompage = false)
        {
            if (frompage)
                ClearFilter();
            string db = Session["ConString"].ToString();
            Session["GridFilterWheres"] = null;
            var wheres = new List<SqlWhere>();
            if (filterName == "")
                createFilterList(0); //create the filterdata to work with
            else
            {
                GridFilterModel gridFilter = GetGridFilter(filterName);
                if (displaylist.Count > 0)
                    createFilterList(gridFilter.GridFilter_Key);
                else
                {
                    IEnumerable<GridFilterWhereModel> filterWheres = GetGridFilterWheres(gridFilter.GridFilter_Key);
                    filterlist.AddRange(filterWheres);
                    createDisplayList(gridFilter.GridFilter_Key);
                    HttpRuntime.Cache["FilterList"] = displaylist;
                }
            }
            string result = FilterValidation("results");
            foreach (var row in filterlist)
            {
                wheres.Add(new SqlWhere(row.OpenGroup, row.CloseGroup, row.TableName, row.ColumnName, row.Value1, row.Value2, row.ComparisonOperator, GetAndOrSyntax(row.AndOr)));
            }
            Session["GridFilterWheres"] = wheres;

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #region +++++++Filter Support Functions+++++++
        [HttpGet]
        public ActionResult ValueField(string field)
        {
            Type model = Session["ModelType"].GetType();
            foreach (PropertyInfo p in model.GetProperties())
            {
                if (p.Name == field)
                {
                    if (p.GetCustomAttribute<FilterTypeAttribute>() != null)
                    {
                        if (p.GetCustomAttribute<FilterTypeAttribute>().Dropdown == true)
                        {
                            return Json("Dropdown", JsonRequestBehavior.AllowGet);
                        }
                        if (p.GetCustomAttribute<FilterTypeAttribute>().Operator2 == true)
                        {
                            return Json("Operator2", JsonRequestBehavior.AllowGet);
                        }
                        if (p.GetCustomAttribute<FilterTypeAttribute>().Date == true)
                        {
                            return Json("Date", JsonRequestBehavior.AllowGet);
                        }
                        if (p.GetCustomAttribute<FilterTypeAttribute>().Text == true)
                        {
                            return Json("Text", JsonRequestBehavior.AllowGet);
                        }
                        if (p.GetCustomAttribute<FilterTypeAttribute>().Number == true)
                        {
                            return Json("Number", JsonRequestBehavior.AllowGet);
                        }
                        if (p.GetCustomAttribute<FilterTypeAttribute>().Dropdown2 == true)
                        {
                            return Json("Dropdown2", JsonRequestBehavior.AllowGet);
                        }
                        if (p.GetCustomAttribute<FilterTypeAttribute>().Group1 == true)
                        {
                            return Json("Group1", JsonRequestBehavior.AllowGet);
                        }

                        if (p.GetCustomAttribute<FilterTypeAttribute>().Group2 == true)
                        {
                            return Json("Group2", JsonRequestBehavior.AllowGet);
                        }
                    }
                }
            }
            return null;
        }

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

        public string FilterValidation(string type)
        {
            var records = 0;
            switch (type)
            {
                case "results":
                    var wheres = new List<SqlWhere>();
                    foreach (var row in filterlist)
                    {
                        wheres.Add(new SqlWhere(row.OpenGroup, row.CloseGroup, row.TableName, row.ColumnName, row.Value1, row.Value2, row.ComparisonOperator, GetAndOrSyntax(row.AndOr)));
                    }
                    records = _coreService.RecordCount(Session["ModelType"].GetType(), wheres);
                    if (records > 0)
                        return "valid";
                    else
                        return "This generates no results, do you want to continue with this filter?";
            }

            return "";
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
                var realGridRow = (GridFilterWhereModel) serializer.Deserialize(item.InList, typeof(GridFilterWhereModel));
                string inListRow = item.InList;
                item.InList = "";
                realGridRow.InList = serializer.Serialize(item);
                item.InList = inListRow;
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
                var gridRow = (GridFilterWhereModel) serializer.Deserialize(item.InList, typeof(GridFilterWhereModel));
                string inListRow = item.InList;
                item.InList = "";
                gridRow.InList = serializer.Serialize(item);
                item.InList = inListRow;
                gridRow.GridFilter_Key = gridfilterkey;
                gridRow.CreatedByUser_Key = (int) Session["CurrentUserKey"];
                gridRow.UpdatedByUser_Key = (int) Session["CurrentUserKey"];
                gridRow.Tenant_Key = (int) Session["CurrentTenantKey"];
                displaylist.Add(gridRow);
            }
        }

        private GridFilterModel GetGridFilter(string filterName)
        {
            var wheres = new List<SqlWhere>();
            wheres.Add(new SqlWhere(null, null, "GridFilter", "Name", filterName, null, SqlWhereComparison.SqlComparer.Equal, SqlWhereAndOrOptions.SqlWhereAndOr.And));
            wheres.Add(new SqlWhere(null, null, "GridFilter", "PageName_Key", _page.PageKey(Session["ModelType"].GetType().Name.Replace("Model", "")), null, SqlWhereComparison.SqlComparer.Equal, SqlWhereAndOrOptions.SqlWhereAndOr.And));
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