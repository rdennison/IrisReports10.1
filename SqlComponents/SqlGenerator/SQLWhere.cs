﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace SqlComponents
{
    public class SqlWhere
    {
        public SqlField Field = null;
        public object Value1 = null;
        public object Value2 = null;
        public List<object> InList = null;
        public string Comparator = "";
        public SqlWhereAndOrOptions.SqlWhereAndOr Andor = SqlWhereAndOrOptions.SqlWhereAndOr.And;
        //Mock SQL initialization variables
        //These are used to declare a WHERE object before the generator has been created.
        public string MockTableName = null;
        public string MockFieldName = null;
        public object MockValue1 = null;
        public object MockValue2 = null;
        public string MockComparator = null;
        public bool MockInitialization = false;
        public string Group1 = null;
        public string Group2 = null;

        public List<SqlTable> Tables = new List<SqlTable>();
        public List<SqlField> Fields = new List<SqlField>();

        public SqlWhere(ref List<SqlTable> tables, ref List<SqlField> fields)
        {
            this.Tables = tables;
            this.Fields = fields; 
        }

        public SqlWhere(string group1, string group2, string tableName, string fieldName, object value1, object value2, SqlWhereComparison.SqlComparer comparator, SqlWhereAndOrOptions.SqlWhereAndOr andOr) :
            this(group1, group2, tableName, fieldName, value1, value2, comparator.GetSqlComparer(),andOr) {}

        public SqlWhere(string group1, string group2, string tableName, string fieldName, object value1, object value2, string comparator, SqlWhereAndOrOptions.SqlWhereAndOr andOr)
        {
            this.MockTableName = tableName;
            this.MockFieldName = fieldName;
            this.MockValue1 = value1;
            this.MockValue2 = value2;
            this.MockComparator = comparator;
            this.Andor = andOr;
            this.Group1 = group1;
            this.Group2 = group2;
            MockInitialization = true;
        }

        public List<SqlWhere> InnerWheres = new List<SqlWhere>();

        public SqlWhere AddWhereParameter(string group1, string group2, string tableName, string fieldName, object fieldValue, SqlWhereComparison.SqlComparer comparator)
        {
            return AddWhereParameter(group1, group2, SqlWhereAndOrOptions.SqlWhereAndOr.And, tableName, fieldName, fieldValue, null, comparator);
        }

        public SqlWhere AddWhereParameter(string group1, string group2, SqlWhereAndOrOptions.SqlWhereAndOr andOr, SqlField field, object fieldValue, SqlWhereComparison.SqlComparer comparator)
        {
            return AddWhereParameter(group1, group2, andOr, field, fieldValue, null, comparator);
        }

        public SqlWhere AddWhereParameter(string group1, string group2, SqlWhereAndOrOptions.SqlWhereAndOr andOr, string tableName, string fieldName, object fieldValue, object fieldValue2, SqlWhereComparison.SqlComparer comparator)
        {
            foreach (SqlField field in Fields)
            {
                if (field.Name == fieldName.ToUpper() && field.Table.Name == tableName.ToUpper())
                {
                    return AddWhereParameter(group1, group2, andOr, field, fieldValue, fieldValue2, comparator);
                }
            }

            foreach (SqlTable table in Tables)
            {
                if (table.Name == tableName.ToUpper())
                {
                    SqlField newfield = new SqlField(fieldName.ToUpper(), null, null, table, false, false, null, null, null, null, null);
                    return AddWhereParameter(group1, group2, andOr, newfield, fieldValue, fieldValue2, comparator);
                }
            }

            return null;
        }


        public SqlWhere AddWhereParameter(string group1, string group2, SqlWhereAndOrOptions.SqlWhereAndOr andOr, SqlField field, object fieldValue1, object fieldValue2, SqlWhereComparison.SqlComparer comparator)
        {
            SqlWhere newwhere = new SqlWhere(ref Tables, ref Fields);
            newwhere.Andor = andOr;
            newwhere.Field = field;
            newwhere.Value1 = fieldValue1;
            newwhere.Value2 = fieldValue2;
            newwhere.Comparator = SqlWhereComparison.GetSqlComparer(comparator);
            newwhere.Group1 = group1;
            newwhere.Group2 = group2;
            InnerWheres.Add(newwhere);
            return newwhere;
        }
    }
}
