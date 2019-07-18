using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Iris10ReportUI.Models
{
    public sealed class GisViewModel
    {
        public string Model_Key { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double? XCoord { get; set; }
        public double? YCoord { get; set; }
        public double? ZCoord { get; set; }
    }
}