using CoreDomain;
using IrisModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Iris10ReportUI.Helpers;
using WebGrease.Css.Extensions;
using Iris10ReportUI.Helpers;

namespace Iris10ReportUI.GridBuilder.Extensions
{
    public sealed class UserFilterHelper
    {
        readonly CoreService _coreService = new CoreService();
        private readonly PageNameHelper _page = new PageNameHelper();

        public void SetUserFilter(string model)
        {
            var gridFilters = _coreService.LoadModel<GridFilterModel>(conName: HttpContext.Current.Session["ConString"].ToString()).ToList();
            BuildList(gridFilters, model);
        }
        
        private sealed class GridFilterList
        {
            public string Key { get; set; }
            public string Description { get; set; }

            public object BaseData { get; set; }
        }

        private void BuildList(IEnumerable<GridFilterModel> userGridFilters, string modelName)
        {
            var referenceList = new List<GridFilterList>();

            // Add initial filter
            var selectedValue = new GridFilterList() { Key = "", Description = "--Select--" };
            referenceList.Add(selectedValue);

            if (HttpContext.Current.Session["CurrentUserKey"] != null)
            {
                userGridFilters
                .Where(gf => gf.CreatedByUser_Key == (int) HttpContext.Current.Session["CurrentUserKey"] &&
                                     gf.PageName_Key == _page.PageKey(modelName))
                .ForEach(gf => referenceList.Add(new GridFilterList() { Key = gf.GridFilter_Key.ToString(), Description = gf.Name }));
                HttpContext.Current.Session["FilterList"] = new SelectList(referenceList, "Key", "Description", selectedValue);
            }

        }


    }
}