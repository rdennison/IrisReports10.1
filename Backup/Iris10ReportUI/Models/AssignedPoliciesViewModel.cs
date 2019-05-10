using IrisModels.Models;
using Iris10ReportUI.GridBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Iris10ReportUI.Models
{
    [ModelDataBindings(KeyFieldName = "EMSPolicyList_Key", TableName = "EMSPolicyList")]

    public class AssignedPoliciesViewModel
    {


        public string EMSPolicyList_Key { get; set; }


        public string EMSPolicy_Key { get; set; }

        public string EMSPMMethod_Key { get; set; }

        [Display(Name = "Last PM")]
        public string LastPM { get; set; }

        [Display(Name = "Next PM Due")]
        public string NextPMDue { get; set; }

        [Display(Name = "Past Due")]
        public bool ISPMDue { get; set; }



        public IEnumerable<EMSPolicyListModel> EMSPolicyList { get; set; }


        public IEnumerable<EMSPMMethodModel> EMSPMMethod { get; set; }

        public IEnumerable<EMSPolicyModel> Policy { get; set; }


    }
}