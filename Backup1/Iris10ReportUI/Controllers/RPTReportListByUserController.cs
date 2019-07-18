using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Web.Mvc;
using IrisModels.Models;
using System.Collections.Generic;
using Iris10ReportUI.Helpers;
using SqlComponents;
using System;
using System.Web.Script.Serialization;
using CoreDomain;
using System.Linq;
using Iris10ReportUI.Models;

namespace Iris10ReportUI.Controllers
{

    public class RPTReportListByUserController : RootController
    {
        private readonly CoreService _coreService = new CoreService();
        private readonly ControllerHelper _cHelper = new ControllerHelper();
        
        public ActionResult RPTReportListByUser()
        {
            return View("RPTReportListByUser");
        }

        [HttpGet]
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            SqlWhere byUser = new SqlWhere(null, null, "RPTReportListByUser", "User_Key", Session["CurrentUserKey"], null, SqlWhereComparison.SqlComparer.Equal, SqlWhereAndOrOptions.SqlWhereAndOr.And);
            List<SqlWhere> wheres = new List<SqlWhere>();
            wheres.Add(byUser);
            if(Session ["ReportSearches"] != null)
            {
                wheres.AddRange((List<SqlWhere>) Session["ReportSearches"]);
            }
            DataSourceResult result = _cHelper.Read<RPTReportListByUserModel>(request, true, wheres);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public bool UpdateFavorite(string fav, bool check)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            RPTReportListByUserModel favItem = (RPTReportListByUserModel)serializer.Deserialize(fav, typeof(RPTReportListByUserModel));
            ReportFavoriteModel model = _coreService.LoadModel<ReportFavoriteModel>().FirstOrDefault(r => r.Report_Key == favItem.Report_Key && r.User_Key == int.Parse(Session["CurrentUserKey"].ToString()));
            if(model == null && check)
            {
                model = new ReportFavoriteModel
                {
                    Report_Key = favItem.Report_Key,
                    User_Key = int.Parse(Session["CurrentUserKey"].ToString())
                };
                _cHelper.CreateUpdate(null, model);
            }
            else if(model != null && !check)
            {
                _cHelper.Destroy(null, model);
            }
            return true;
        }

        [HttpPost]
        public ActionResult SelectReport(string report)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            ReportDescriptionViewModel descriptionViewModel = new ReportDescriptionViewModel();
          
            RPTReportListByUserModel curReport = (RPTReportListByUserModel) serializer.Deserialize(report, typeof(RPTReportListByUserModel));
            

            
            IEnumerable<ReportAvailableFilterModel> model = _coreService.LoadModel<ReportAvailableFilterModel>().Where(r => r.Report_Key == curReport.Report_Key);

            descriptionViewModel.Description = curReport.ReportDescription;
            descriptionViewModel.Success = true;
            Session["SelectedReportCriteria"] = model;
            return Json(descriptionViewModel, JsonRequestBehavior.AllowGet);
        }

        public FileContentResult DisplayImage()
        {
            IEnumerable<ReportAvailableFilterModel> model = (IEnumerable<ReportAvailableFilterModel>)Session["SelectedReportCriteria"];

            List<SqlWhere> wheres = new List<SqlWhere>();
            wheres.Add(new SqlWhere(null, null, "ReportExample", "Report_Key", 1000, null, SqlWhereComparison.SqlComparer.Equal, SqlWhereAndOrOptions.SqlWhereAndOr.And));
            byte[] image = _coreService.LoadModel<ReportExampleModel>(wheres, conName: Session["ConString"].ToString()).First().ExampleReport;
            return File(image, "image/jpg");
        }


    }
}

