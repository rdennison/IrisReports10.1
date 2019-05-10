using Iris10ReportUI.GridBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Iris10ReportUI.Models
{
    [ModelDataBindings(KeyFieldName = "EMSPartList_Key", TableName = "EMSPartList")]
    public class AssignedPartsViewModel
    {
        public string EMSPartList_Key { get; set; }

        [Display(Name = "Part")]
        public string EMSPart_Key { get; set; }

        [Display(Name = "Quantity Required")]
        public decimal Quantity { get; set; }

        [Display(Name = "UOM")]
        public string UOM_Key { get; set; }

        [Display(Name = "Qunatity On Hand")]
        public decimal EMSPartQuantity { get; set; }

        [Display(Name = "Part Number")]
        public string EMSMfgPartNum { get; set; }

        [Display(Name = "Vendor")]
        public string Vendor_Key { get; set; }




        [Display(Name = "Part Number")]
        public string MfgPartNum { get; set; }

        [Display(Name = "Category")]

        public string EMSLookupPartCategory_Key { get; set; }




    }
}