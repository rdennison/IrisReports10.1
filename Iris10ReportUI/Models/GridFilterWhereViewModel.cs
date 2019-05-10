using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Iris10ReportUI.Models
{
    public class GridFilterWhereViewModel
    {
        public string FilterDetail_Key { get; set; }
        public string FilterType { get; set; }
        public string TableName { get; set; }
        public string FieldName { get; set; }
        public string Group1 { get; set; }
        public string Description { get; set; }
        public string Operator1 { get; set; }
        public string Value1 { get; set; }
        public string Value2 { get; set; }
        public string Group2 { get; set; }
        public string Operator2 { get; set; }
        public string DataType { get; set; }
        public int KeyField { get; set; }
        public bool UseDropdown { get; set; }
        public bool UseNonUniqueList { get; set; }
        public bool UseLikeOperator { get; set; }
        public string SprocName { get; set; }
        public int RecordOrder { get; set; }
        public string EnglishWhere { get; set; }
        public string ForeignKey { get; set; }
        public ListViewModel FieldList { get; set; }
        public ListViewModel operatordateListoptions { get; set; }
        public ListViewModel operatordropdownListoptions { get; set; }
        public ListViewModel operatortextListoptions { get; set; }
        public ListViewModel operatorTrueFalseoptions {get;set;}

        public DropdownViewModel DropdownValues { get; set; }
        public DropdownViewModel DropdownValues2 { get; set; }
        public DropdownViewModel DropdownValues3 { get; set; }
        public int? NumberBox { get; set; }
        public int? NumberBox2 { get; set; }
        public string TextBox { get; set; }
        public string TextBox2 { get; set; }
        public DateTime? DateValues { get; set; }
        public DateTime? DateValues2 { get; set; }
        public int Position { get; set; }














    }
}