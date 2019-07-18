using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Iris10ReportUI.Models
{
    public class MaintenanceHistoryPartialViewModel
    {
        [Display(Name ="Equipment")]
        public string Equipment_Key { get; set; }

        [Display(Name = "PM Method")]
        public string EMSPMMethod_Key { get; set; }

        [Display(Name = "Maintenance Date")]
        public DateTime? MaintenanceDate { get; set; }

        [Display(Name = "Employee")]
        public string Employee_Key { get; set; }

        [Display(Name = "Repair Type")]
        public string EMSRepairType_Key { get; set; }

        [Display(Name = "Policy")]
        public string EMSPolicy_Key { get; set; }
       
        [Display(Name = "Last PM Reading Hours")]
        public int LastReadingHours { get; set; }

        [Display(Name = "Last PM Reading Miles")]
        public int LastReadingMiles { get; set; }

        [Display(Name = "Vendor")]
        public string Vendor_Key { get; set; }

        [Display(Name = "Service Description")]
        public string ServiceDescription { get; set; }

        [Display(Name = "Work Order Number")]
        public string EMSWorkOrder_Key { get; set; }

        [Display(Name = "Work Order Description")]
        public string WorkOrderDescription { get; set; }


        [Display(Name = "Posted From Work Order")]
        public bool PostedFromWorkOrder { get; set; }


        [Display(Name = "User 1")]
        public string User1 { get; set; }

        [Display(Name = "User 2")]
        public string User2 { get; set; }

        [MaxLength(100)]
        [Display(Name = "User 3")]
        public string User3 { get; set; }

        [Display(Name = "User 4")]
        public string User4 { get; set; }

        [Display(Name = "User 5")]
        public string User5 { get; set; }

        [Display(Name = "User 6")]
        public string User6 { get; set; }

        [Display(Name = "User 7")]
        public string User7 { get; set; }

        [Display(Name = "User 8")]
        public string User8 { get; set; }

        [Display(Name = "User 9")]
        public string User9 { get; set; }

        [Display(Name = "User 10")]
        public string User10 { get; set; }




    }
}