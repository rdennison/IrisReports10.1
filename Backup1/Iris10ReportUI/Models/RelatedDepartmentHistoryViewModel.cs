using Iris10ReportUI.GridBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Iris10ReportUI.Models
{
    [ModelDataBindings(KeyFieldName = "EMSDepartmentHistory_Key", TableName = "EMSDepartmentHistory")]
    public class RelatedDepartmentHistoryViewModel
    {
        public string EMSDepartmentHistory_Key { get; set; }

        [Display(Name = "Department")]
        public string Department_Key { get; set; }

        [Display(Name = "Assign Date")]
        public DateTime? AssignedDate { get; set; }

        [Display(Name = "Begind Miles")]
        public int LifetimeTotalMiles { get; set; }

        [Display(Name = "End Miles")]
        public int EndingLifetimeTotalMiles { get; set; }

        [Display(Name = "Begin Hours")]
        public int LifetimeTotalHours { get; set; }

        [Display(Name = "End Hours")]
        public int EndingLifetimeTotalHours { get; set; }

        [Display(Name = "Reassign Date")]
        public DateTime? ReassignedDate { get; set; }

    }
}
