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
  
        public RoadCostReport(string sql = null)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            if(sql != null)
            {
                this.sqlDataSource1.SelectCommand = sql;
            }
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
    }
}