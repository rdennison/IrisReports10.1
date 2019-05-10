using SqlComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoreDomain.ViewModels
{
    public class SqlWhereViewModel
    {
        public SqlField Field { get; set; }
        public object Value1 { get; set; }
        public object Value2 { get; set; }
        public List<object> InList { get; set; }
        public string Comparator { get; set; }
        public SqlWhereAndOrOptions.SqlWhereAndOr AndOr { get; set; }
        public string MockTableName { get; set; }
        public string MockFieldName { get; set; }
        public object MockValue1 { get; set; }
        public object MockValue2 { get; set; }
        public string MockComparator { get; set; }
        public bool MockInitialization { get; set; }
        public string Group1 { get; set; }
        public string Group2 { get; set; }
    }
}