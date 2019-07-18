using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Iris10ReportUI.Models
{
    public class ARSInvoicePaymentViewModel
    {
        [Key]
        public string ARSInvoice_Key { get; set; }

        [Display(Name = "Invoice Number")]
        public string InvoiceNumber { get; set; }

        [Display(Name = "Invoice Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? InvoiceDate { get; set; }

        [Display(Name = "Invoice Total")]
        public decimal InvoiceTotal { get; set; }

        [Display(Name = "Received From")]
        public string ReceivedFrom { get; set; }
    }
}