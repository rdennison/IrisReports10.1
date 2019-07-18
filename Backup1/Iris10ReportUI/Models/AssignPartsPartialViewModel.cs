using Iris10ReportUI.GridBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Iris10ReportUI.Models
{

    [ModelDataBindings(KeyFieldName = "EMSWorkOrder_Key", TableName = "EMSWorkOrder")]
    public class AssignPartsPartialViewModel
    {

       
        [Display(Name = "Equipment")]
        public string Equipment_Key { get; set; }


      
        [Display(Name = "EMS Part")]   
        public string EMSPart_Key { get; set; }

        [Display(Name = "Quantity")]
        public decimal Quantity { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}