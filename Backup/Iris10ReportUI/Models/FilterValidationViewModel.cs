using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using IrisAttributes;


namespace Iris10ReportUI.Models
{
    public class FilterValidationViewModel
    {
        [Required(ErrorMessage = "{0} is required.")]
        public string FilterName { get; set; }

        [RequiredIfOtherFieldIsNullAttribute(new string[] {"Group1", "Value1", "Operator1", "Value2", "Group2"})]
        public string Description { get; set; }

        [RequiredIfOtherFieldIsNotNullAttribute(new string[] { "Description", "Value1" })]
        public string Operator1 { get; set; }

        [RequiredIfOtherFieldIsNotNullAttribute(new string[] { "Description", "Operator1" })]
        public string Value1 { get; set; }

        [RequiredIfOtherFieldIsSpecificValueAttribute(new string[] {"Operator1"},"BETWEEN")]
        public string Value2 { get; set; }    

        [RequiredIfOtherFieldIsNotNullAttribute(new string[] { "Description" })]
        public string Group1 { get; set; }

        [RequiredIfOtherFieldIsNotNullAttribute(new string[] { "Group1" })]
        public string Group2 { get; set; }


    }
}