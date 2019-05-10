using CoreDomain;
using IrisModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Iris10ReportUI.GridBuilder.Extensions
{
    public sealed class ReportNames
    {
        public string Key { get; set; }
        public string Description { get; set; }

        public object BaseData { get; set; }
    }

    public class ReportNameHelper
    {
        public SelectList GenerateReportList()
        {
            CoreService _coreService = new CoreService();
            IList<ReportModel> reportData = _coreService.LoadModel<ReportModel>().ToList();
            List<ReportNames> rName = new List<ReportNames>();

            foreach (var name in reportData)
            {
                if (name.User_Key != 412)
                    rName.Add(new ReportNames { Key = name.Report_Key.ToString(), Description = name.Name });
            }







            return new SelectList(rName, "Key", "Description");


        }
    }
}