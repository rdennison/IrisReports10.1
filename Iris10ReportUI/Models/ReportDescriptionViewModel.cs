using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Iris10ReportUI.Models
{
    public sealed class ReportDescriptionViewModel
    {
        public bool Success { get; set; }
        public string Description { get; set; }
        public SelectList DescriptionList { get; set; }
    }
}