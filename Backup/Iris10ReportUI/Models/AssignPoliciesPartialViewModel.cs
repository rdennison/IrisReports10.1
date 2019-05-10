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

    public class AssignPoliciesPartialViewModel
    {


        //public string EMSPolicyList_Key { get; set; }

        [Display(Name = "Policy")]
        public string EMSPolicy_Key { get; set; }

        [Display(Name = "PM Method")]
        public string EMSPMMethod_Key { get; set; }

        [Display(Name = "Last PM")]
        public string LastPM { get; set; }

        [Display(Name = "Next PM Due")]
        public string NextPMDue { get; set; }



        //public IEnumerable<EMSPolicyListModel> EMSPolicyList { get; set; }


        //public IEnumerable<EMSPMMethodModel> EMSPMMethod { get; set; }

        //public IEnumerable<EMSPolicyModel> Policy { get; set; }


    }
}