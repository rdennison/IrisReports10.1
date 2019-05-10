using Iris10ReportUI.GridBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Iris10ReportUI.Models
{
    [ModelDataBindings(KeyFieldName = "EMSEmployeeHistory_Key", TableName = "EMSEmployeeHistory")]
    public class ViewRelatedEmployeeHistoryPartialViewModel
    {

        [Display(Name = "Employee")]
        public string Employee_Key { get; set; }
        [Display(Name = "Assign Date")]
        public DateTime? AssignedDate { get; set; }

        [Display(Name = "Reassign Date")]
        public DateTime? ReassignedDate { get; set; }


        [Display(Name = "Lifetime Total Hours At Assignment")]
        public int? LifeTimeTotalHours { get; set; }

        [Display(Name = "Lifetime Total Miles At Assignment")]
        public int? LifeTimeTotalMiles { get; set; }

        [Display(Name = "Lifetime Total Hours At Reassignement")]
        public int? EndingLifetimeTotalHours { get; set; }

        [Display(Name = "Lifetime Total Miles At Reassigement")]
        public int? EndingLifetimeTotalMiles { get; set; }

 
    }
}