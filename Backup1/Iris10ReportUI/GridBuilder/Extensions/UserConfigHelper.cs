using CoreDomain;
using IrisModels.Models;
using Iris10ReportUI.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Iris10ReportUI.Helpers;
using WebGrease.Css.Extensions;
using Iris10ReportUI.Helpers;
using Iris10ReportUI.Models;

namespace Iris10ReportUI.GridBuilder.Extensions
{
    public sealed class UserConfigHelper
    {
        private readonly CoreService _coreService = new CoreService();
        private readonly PageNameHelper _page = new PageNameHelper();
        public IList UserScreenConfig = new List<GridConfigJsonModel>();

        public void SetUserConfig(string model)
        {
            var jsonSerializer = new JavaScriptSerializer();
            var userGridConfigs = _coreService.LoadModel<GridConfigurationModel>(conName: HttpContext.Current.Session["ConString"].ToString()).ToList();
            BuildList(userGridConfigs, model);
            if (HttpContext.Current.Session["CurrentUserKey"] != null)
            {
                var userGridConfig = userGridConfigs.FirstOrDefault(gc =>
                        gc.CreatedByUser_Key == (int) HttpContext.Current.Session["CurrentUserKey"] &&
                        model.Equals(_page.UIPageName(gc.PageName_Key), StringComparison.CurrentCultureIgnoreCase));

                if (userGridConfig?.Configuration != null)
                {
                    object userConfig = jsonSerializer.DeserializeObject(userGridConfig.Configuration);
                    var configLines = (IList) userConfig;
                    foreach (var configLine in configLines)
                    {
                        var myList = (Dictionary<string, object>) configLine;
                        if(myList["column"] != null)
                        {
                            var ugc = new GridConfigJsonModel
                            {
                                ColumnField = myList["columnField"].ToString(),
                                Column = myList["column"].ToString(),
                                ColumnIndex = (int) myList["columnIndex"],
                                Required = (bool) myList["required"],
                                CopyDown = (bool) myList["copyDown"],
                                Hidden = (bool) myList["hidden"],
                                SortOrder = myList["sortorder"].ToString()
                            };
                            UserScreenConfig.Add(ugc);
                        }                      
                        
                    }
                }

            }
        }

        private sealed class GridConfigList
        {
            public string Key { get; set; }
            public string Description { get; set; }

            public object BaseData { get; set; }
        }

        private void BuildList(IEnumerable<GridConfigurationModel> userGridConfigs, string modelName)
        {
            IList<GridConfigList> referenceList = new List<GridConfigList>();
            if (HttpContext.Current.Session["CurrentUserKey"] != null)
            {
                userGridConfigs
                .Where(gc => gc.CreatedByUser_Key == (int) HttpContext.Current.Session["CurrentUserKey"] &&
                                     modelName.Equals(_page.UIPageName(gc.PageName_Key), StringComparison.CurrentCultureIgnoreCase))
                .ForEach(gc => referenceList.Add(new GridConfigList() { Key = gc.Name, Description = gc.Name }));
                HttpContext.Current.Session["ConfigList"] = new SelectList(referenceList, "Key", "Description");
            }
            
        }

    }
}