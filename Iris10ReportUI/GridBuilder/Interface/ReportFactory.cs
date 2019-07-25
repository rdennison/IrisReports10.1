using CoreDomain;
using IrisModels.Models;
using SqlComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using Telerik.Reporting;
using Telerik.Reporting.Processing;

namespace Iris10ReportUI.GridBuilder.Interface
{
    public static class ReportFactory
    {
        private static readonly CoreService _coreService = new CoreService();

        public static TypeReportSource GetReport(List<GridFilterWhereModel> filterlist)
        {
            string db = HttpContext.Current.Session["ConString"].ToString();
            string reportName = HttpContext.Current.Session["ReportName"].ToString();
            string reportModelName = HttpContext.Current.Session["ReportModelName"].ToString();
            Type reportType = null;

            if (db != null && reportName != null && reportModelName != null)
            {
                Assembly a = Assembly.Load("ReportLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
                reportType = a.GetType("ReportLibrary." + reportName);
                
                SqlGenerator gen = new SqlGenerator(SqlGenerator.SqlTypes.Select, reportModelName);
                foreach (var row in filterlist)
                {
                    gen.AddWhereParameter(new SqlWhere(row.OpenGroup, row.CloseGroup, reportModelName, row.ColumnName, row.Value1, row.Value2, row.ComparisonOperator, GetAndOrSyntax(row.AndOr)));
                }
                DataTable dt = SQLHelper.FetchDataTable(gen, db);
                Telerik.Reporting.ObjectDataSource source = new ObjectDataSource();
                source.DataSource = dt;
                SetCalculatedFields(db, source);
                reportType.GetProperty("SqlSource").SetValue(reportType, source);
                //reportType.GetProperty("ReportParameterList").SetValue(reportType, GetParameters(db, source));
                TypeReportSource reportSource = new TypeReportSource() { TypeName = "ReportLibrary." + reportName + ", ReportLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" };
                System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;
                var task = new Task(() => { CreateReportLog(gen, db, reportSource, session); });
                task.Start();
                
                return reportSource;
            }
            return null;
        }
        
        public static void SetCalculatedFields(string db, Telerik.Reporting.ObjectDataSource source)
        {
            int reportKey = (int) HttpContext.Current.Session["Report_Key"];
            List<SqlWhere> wheres = new List<SqlWhere>();
            List<ReportComputedColumnModel> reportComputedColumnsFromDatabase = new List<ReportComputedColumnModel>();
            wheres.Add(new SqlWhere(null, null, "ReportComputedColumn", "Report_Key", reportKey, null, SqlWhereComparison.SqlComparer.Equal, SqlWhereAndOrOptions.SqlWhereAndOr.And));
            reportComputedColumnsFromDatabase = _coreService.LoadModel<ReportComputedColumnModel>(wheres, conName: db).ToList();
            
            foreach (ReportComputedColumnModel rcc in reportComputedColumnsFromDatabase)
            {
                source.CalculatedFields.Add(new CalculatedField(rcc.ColumnName, typeof(String), rcc.ColumnValue));
            }
        }

        public static List<ReportParameter> GetParameters(string db, Telerik.Reporting.ObjectDataSource source)
        {
            int reportKey = (int) HttpContext.Current.Session["Report_Key"];
            List<SqlWhere> wheres = new List<SqlWhere>();
            List<ReportParameterModel> reportParametersFromDatabase = new List<ReportParameterModel>();
            List<ReportParameter> reportParameterSet = new List<ReportParameter>();
            wheres.Add(new SqlWhere(null, null, "ReportParameter", "Report_Key", reportKey, null, SqlWhereComparison.SqlComparer.Equal, SqlWhereAndOrOptions.SqlWhereAndOr.And));
            reportParametersFromDatabase = _coreService.LoadModel<ReportParameterModel>(wheres, conName: db).ToList();

            foreach (ReportParameterModel rp in reportParametersFromDatabase)
            {
                ReportParameter trp = new ReportParameter();
                trp.AvailableValues.DataSource = source;
                trp.AvailableValues.ValueMember = rp.ParameterValue;
                trp.Name = rp.ParameterName;
                trp.Value = rp.ParameterValue;
                reportParameterSet.Add(trp);
            }
            return reportParameterSet;
        }

        private static void CreateReportLog(SqlGenerator gen, string db, TypeReportSource typeReportSource, System.Web.SessionState.HttpSessionState session)
        {
            ReportProcessor reportProcessor = new ReportProcessor();
            //RenderingResult result = reportProcessor.RenderReport("HTML5", typeReportSource, null);

            ReportLogModel reportLog = new ReportLogModel
            {
                Report_Key = (int) session["Report_Key"],
                Criteria = gen.SqlDebug,
                PageCount = 0,//result.PageCount,
                ErrorMessage = "",// result.HasErrors ? result.Errors[0].Message : "",
                CreatedByUser_Key = (int) session["CurrentUserKey"],
                UpdatedByUser_Key = (int) session["CurrentUserKey"]
            };

            _coreService.SprocInsert(reportLog, db);
        }

        private static SqlWhereAndOrOptions.SqlWhereAndOr GetAndOrSyntax(string AndOr = null, SqlWhereAndOrOptions.SqlWhereAndOr AndOr2 = SqlWhereAndOrOptions.SqlWhereAndOr.And)
        {
            if (AndOr != null)
            {
                if (AndOr == "Or")
                {
                    return SqlWhereAndOrOptions.SqlWhereAndOr.Or;
                }
                else if (AndOr == "And")
                {
                    return SqlWhereAndOrOptions.SqlWhereAndOr.And;
                }
            }
            else
            {
                return AndOr2;
            }
            return SqlWhereAndOrOptions.SqlWhereAndOr.Or;
        }
    }
}