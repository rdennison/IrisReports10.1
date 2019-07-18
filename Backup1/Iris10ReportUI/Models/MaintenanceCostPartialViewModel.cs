using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Iris10ReportUI.Models
{
    public class MaintenanceCostPartialViewModel
    {

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Task Date")]
        //[IrisGridColumn(Width = 180)]
        public DateTime? TaskDate { get; set; }

        [Display(Name = "Activity")]
        public string Activity_Key { get; set; }

        [Display(Name = "Employee")]
        public string Employee_Key { get; set; }

        [Display(Name = "Resource Type")]
        public string Resource_Type_Key { get; set; }

        [Display(Name = "Pay Type")]
        public string PayType_Key { get; set; }

        [Display(Name = "Quantity")]
        public decimal Quantity { get; set; }

        [Display(Name = "Bill Rate")]
        public decimal? Bill_Rate { get; set; }


        [Display(Name = "Resource Rate")]
        public decimal? Resource_Rate { get; set; }

        [Display(Name = "Extended Cost")]
        public decimal? Extended_Cost { get; set; }

        [Display(Name ="Inventory/Location")]
        public string Inventory_Location_Key { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }







    }
}