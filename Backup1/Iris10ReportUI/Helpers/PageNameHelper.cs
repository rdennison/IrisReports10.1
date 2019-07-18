using CoreDomain;
using IrisModels.Models;
using Iris10ReportUI.Attributes;
using Iris10ReportUI.Controllers;
using Iris10ReportUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Iris10ReportUI.Attributes;

namespace Iris10ReportUI.Helpers
{
    public class PageNameHelper
    {
        private readonly CoreService _coreService = new CoreService();
        private static IEnumerable<PageTranslationViewModel> pageUI = new List<PageTranslationViewModel>();

        public void CreateTables()
        {
            List<PageTranslationViewModel> pageTranslationList = new List<PageTranslationViewModel>();
            IEnumerable<PageNameModel> pageList = _coreService.LoadModel<PageNameModel>(conName: HttpContext.Current.Session["ConString"].ToString());
            var _controllers = RootController.ControllerTypes;
            foreach(var item in pageList)
            {
                PageTranslationViewModel m = new PageTranslationViewModel();
                m.PageName = item.PageName;
                m.PageName_Key = item.PageName_Key;
                var cMethods = _controllers.FirstOrDefault(c => c.Name.Replace("Controller", "") == item.PageName)?.GetMethods();
                if(cMethods != null)
                {
                    foreach (var a in cMethods)
                    {
                        var auth = (AuthenticateAttribute) a.GetCustomAttributes(typeof(AuthenticateAttribute), inherit: true).FirstOrDefault();
                        if (auth != null)
                            m.UIName = auth.PermissionKey;
                    }
                }
                else
                {
                    m.UIName = item.PageName;
                }
                
                pageTranslationList.Add(m);
            }
            pageUI = pageTranslationList;
        }

        public string UIPageName(int id)
        {
            return pageUI.FirstOrDefault(i => i.PageName_Key == id).UIName;
        }

        public string UIPageName(string page)
        {
            return pageUI.FirstOrDefault(i => String.Compare(i.PageName, page, StringComparison.InvariantCultureIgnoreCase) == 0).UIName;
        }

        public int PageKey(string page)
        {
            return pageUI.FirstOrDefault(i => String.Compare(i.PageName,page, StringComparison.InvariantCultureIgnoreCase) == 0 || String.Compare(i.UIName, page, StringComparison.InvariantCultureIgnoreCase) == 0).PageName_Key;
        }
    }
}