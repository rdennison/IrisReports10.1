using System.Data;

namespace SqlComponents
{
    public sealed class SqlField
    {
        public string Name;
        public string Alias;
        public object Value;
        public SqlTable Table;
        public bool CriteriaOnly = false;
        public bool Function = false;
        public bool GroupBy = false;
        public int? Precision;
        public int? Size;
        public int? Scale;
        public SqlDbType? SqlType;
        public ParameterDirection? Direction;

        public SqlField(string fieldName, string alias, object fieldValue, SqlTable tableObject, bool function, bool groupBy, int? precision, int? size, int? scale, SqlDbType? sqlType, ParameterDirection? direction)
        {
            Name = fieldName;
            Alias = alias;
            Value = fieldValue;
            Table = tableObject;
            this.Function = function;
            GroupBy = groupBy;
            Precision = precision;
            Size = size;
            Scale = scale;
            SqlType = sqlType;
            Direction = direction;
        }
    }
}
