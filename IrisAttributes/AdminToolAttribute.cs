using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System;
using System.Web;

namespace IrisAttributes
{
    public sealed class AdminToolReference
    {
        public string Key { get; set; }
        public string Description { get; set; }

        public object BaseData { get; set; }
    }
    public class AdminToolAttribute : Attribute
    {
        private static List<Type> _controllerTypeCache = null;

        public string Name { get; set; }

        public string ID { get; set; }

        public string OnClick { get; set; }

        public int SecurityLevel {get; set;}

        public AdminToolAttribute() { }

        public void AdminListBuilder()
        {
            var userSecurityLevel = (int) HttpContext.Current.Session["CurrentGeneralAccessLevel"];
            List<SelectListItem> mainList = new List<SelectListItem>();
            IEnumerable<SelectListItem> referenceList = new List<SelectListItem>();
            string modelTypeName = HttpContext.Current.Session["ModelType"].ToString() == "null" ? null : HttpContext.Current.Session["ModelType"].GetType().Name;
            IrisGridColumnAttribute specialName = HttpContext.Current.Session["ModelType"].GetType().GetCustomAttribute<IrisGridColumnAttribute>();
            modelTypeName = (specialName?.ScreenName != null) ? specialName?.ScreenName : modelTypeName;

            if (_controllerTypeCache == null)
            {
                Assembly a = Assembly.Load("Iris10ReportUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
                Type[] allTypes = a.GetTypes();

                _controllerTypeCache = new List<Type>();
                foreach (var t in allTypes)
                {
                    if (t.FullName.StartsWith("Iris10ReportUI.Controllers"))
                        _controllerTypeCache.Add(t);
                }
            }

            if (modelTypeName != null)
            {
                foreach (var controllerType in _controllerTypeCache)
                {
                    var modelProperties = controllerType.GetMethods();
                    if(controllerType.Name != "ToolbarController" && controllerType.Name.EndsWith("Controller") &&
                        controllerType.Name.Replace("Controller", "").Equals(modelTypeName.Replace("Model", ""), StringComparison.CurrentCultureIgnoreCase))
                    {
                        foreach(var modelProperty in modelProperties)
                        {
                            var adminItem = modelProperty.GetCustomAttribute<AdminToolAttribute>();
                            if(adminItem != null && adminItem.SecurityLevel <= userSecurityLevel) //TODO: Add Security Level here
                            {
                                mainList.Add(new SelectListItem { Text = adminItem.Name, Value = adminItem.ID });
                            }
                        }
                    }
                }
                referenceList = mainList.OrderBy(o => o.Text);
                HttpContext.Current.Session["AdminTList"] = new SelectList(referenceList,"Value","Text");
            }
        }
    }
}
