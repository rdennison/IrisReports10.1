using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;

namespace Iris10ReportUI.Controllers
{
    public class RootController : AsyncController
    {
        private static List<Type> controllerTypeCache = null;
        public static List<Type> ControllerTypes
        {
            get
            {
                if (controllerTypeCache == null)
                {
                    Assembly a = Assembly.GetExecutingAssembly();
                    Type[] allTypes = a.GetTypes();

                    controllerTypeCache = new List<Type>();
                    foreach (Type t in allTypes)
                    {
                        if (t.FullName.StartsWith("Iris10ReportUI.Controllers"))
                            controllerTypeCache.Add(t);
                    }
                }

                return controllerTypeCache;
            }
        }

        [HttpPost]
        public JsonResult KeepSessionAlive()
        {
            Session.Timeout = 1440;
            return new JsonResult { Data = "Success" };
        }

        /// <summary>
        /// This is the root path for the website when someone does not provide any subfolders.
        /// </summary>
        [Route("~/")]
        public ActionResult Index()
        {
            // Redirect the user to the application landing page.
            return RedirectToAction("Login", "ReportApp");
        }
    }
}