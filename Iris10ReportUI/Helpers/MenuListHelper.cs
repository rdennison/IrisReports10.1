using System;
using System.Collections.Generic;
using System.Web;
using IrisAttributes;
using Iris10ReportUI.Controllers;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Caching;
using Iris10ReportUI.Attributes;
using Iris10ReportUI.Attributes;

namespace Iris10ReportUI.Helpers
{
    //use to create a selectList of menu items that will bind to the menu
    public sealed class MenuReference
    {
        public string Text { get; set; }
        public string URL { get; set; }
    }

    public class MenuListHelper
    {
        public void ListGenerator()
        {
            List<Type> controllers = RootController.ControllerTypes;
            List<MenuReference> menuList = new List<MenuReference>();
            foreach (Type c in controllers)
            {
                // Get a list of all the endpoints on this controller
                MethodInfo[] actions = c.GetMethods(BindingFlags.Instance | BindingFlags.Public);
                foreach (MethodInfo a in actions)
                {
                    // Look for a menu attribute that matches our requested menu
                    IrisMenuAttribute menu = a.GetCustomAttribute<IrisMenuAttribute>();
                    // We can skip over this action since we couldn't find a matching menu item
                    if (menu == null) continue;

                    AuthenticateAttribute authAttribute = a.GetCustomAttribute<AuthenticateAttribute>(true);
                    if (authAttribute != null && authAttribute.MenuService() == false) continue;

                    string[] route = menu.GetPathChain();
                    // Get token info for this step
                    string t = route[0];
                    string u = route[1];
                    string p = "";
                    if (route.Length == 3)
                        p = route[2];
                    MenuReference item = null;
                    if (u == "onGridReports" || u == "openProfileWindow" || u == "requestEntryWindow" || u == "roadRequestEntryWindow" || u == "utilityRequestEntryWindow" || u == "streetWiseConfigurationWindow")
                        item = new MenuReference { Text = t, URL = u };
                    else if(p != "")
                        item = new MenuReference { Text = t, URL = "/" + u + "/" + p };
                    else
                        item = new MenuReference { Text = t, URL = "/" + u + "/" + u };
                    menuList.Add(item);
                }
            }
            HttpRuntime.Cache.Remove("IrisMenu");
            HttpRuntime.Cache.Add("IrisMenu", new SelectList(menuList, "URL", "Text"), null, DateTime.Now.AddDays(14), Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);
        }

    }
}