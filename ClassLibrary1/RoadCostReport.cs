namespace ReportLibrary
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for RoadCostReport.
    /// </summary>
    public partial class RoadCostReport : Telerik.Reporting.Report
    {
        private static ObjectDataSource _sqlSource = null;
        public static ObjectDataSource SqlSource { get { return _sqlSource; } set { _sqlSource = value; } }
        public RoadCostReport()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
    }
}