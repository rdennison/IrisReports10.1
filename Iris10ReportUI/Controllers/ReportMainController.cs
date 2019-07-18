using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Iris10ReportUI.Controllers
{
    public class ReportMainController : Controller
    {
        // GET: ReportMain
        public ActionResult ReportMainUpper()
        {

            return PartialView();
        }
        [HttpGet]
        public ActionResult ReportMainLower()
        {

            return PartialView();
        }

        [HttpGet]
        public ActionResult ReportNameDropdown()
        {

            return PartialView();
        }
    }
}