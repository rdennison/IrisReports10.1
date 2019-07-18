using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Iris10ReportUI.Models
{
    public class EditLastPMViewModel
    {
        [Display(Name = "Equipment")]
        public string Equipment_Key { get; set; }

        [Display(Name = "Policy")]
        public string EMSPolicy_Key { get; set; }

        [Display(Name = "PM Method")]
        public string EMSPMMethod_Key { get; set; }

        [Display(Name = "Last PM")]
        public string LastPM { get; set; }

        [Display(Name = "Next PM Due")]
        public string NextPMDue { get; set; }

        [Display(Name = "Maintenance Date")]
        public DateTime? MaintenanceDate { get; set; }

        [Display(Name = "Past Due")]
        public bool IsPMDue { get; set; }
    }
}