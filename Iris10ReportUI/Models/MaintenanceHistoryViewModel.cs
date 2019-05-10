using Iris10ReportUI.GridBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Iris10ReportUI.Models
{
    [ModelDataBindings(KeyFieldName = "EMSHistory_Key", TableName = "EMSHistory")]
    public class MaintenanceHistoryViewModel
    {
        public string EMSHistory_Key { get; set; }

        [Display(Name = "Maintenance Date")]
        public DateTime? MaintenanceDate { get; set; }

        [Display(Name = "Maintenance Policy")]
        public string EMSPolicy_Key { get; set; }

        [Display(Name = "Hours")]
        public string LastReadingHours { get; set; }

        [Display(Name = "Miles")]
        public string LastReadingMiles { get; set; }

        [Display(Name = "Service Description")]
        public string ServiceDescription { get; set; }

        [Display(Name = "Work Order Number")]
        public string EMSWorkOrder_Key { get; set; }

        [Display(Name = "Work Order Description")]
        public string WorkOrderDescription { get; set; }


    }
}
