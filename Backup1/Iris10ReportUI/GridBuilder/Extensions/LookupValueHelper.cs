using CoreDomain;
using IrisModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Iris10ReportUI.GridBuilder.Extensions
{
    public sealed class LookupValueHelper
    {
        private readonly CoreService _coreService = new CoreService();
        public sealed class LookupData
        {
            public string Key { get; set; }
            public string Description { get; set; }

            public object BaseData { get; set; }
        }

        public void CreateLookupList()
        {
            IEnumerable<LookupValuesModel> listData = _coreService.LoadModel<LookupValuesModel>(conName: HttpContext.Current.Session["ConString"].ToString());
            IEnumerable<LookupData> lookupLista = new List<LookupData>();
            IList<LookupData> lookupListb = new List<LookupData>();

            foreach(var m in listData)
            {
                LookupData data = new LookupData
                {
                    Key = m.TableName,
                    Description = m.Description
                };
                lookupListb.Add(data);
            }
            lookupLista = lookupListb.OrderBy(o => o.Description);
            SelectList lookupList = new SelectList(lookupLista, "Key", "Description");
            HttpContext.Current.Session["LookupList"] = lookupList;
        }
    }
}