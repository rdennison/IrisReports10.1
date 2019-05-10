using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Iris10ReportUI.Models
{
    public class GridConfigDefaultSpecificViewModel
    {
        //Must be lower case to mimic coming from js exactly
        public string columnField { get; set; }
        public string column { get; set; }
        public int columnIndex { get; set; }
        public bool required { get; set; }
        public bool copyDown { get; set; }
        public bool hidden { get; set; }
        public string sortorder { get; set; }
    }
}