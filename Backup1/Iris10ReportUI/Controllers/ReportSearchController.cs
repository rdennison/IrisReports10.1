using IrisModels.Models;
using SqlComponents;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;

namespace Iris10ReportUI.Controllers
{
    public class ReportSearchController : Controller
    {
        private List<SqlWhere> _searchWheres = new List<SqlWhere>();
       

        [HttpPost]
        public ActionResult ReportQuery(string searchString)
        {
            try
            {
                Session["ReportSearches"] = null;
               
                IEnumerable<PropertyInfo> props = null;
                RPTReportListByUserModel model = new RPTReportListByUserModel();
                List<SqlWhere> searchWheres = new List<SqlWhere>();
                if (model != null)
                    props = model.GetType().GetProperties();
                else
                    return Json("session expired");
                
                if (searchString != "")
                {
                    searchWheres = CreateWheres(searchString, searchWheres);
                    Session["ReportSearches"] = searchWheres;
                }
                else
                {
                    Session["ReportSearches"] = null;
                }
                return Json("ready");
            }
            catch (Exception ex)
            {
                return Json(Session?.Keys.ToString());
            }

        }

        private List<SqlWhere> CreateWheres(string searchString, List<SqlWhere> searches)
        {

            searches.Add(new SqlWhere("(", null, "RPTReportListByUser", "ReportName", '%' + searchString + '%', null, SqlWhereComparison.SqlComparer.Like, SqlWhereAndOrOptions.SqlWhereAndOr.And));


            searches.Add(new SqlWhere(null, null, "RPTReportListByUser", "ReportDescription", '%' + searchString + '%', null, SqlWhereComparison.SqlComparer.Like, SqlWhereAndOrOptions.SqlWhereAndOr.Or));
            searches.Add(new SqlWhere(null, ")", "RPTReportListByUser", "TagList", '%' + searchString + '%', null, SqlWhereComparison.SqlComparer.Like, SqlWhereAndOrOptions.SqlWhereAndOr.Or));
            
            return searches;

        }

    }
}