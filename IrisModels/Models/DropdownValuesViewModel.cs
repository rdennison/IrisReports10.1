using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IrisModels.Models
{
    public class DropdownValuesViewModel
    {
        public string Text { get; set; }

        public string Value { get; set;}

        public bool Selected { get; set; }

        public bool Disabled { get; set; }

        public string Group { get; set; }
    }
}