using Iris10ReportUI.GridBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Iris10ReportUI.Models
{
    [ModelDataBindings(KeyFieldName = "EMSWorkOrder_Key", TableName = "EMSWorkOrder")]

    public class WorkOrderPartialViewModel

    {
        [Key]
        public string EMSWorkOrder_Key { get; set; }

        [Display(Name = "Equipment")]
        public string Equipment_Key { get; set; }

        [Display(Name = "Work Order Number")]
        public string WorkOrderNumber { get; set; }


        [Display(Name = "Work Order Description")]
        public string Description { get; set; }

        [Display(Name = "Location")]
        public string Location_Key { get; set; }

        [Display(Name = "Status")]
        public string EMSWorkOrderStatus_Key { get; set; }

        [Display(Name = "Date In")]
        public DateTime? DateTimeIn { get; set; }

        [Display(Name = "Date Out")]
        public DateTime? DateTimeOut { get; set; }

        [Display(Name = "Date Time Closed")]
        public DateTime? DateTimeClosed { get; set; }

        [Display(Name = "Miles At Service")]
        public int MilesAtService { get; set; }

        [Display(Name = "Hours At Service")]
        public int HoursAtService { get; set; }

        [Display(Name = "Requested By")]
        public string RequestedBy { get; set; }
       
        [Display(Name = "Posted to Maintenance History")]
        public bool PostedToMaintenanceHistory { get; set; }

        [MaxLength(100)]
        [Display(Name = "User 1")]
        public string User1 { get; set; }

        [MaxLength(100)]
        [Display(Name = "User 2")]
        public string User2 { get; set; }

        [MaxLength(100)]
        [Display(Name = "User 3")]
        public string User3 { get; set; }

        [MaxLength(100)]
        [Display(Name = "User 4")]
        public string User4 { get; set; }

        [MaxLength(100)]
        [Display(Name = "User 5")]
        public string User5 { get; set; }

        [MaxLength(100)]
        [Display(Name = "User 6")]
        public string User6 { get; set; }

        [MaxLength(100)]
        [Display(Name = "User 7")]
        public string User7 { get; set; }

        [MaxLength(100)]
        [Display(Name = "User 8")]
        public string User8 { get; set; }

        [MaxLength(100)]
        [Display(Name = "User 9")]
        public string User9 { get; set; }

        [MaxLength(100)]
        [Display(Name = "User 10")]
        public string User10 { get; set; }
    }
}