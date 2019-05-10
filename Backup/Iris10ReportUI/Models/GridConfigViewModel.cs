using System.ComponentModel;

namespace Iris10ReportUI.Models
{
    public sealed class GridConfigViewModel
    {
        [ReadOnly(true)]
        public string Column { get; set; }
        [ReadOnly(true)]
        public int ColumnIndex { get; set; }
        [ReadOnly(true)]
        public bool ShowColumn { get; set; }
        [ReadOnly(true)]
        public bool Required { get; set; }
        [ReadOnly(true)]
        public bool CopyDown { get; set; }
        [ReadOnly(true)]
        public bool ModelDisabled { get; set; }
        [ReadOnly(true)]
        public string SortOrder { get; set; }
    }
}