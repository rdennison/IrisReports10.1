using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Reporting;

namespace ReportLibrary
{
    public class ReportParams : Telerik.Reporting.Report
    {
        private static ObjectDataSource _sqlSource = null;
        public static ObjectDataSource SqlSource { get { return _sqlSource; } set { _sqlSource = value; } }
        private static List<ReportParameter> _reportParameters = null;
        public static List<ReportParameter> ReportParameterList { get { return _reportParameters; } set { _reportParameters = value; } }
    }
}
