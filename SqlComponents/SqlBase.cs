using IrisAttributes;
using System.Collections.Generic;

namespace SqlComponents
{
  
    public class SqlBase
    {
        public enum SqlComparer
        {
            None = 0,
            Equal = 1,
            Like = 65,
            GreaterThan = 2,
            LessThan = 4,
            NotEqual = 6,
            NotLike = 70,
            BitwiseAnd = 8,
            BitwiseOr = 16,
            BitwiseExclusiveOr = 32,
            Partial = 64,
            Between = 100,
            Outside = 150,
            In = 200
        }
        [IrisGridColumn(Hidden = true, IncludeInMenu = false)]
        [IsExcludeSql]
        [DisableSqlRead]
        public List<SqlWhere> Wheres { get; set; }


        public SqlWhere AddWhere (string group1, string modelname, string fieldname, SqlWhereComparison.SqlComparer comparer, object value1, object value2, SqlWhereAndOrOptions.SqlWhereAndOr andOr, string group2)
        {
            SqlWhere newwhere = new SqlWhere(group1, group2, modelname, fieldname, value1, value2, comparer.GetSqlComparer(), andOr);
      

            return newwhere;
        }

    }
}
