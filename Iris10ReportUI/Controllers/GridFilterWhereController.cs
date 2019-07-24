using CoreDomain;
using Iris10ReportUI.Helpers;
using IrisModels.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Iris10ReportUI.Controllers
{
    public class GridFilterWhereController : Controller
    {
        private readonly CoreService _coreService = new CoreService();
        private readonly ControllerHelper _cHelper = new ControllerHelper();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request">requester (kendo grid)</param>
        /// <param name="FilterDetail">Model in question</param>
        /// <returns> new list from database</returns>
        [HttpPost]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<GridFilterWhereModel> model)
        {
            _cHelper.CreateUpdate(request, model);
            return Json(model.ToDataSourceResult(request));
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="request">requester (kendo grid)</param>
        /// <returns> new list from database</returns>
        [HttpGet]
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            List<GridFilterWhereModel> empty = new List<GridFilterWhereModel>();
            DataSourceResult result;
            if (HttpRuntime.Cache["ReportFilterList"] != null && ((List<GridFilterWhereModel>) HttpRuntime.Cache["ReportFilterList"]).Count > 0)
            {
                //((List<GridFilterWhereModel>) HttpRuntime.Cache["ReportFilterList"]).Add(new GridFilterWhereModel { });
                result = ((List<GridFilterWhereModel>) HttpRuntime.Cache["ReportFilterList"]).ToDataSourceResult(request);
            }
            else
            {
                empty.Add(new GridFilterWhereModel { });
                result = empty.ToDataSourceResult(request);
            }
               
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request">requester (kendo grid)</param>
        /// <param name="FilterDetail">Model in question</param>
        /// <returns> new list from database</returns>
        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<GridFilterWhereModel> model)
        {
            _cHelper.CreateUpdate(request, model);
            return Json(model.ToDataSourceResult(request));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request">requester (kendo grid)</param>
        /// <param name="FilterDetail">Model in question</param>
        /// <returns> new list from database</returns>
        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<GridFilterWhereModel> model)
        {
            model = _cHelper.Destroy(request, model);
            return Json(model.ToDataSourceResult(request));
        }
    }
}