
using Iris10ReportUI.GridBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Iris10ReportUI.Models
{
    [ModelDataBindings(KeyFieldName = "EMSEmployeeHistory_Key", TableName = "EMSEmployeeHistory")]
    public class RelatedEmployeeHistoryViewModel
    {
        [Key]
        public string EmployeeHistory_Key { get; set; }

        public string Equipment_Key { get; set; }

        [Display(Name = "Employee")]
        public string Employee_Key { get; set; }

        [Display(Name = "Begin Hours")]
        public int? LifeTimeTotalHours { get; set; }

        [Display(Name = "Begin Miles")]
        public int? LifeTimeTotalMiles { get; set; }

        [Display(Name = "End Hours")]
        public int? EndingLifetimeTotalHours { get; set; }

        [Display(Name = "End Miles")]
        public int? EndingLifetimeTotalMiles { get; set; }

        [Display(Name = "Assign Date")]
        public DateTime? AssignedDate { get; set; }

        [Display(Name = "Reassign Date")]
        public DateTime? ReassignedDate { get; set; }


    }
}