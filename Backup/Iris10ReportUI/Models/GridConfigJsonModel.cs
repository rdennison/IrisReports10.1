using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Iris10ReportUI.Models
{
    public class GridConfigJsonModel
    {
        public string ColumnField { get; set; }
        public string Column { get; set; }
        public int ColumnIndex { get; set; }
        public bool Required { get; set; }
        public bool CopyDown { get; set; }
        public bool Hidden { get; set; }
        public string SortOrder { get; set; }
    }
}