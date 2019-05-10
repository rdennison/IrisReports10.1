using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Iris10ReportUI.Models
{
    public class DropdownViewModel
    {
        public bool Disabled { get; set; }
        public string Group { get; set; }
        public bool Selected { get; set; }
        public string Text { get; set; }
        public string Value { get; set; }
    }
}