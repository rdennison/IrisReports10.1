namespace ReportLibrary
{
    using System;
    using System.Collections.Generic;
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
        private static List<ReportParameter> _reportParameters = null;
        public static List<ReportParameter> ReportParameterList { get { return _reportParameters; } set { _reportParameters = value; } }
        public RoadCostReport() 
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            this.DataSource = SqlSource;
            //this.ReportParameters.AddRange(ReportParameterList);
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
    }
}