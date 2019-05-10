using System;
using System.Collections.Generic;
using System.Linq;

namespace SqlComponents
{
    public static class SqlWhereComparison
    {
        readonly static Dictionary<SqlComparer, string> _comparerLookup = new Dictionary<SqlComparer, string>()
        {
            { SqlComparer.Equal, "=" },
            { SqlComparer.NotEqual, "<>" },
            { SqlComparer.GreaterThan, ">" },
            { SqlComparer.LessThan, "<" },
            { SqlComparer.GreaterThan | SqlComparer.Equal, ">=" },
            { SqlComparer.LessThan | SqlComparer.Equal, "<=" },
            { SqlComparer.BitwiseAnd, "&" },
            { SqlComparer.BitwiseOr, "|" },
            { SqlComparer.BitwiseExclusiveOr, "^" },
            { SqlComparer.BitwiseAnd | SqlComparer.Equal, "&=" },
            { SqlComparer.BitwiseOr | SqlComparer.Equal, "|=" },
            { SqlComparer.BitwiseExclusiveOr | SqlComparer.Equal, "^=" },
            { SqlComparer.Like, "LIKE" },
            { SqlComparer.NotLike, "NOT LIKE" },
            { SqlComparer.Between, "BETWEEN"},
            { SqlComparer.Outside, "OUTSIDE"},
            { SqlComparer.In, "IN"},
            { SqlComparer.Contains, "CONTAINS" },
            { SqlComparer.StartsWith, "STARTS WITH" },
            { SqlComparer.EndsWith, "ENDS WITH" }
        };

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
            In = 200,
            Contains = 63,
            StartsWith = 53,
            EndsWith = 43
        }

        public static string GetSqlComparer(this SqlComparer c)
        {
            string sql = null;
            if (_comparerLookup.TryGetValue(c, out sql) == false)
                return string.Empty;

            return sql;
        }

        // Do a simple reverse lookup into the dictionary
        public static SqlComparer ParseComparer(string s)
        {
            // Default is 0 = SqlComparer.None
            var comparer = _comparerLookup.FirstOrDefault(c => c.Value == s).Key;
            return comparer;
        }
    }
}
