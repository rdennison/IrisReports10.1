using System;
using System.Linq;

namespace SqlComponents
{
    public sealed class SqlTable
    {
        public string Name = "";
        public string JoinType = "";
        public string JoinFieldNameA = "";
        public string JoinFieldNameB = "";
        public string JoiningTable = "";
        public string JoinComparator = "=";
        public bool DoNotBracket = false;
    }
}
