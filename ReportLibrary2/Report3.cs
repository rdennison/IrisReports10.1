using System;
using System.ComponentModel;
using System.Drawing;
// using System.Windows.Forms;
using Telerik.Reporting;
using Telerik.Reporting.Drawing;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using ReportFormat.Model;
using Newtonsoft.Json;
using ReportLibrary2.Models;
using System.Web.Script.Serialization;
using System.Text;

namespace ReportLibrary2
{
    /// <summary>
    /// Summary description for Report3.
    /// </summary>
    public partial class Report3 : Report
    {
        public static ReportHeaderSection ReportHeader = new ReportHeaderSection();
        public static ReportFooterSection ReportFooter = new ReportFooterSection();
        public static PageFooterSection PageFooter = new PageFooterSection();
        public static PageHeaderSection PageHeader = new PageHeaderSection();
        public static DetailSection Detail = new DetailSection();
        public static string SqlConnectionString;
        public static string SqlCommandString;
        public static string CountyName;
        public static string SortOption;
        public static SortDirection MySortDirection;
        public static bool SortMe = false;
        public static SqlDataSource SqlDataSource1 = new SqlDataSource();
        public static Report Report1 = new Report();
        public static string StartDate;
        public static string EndDate;
        public static bool DateFilter = false;
        public static bool GroupBy = false;
        public static string MyGrouping = "";
        public static string ReportName = "Report";

        //TextBoxes always around
        public static TextBox currentTime;
        public static TextBox datePrinted;
        public static TextBox currentPage;
        public static TextBox totalPages;
        public static TextBox displayPage;
        public static TextBox displayOf;

        //TextBox Vars
        private static int count = 0;
        public static TextBox[] MyCaptionBoxes = new TextBox[50];
        public static TextBox[] MyDataBoxes = new TextBox[50];
        public static double CapLocX = 0.1D;
        public static double CapLocY = 0.14083333395421505D;
        public static double GroupLoc = 0.14083333395421505D;
        public static double TBHeight = 0.44166669249534607D;

        //Groups
        public static bool AddReportFooter = true;
        public static bool AddReportHeader = true;
        public static GroupHeaderSection[] HeaderSections = new GroupHeaderSection[50];
        public static GroupFooterSection[] FooterSections = new GroupFooterSection[50];
        public static Telerik.Reporting.Group[] AllGroups = new Telerik.Reporting.Group[50];
        public static int GroupCount = 0;
        public static int Summing = 1;
        public static int Counting = 0;
        //public static SizeU TBSize = new SizeU(Unit.Inch(CapLocX), Unit.Inch(TBHeight));
        public static int ColorR = 121;
        public static int ColorG = 167;
        public static int ColorB = 227;


        public Report3()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent2();
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        public static void GenerateReport(string reportName, string cs, string comS, string props, string columnAttribs, string[] groups, List<StringBuilder> filters = null, Dictionary<string,string> typeList = null)
        {
            var p = props.Split(',');
            Dictionary<string,int> duplicateCheck = new Dictionary<string,int>();
            List<GroupViewModel> reportGroups = new List<GroupViewModel>();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            foreach(var group in groups)
            {
                reportGroups.Add((GroupViewModel)serializer.Deserialize(group, typeof(GroupViewModel)));
            }

            List<ColumnAttributeViewModel> columns = (List<ColumnAttributeViewModel>)serializer.Deserialize(columnAttribs, typeof(List<ColumnAttributeViewModel>));

            if(filters != null)
            {
                StringBuilder filter = new StringBuilder();
                foreach(var s in filters)
                {
                    filter.Append(s);
                }
                AddReportFooterFilters(filter.ToString()); //adds filter criteria to the report footer
                AddReportHeaderFilters(filter.ToString()); // adds filter criteria to the report header
            }
            //Debug.WriteLine("JSON: " + json);
            //var data = new ReportModel(json);
            GenerateHeaderFooters();
            AddReportHeaderSection(reportName);
            for (int k = 0; k < p.Length; k++)
            {
                bool isGroup = false;
                foreach (var group in reportGroups)
                {
                    if (group != null)
                        if (group.field == p[k])
                            isGroup = true;
                }
                if (!isGroup)
                {
                    ColumnAttributeViewModel col = new ColumnAttributeViewModel();
                    foreach (var column in columns)
                    {
                        if (column.field == p[k].Replace(".", ""))
                        {
                            col = column;
                        }
                    }
                    if (duplicateCheck.ContainsKey(p[k].Split('.')[1]))
                    {
                        duplicateCheck[p[k].Split('.')[1]] = duplicateCheck[p[k].Split('.')[1]] + 1;
                        GenerateTextField(p[k].Split('.')[1], "=Fields." + p[k].Split('.')[1] + duplicateCheck[p[k].Split('.')[1]].ToString(), col, typeList[p[k].Split('.')[1]]);
                    }
                    else
                    {
                        duplicateCheck.Add(p[k].Split('.')[1], 0);
                        GenerateTextField(p[k].Split('.')[1], "=Fields." + p[k].Split('.')[1], col, typeList[p[k].Split('.')[1]]);
                    }
                }
            }
            foreach(var group in reportGroups)
            {
                ColumnAttributeViewModel col = new ColumnAttributeViewModel();
                
                if (group != null)
                {
                    foreach (var column in columns)
                    {
                        if (column.field == group.field.Replace(".", ""))
                        {
                            col = column;
                        }
                    }
                    Groupings(group.field.Split('.')[1] + ":", "=Fields." + group.field.Split('.')[1], col);
                }
                    
            }
            ChangeSqlString(cs);
            SqlCommandString = comS;
        }

        public static void TestFunction(List<string> myModel)
        {
           // Debug.WriteLine("This is sparta! " + myModel[0] + " " + myModel[0]);
        }

        public static void AddDateFilter(string start,string end)
        {
            StartDate = start;
            EndDate = end;
            Debug.WriteLine(StartDate + " " + EndDate);
            DateFilter = true;
        }

       

        public static void ChangeSqlString(string sqlCon)
        {
            SqlConnectionString = sqlCon;
           // Debug.WriteLine("THIS IS FROM THE REPORT " + SqlConnectionString);
        }

        public static void GenerateHeaderFooters()
        {
            for (int i = 0; i < 50; i++)
            {
                HeaderSections[i] = new GroupHeaderSection();
                FooterSections[i] = new GroupFooterSection();
                AllGroups[i] = new Telerik.Reporting.Group();
                HeaderSections[i].Height = Unit.Inch(TBHeight);
                HeaderSections[i].PrintOnEveryPage = true;
                HeaderSections[i].Style.Font.Bold = true;
                //HeaderSections[i].Style.BackgroundColor = Color.FromArgb(red: 224, green: 224, blue: 224);
                FooterSections[i].Height = Unit.Inch(TBHeight);
                AllGroups[i].GroupFooter = FooterSections[i];
                AllGroups[i].GroupHeader = HeaderSections[i];
            }
        }

        public static void AddReportHeaderFilters(string data)
        {
            var filterBox = new TextBox();
            filterBox = GenerateAttributes(new PointU(Unit.Inch(0.1D), Unit.Inch(0.05D)), data, "filterBox", format: "{0}");
            filterBox.Size = new SizeU(Unit.Inch(6.32D), Unit.Inch(0.2D));
            filterBox.Size = new SizeU(Unit.Inch(3.12D), Unit.Inch(0.2D));
            filterBox.Style.TextAlign = HorizontalAlign.Left;
            //ReportHeader.Items.Add(filterBox);
            PageHeader.Items.Add(filterBox);
        }

        public static void AddReportFooterFilters(string data)
        {
            var filterBox = new TextBox();
            filterBox = GenerateAttributes(new PointU(Unit.Inch(0.1D), Unit.Inch(0.05D)), data, "filterBox", format: "{0}");
            filterBox.Size = new SizeU(Unit.Inch(6.32D), Unit.Inch(0.2D));
            ReportFooter.Items.Add(filterBox);
        }

        public static void AddReportFooterSection(int sumOrCount, string section, string name)
        {
            var sumCount = new TextBox();
            var totalBox = new TextBox();
            int spot = GetPosition(name);

            if (sumOrCount == 1)
            {
                sumCount = GenerateAttributes(MyCaptionBoxes[spot].Location, "= Sum(" + section + ")", name, format: "{0:$#,0.00}");
            }
            else
            {
                sumCount = GenerateAttributes(MyCaptionBoxes[spot].Location, "= Count(" + section + ")", name, format: "{0:$#,0.00}");
            }
            ReportFooter.Items.Add(sumCount);
            totalBox = GenerateAttributes(MyCaptionBoxes[0].Location, value: "Grand Total: ", name: name, format: "{0:$#,0.00}");
            ReportFooter.Items.Add(totalBox);
            //ReportFooter.Style.BackgroundColor = Color.FromArgb(red: 89, green: 220, blue: 216);
            //AddReportFooter = true;
        }

        public static void AddReportHeaderSection(string name)
        {
            var reportName = new TextBox(); 
            reportName = GenerateAttributes(new PointU(Unit.Inch(1.0D), Unit.Inch(0.5D)), value: name, name: name, format: "{0}");
            reportName.Size = new SizeU(Unit.Inch(5D), Unit.Inch(1D));
            reportName.Style.Font.Size = Unit.Point(14D);
            reportName.Style.Font.Bold = true;
            reportName.Style.TextAlign = HorizontalAlign.Center;
            ReportHeader.Items.Add(reportName);
           // PageHeader.Items.Add(reportName);
            //ReportHeader.Style.BackgroundColor = Color.FromArgb(red: 89, green: 220, blue: 216);
            //AddReportHeader = true;
            SetupStaticTextboxes();
        }

        public static void SetupStaticTextboxes()
        {
            currentTime = new TextBox();
            datePrinted = new TextBox();
            currentPage = new TextBox();
            totalPages = new TextBox();
            displayPage = new TextBox();
            displayOf = new TextBox();
            currentTime.Format = "{0:d}";
            currentTime.Location = new PointU(Unit.Inch(5.5D), Unit.Inch(0D));
            currentTime.Name = "currentTimeTextBox";
            currentTime.Size = new SizeU(Unit.Inch(0.95829421281814575D), Unit.Inch(0.20000000298023224D));
            currentTime.Style.TextAlign = HorizontalAlign.Right;
            currentTime.Value = "=NOW()";
            currentPage.Location = new PointU(Unit.Inch(3.0000002384185791D), Unit.Inch(0.0416666679084301D));
            currentPage.Name = "currentPage";
            currentPage.Size = new SizeU(Unit.Inch(0.3999999463558197D), Unit.Inch(0.20000000298023224D));
            currentPage.Style.TextAlign = HorizontalAlign.Center;
            currentPage.Value = "=PageNumber";
            datePrinted.Location = new PointU(Unit.Inch(4.7000002861022949D), Unit.Inch(0D));
            datePrinted.Name = "datePrinted";
            datePrinted.Size = new SizeU(Unit.Inch(0.90000004768371582D), Unit.Inch(0.20000000298023224D));
            datePrinted.Style.TextAlign = HorizontalAlign.Right;
            datePrinted.Value = "Date Printed";
            totalPages.Location = new PointU(Unit.Inch(3.7000000476837158D), Unit.Inch(0.0416666679084301D));
            totalPages.Name = "totalPages";
            totalPages.Size = new SizeU(Unit.Inch(0.40000024437904358D), Unit.Inch(0.20000000298023224D));
            totalPages.Style.TextAlign = HorizontalAlign.Center;
            totalPages.Value = "=PageCount";
            displayPage.Location = new PointU(Unit.Inch(2.4999215602874756D), Unit.Inch(0.0416666679084301D));
            displayPage.Name = "displayPage";
            displayPage.Size = new SizeU(Unit.Inch(0.5D), Unit.Inch(0.20000000298023224D));
            displayPage.Style.TextAlign = HorizontalAlign.Center;
            displayPage.Value = "Page";
            displayOf.Location = new PointU(Unit.Inch(3.4000003337860107D), Unit.Inch(0.0416666679084301D));
            displayOf.Name = "displayOf";
            displayOf.Size = new SizeU(Unit.Inch(0.30000022053718567D), Unit.Inch(0.20000000298023224D));
            displayOf.Style.TextAlign = HorizontalAlign.Center;
            displayOf.Value = "of";
            PageFooter.Height = Unit.Inch(0.28125D);
            PageFooter.Name = "pageFooter";
            //ReportHeader.Items.Add(currentTime);
            //ReportHeader.Items.Add(datePrinted);
            PageHeader.Items.Add(currentTime);
            PageHeader.Items.Add(datePrinted);
            PageFooter.Items.Add(currentPage);
            PageFooter.Items.Add(totalPages);
            PageFooter.Items.Add(displayPage);
            PageFooter.Items.Add(displayOf);
        }

        public static void Groupings(string name, string val, ColumnAttributeViewModel col)
        {
          //  Debug.WriteLine("GroupBy: " + val);
            var myGroupBoxName = new TextBox();
            var myGroupBoxValue = new TextBox();
            myGroupBoxName = GenerateAttributes(new PointU(Unit.Inch(GroupLoc), Unit.Inch(CapLocY)), name, name, format: "{0}");
            myGroupBoxValue = GenerateAttributes(new PointU(Unit.Inch(GroupLoc + 1.4D), Unit.Inch(CapLocY)), val, name, format: "{0}");
            myGroupBoxName.Size = new SizeU(Unit.Inch(col.width / 72.0D), Unit.Inch(0.5D));
            myGroupBoxValue.Size = new SizeU(Unit.Inch(col.width / 72.0D), Unit.Inch(0.5D));
            GroupLoc += 0.2D;
            GroupCount++;

            HeaderSections[GroupCount]?.Items.AddRange(new ReportItemBase[] {
            myGroupBoxName,
            myGroupBoxValue});

            AllGroups[GroupCount].Groupings.Add(new Grouping(val));
        }

        public static void ChangeBandColor(int bs, int section, int r, int g, int b)
        {
            if (bs == 0)
                HeaderSections[section].Style.BackgroundColor = Color.FromArgb(r, g, b);
            else
                FooterSections[section].Style.BackgroundColor = Color.FromArgb(r, g, b);
        }

        public static void AddSortings(int groupNum, string field, SortDirection dir)
        {
           // Debug.WriteLine("YATA: " + groupNum + " field: " + field);
            AllGroups[groupNum].Sortings.Add(new Sorting(field, dir));
        }

        public static void SumOrCount(string name, int typeFlag, string field)
        {
            var sumCount = new TextBox();
            int spot = GetPosition(name);
            if (typeFlag == 1)
            {

                sumCount = GenerateAttributes(MyCaptionBoxes[spot].Location, "= Sum(" + field + ")", name: "", format: "{0:$#,0.00}");
                FooterSections[GroupCount]?.Items.Add(sumCount);
            }
            else if (typeFlag == 0)
            {
                sumCount = GenerateAttributes(MyCaptionBoxes[spot].Location, "= Count(" + field + ")", name: "", format: "{0:#,0}");
                FooterSections[GroupCount]?.Items.Add(sumCount);
            }
        }

        public static void GenerateTextField(string title, string data, ColumnAttributeViewModel col, string type) //Adds labels
        {
            var boxLoc = new PointU(Unit.Inch(CapLocX), Unit.Inch(CapLocY));
            if (count == 0)
            {
                boxLoc = new PointU(Unit.Inch(0.1D), Unit.Inch(CapLocY));
            }
            MyCaptionBoxes[count] = GenerateAttributes(boxLoc, title, title, format: "{0}");
            MyCaptionBoxes[count].Location = boxLoc;
            MyCaptionBoxes[count].Size = new SizeU(Unit.Inch(col.width / 72.0D), Unit.Inch(0.5D));
            HeaderSections[GroupCount]?.Items.Add(MyCaptionBoxes[count]);
            if (data.ToLower().Contains("date"))
                MyDataBoxes[count] = GenerateAttributes(boxLoc, data, title, format: "{0:MM/dd/yyyy}");
            else if(type == "money")
                MyDataBoxes[count] = GenerateAttributes(boxLoc, data, title, format: "{0:$#,0.00}");
            else
                MyDataBoxes[count] = GenerateAttributes(boxLoc, data, title, format: "{0}");
            MyDataBoxes[count].Size = new SizeU(Unit.Inch(col.width / 72.0D), Unit.Inch(0.5D));
            Detail?.Items.Add(MyDataBoxes[count]);
            //if(count != 0)
                CapLocX += MyDataBoxes[count].Size.Width.Value+0.1D;
            //else
              //  CapLocX = 0.4D;
            count++;
        }


        private static TextBox GenerateAttributes(PointU loc, string value, string name, string format)
        {
            var retBox = new TextBox();
            retBox.CanGrow = true;
            //retBox.Size = TBSize;
            retBox.Location = loc;
            retBox.Value = value;
            retBox.Name = name;
            retBox.Format = format;
            //if (Regex.IsMatch(name, pattern: "date", options: RegexOptions.IgnoreCase)== false && Regex.IsMatch(name, pattern: "last", options: RegexOptions.IgnoreCase)== false) //all number formats except date
            //{
            //    retBox.Format = format;
            //}
            return retBox;
        }

        private static int GetPosition(string name)
        {
            for (int i = 0; i < MyCaptionBoxes.Length; i++)
            {
                if (MyCaptionBoxes[i].Name == name)
                {
                    return i;
                }
            }
            return -1;
        }
        /// <summary>
        /// Refreshes the data on the telerik report so new data can be added.
        /// </summary>
        public static void ResetDefaults()
        {
            for (int i = 0; i < count; i++)
            {
                MyDataBoxes[i].Value = "";
                MyCaptionBoxes[i].Value = "";
                MyDataBoxes[i] = new TextBox();
                MyCaptionBoxes[i] = new TextBox();
                AllGroups[i] = new Telerik.Reporting.Group();
            }
            for (int i = 0; i < 50; i++)
            {
                HeaderSections[i]?.Items.Clear();
                FooterSections[i]?.Items.Clear();
                AllGroups[i]?.Groupings.Clear();

            }
            ReportHeader?.Items.Clear();
            ReportFooter?.Items.Clear();
            PageFooter?.Items.Clear();
            PageHeader?.Items.Clear();

            GroupCount = 0;
            Summing = 1;
            Counting = 0;
            GroupLoc = 0.14083333395421505D;

            Detail?.Items.Clear();
            count = 0;
            CapLocX = 0.1D;
        }


        private void InitializeComponent2()
        {
            
            ((ISupportInitialize)(this)).BeginInit();
            
            // 
            // sqlDataSource1
            // 
            SqlDataSource1.ConnectionString = SqlConnectionString;
            SqlDataSource1.Name = "sqlDataSource1";
            SqlDataSource1.SelectCommand = SqlCommandString;

            if (AddReportHeader) { this.Items.Add(ReportHeader); }
            this.Items.Add(PageHeader);
            this.DataSource = SqlDataSource1;
            for (int i = 0; i < (GroupCount + 1); i++)
            {
                this.Groups.Add(AllGroups[i]);
            }
            this.Items.Add(Detail);
            if (AddReportFooter) { this.Items.Add(ReportFooter); }
            this.Items.Add(PageFooter);

            //group1.GroupFooter = GroupFooter;
            //group1.GroupHeader = GroupHeader;
            //group1.Name = "labelsGroup";
            //this.Groups.AddRange(new Telerik.Reporting.Group[] {
            //group1});
            //this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            //GroupHeader,
            //GroupFooter,
            //PageHeader,
            //PageFooter,
            //ReportHeader,
            //ReportFooter,
            //DetailSection});
            //this.Name = "Report3";
            //this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D));
            //this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            //styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            //new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.TextItemBase)),
            //new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.HtmlTextBox))});
            //styleRule1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2D);
            //styleRule1.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(2D);
            //styleRule2.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            //new Telerik.Reporting.Drawing.StyleSelector("Title")});
            //styleRule2.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(34)))), ((int)(((byte)(77)))));
            //styleRule2.Style.Font.Name = "Calibri";
            //styleRule2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(18D);
            //styleRule3.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            //new Telerik.Reporting.Drawing.StyleSelector("Caption")});
            //styleRule3.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(ColorR)))), ((int)(((byte)(ColorG)))), ((int)(((byte)(ColorB)))));
            //styleRule3.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(34)))), ((int)(((byte)(77)))));
            //styleRule3.Style.Font.Name = "Calibri";
            //styleRule3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            //styleRule3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            //styleRule4.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            //new Telerik.Reporting.Drawing.StyleSelector("Data")});
            //styleRule4.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(34)))), ((int)(((byte)(77)))));
            //styleRule4.Style.Font.Name = "Calibri";
            //styleRule4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            //styleRule4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            //styleRule5.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            //new Telerik.Reporting.Drawing.StyleSelector("PageInfo")});
            //styleRule5.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(34)))), ((int)(((byte)(77)))));
            //styleRule5.Style.Font.Name = "Calibri";
            //styleRule5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            //styleRule5.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            //this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            //styleRule1,
            //styleRule2,
            //styleRule3,
            //styleRule4,
            //styleRule5});
            //this.Width = Telerik.Reporting.Drawing.Unit.Inch(6.4583334922790527D);
            ((ISupportInitialize)(this)).EndInit();

        }

    }
}