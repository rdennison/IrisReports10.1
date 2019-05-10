//using IrisWeb.Code.Data.Attributes;
using IrisAttributes;
using Iris10ReportUI.GridBuilder.Attributes;
using System;
//using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
//using System.Web.Mvc;

namespace IrisModels.Models
{
    //[IrisGrid("~/API/Activity/Read", "~/API/Activity/Create", "~/API/Activity/Update", "~/API/Activity/Destroy")]
    [ModelDataBindings(TableName = "Inventory_LocationList", KeyFieldName = "Inventory_Location_Key")]
    [Report(NotReportable = true)]
    public sealed class InventoryLocationListModel
    {

        [Key]
        //[IrisGridColumn(DefaultValue = "1600000000")]
        public string Inventory_Location_Key { get; set; }

        public string NameDesc { get; set; }

        public string Active { get; set; }

        [IrisGridColumn(Hidden = true, IncludeInMenu = false)]
        [ForeignKey(typeof(CountyModel))]
        public string County_Key { get; set; }
    }
}
