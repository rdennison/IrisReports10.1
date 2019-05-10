using Iris10ReportUI.GridBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Iris10ReportUI.Models
{
    [ModelDataBindings(KeyFieldName = "EMSWorkOrder_Key", TableName = "EMSWorkOrder")]

    public class WorkOrderViewModel

    {
        [Key]
        public string EMSWorkOrder_Key { get; set; }

        [Display(Name = "Equipment")]
        public string Equipment_Key { get; set; }

        [Display(Name = "Work Order Number")]
        public string WorkOrderNumber { get; set; }


        [Display(Name = "Status")]
        public string EMSWorkOrderStatus_Key { get; set; }

        [Display(Name = "Date In")]
        public DateTime? DateTimeIn { get; set; }

        [Display(Name = "Date Out")]
        public DateTime? DateTimeOut { get; set; }

        [Display(Name = "Work Order Description")]
        public string Description { get; set; }





    }
}