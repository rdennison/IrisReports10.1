using System;
using System.Collections.Generic;
using System.Linq;

namespace SqlComponents
{
    public static class SqlWhereAndOrOptions
    {
        readonly static Dictionary<SqlWhereAndOr, string> _andOrLookup = new Dictionary<SqlWhereAndOr, string>()
        {
            { SqlWhereAndOr.And, "AND" },
            { SqlWhereAndOr.Or, "OR" },
        };

        public enum SqlWhereAndOr
        {
            And = 0,
            Or = 1,
        }

        public static string GetSqlWhereAndOr(this SqlWhereAndOr c)
        {
            string sql;
            if (_andOrLookup.TryGetValue(c, out sql) == false)
                return string.Empty;

            return sql;
        }
    }
}
