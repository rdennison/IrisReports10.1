using IrisAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace SqlComponents
{
    public sealed class SqlGenerator  //$TODO - add user service interface
    {       
        private string _sqlTypeString;
        private string _baseTable;
        private string _keyFieldName;
        private string _aggField;
        private bool _distinct = false;
        private bool _audit = false;

        int _currentPage;
        int _pageSize = 0;
        List<string> _groups = new List<string>();

        private List<SqlField> _fields;
        private List<SqlTable> _tables;
        private List<SqlWhere> _wheres;
        private List<SqlOrder> _orders;

        private readonly bool _fullyQualifyFields = true;

        /// <summary>
        /// Limits the return size of a SELECT statement (TOP 10000 default)
        /// </summary>
        private readonly int _selectStatementLimit;
        
        /// <summary>
        /// A list of SQL Parameters to be used in the execution of a SQL Command
        /// </summary>
        public List<SqlParameter> SqlVariables { get; }

        /// <summary>
        /// SQLGenerator constructor.
        /// </summary>
        public SqlGenerator(SqlTypes sqlType, string baseTableName, string keyFieldName = "", SqlTable[] optionalTables = null, bool distinct = false, bool audit = false, bool tenantkey = false, object tenant = null, string field = "")
        {
            _selectStatementLimit = 0;

            _baseTable = baseTableName.ToUpper();
            _aggField = field.ToUpper();
            _sqlTypeString = GetSqlType(sqlType);
            if(keyFieldName != "")
                _keyFieldName = keyFieldName;

            _distinct = distinct;
            _audit = audit;

            _fields = new List<SqlField>();
            _tables = new List<SqlTable>();
            _wheres = new List<SqlWhere>();
            _orders = new List<SqlOrder>();

            SqlVariables = new List<SqlParameter>();

            BuildSqlGen(sqlType, baseTableName, optionalTables);

            if (tenantkey && tenant != null)
            {
                this.AddWhereParameter(new SqlWhere(null, null, _baseTable, "Tenant_Key", tenant, null, SqlWhereComparison.SqlComparer.Equal, SqlWhereAndOrOptions.SqlWhereAndOr.And));
                //_wheres.Add(new SqlWhere(null, null, _baseTable, "County_Key", county, null, SqlWhereComparison.SqlComparer.Equal, SqlWhereAndOrOptions.SqlWhereAndOr.And));
            }

            
        }

        private void BuildSqlGen(SqlTypes sqlType, string baseTableName, SqlTable[] optionalTables = null)
        {
            _sqlTypeString = GetSqlType(sqlType);
            var basetable = new SqlTable {Name = baseTableName.ToUpper()};
            _baseTable = baseTableName.ToUpper();

            if (_baseTable.Contains(".")) basetable.DoNotBracket = true;
            _tables.Add(basetable);
            if (optionalTables != null)
            {
                _tables.AddRange(optionalTables);
            }
        }

        public void SetupPagination(int pageSize, int currentPage, List<string> groups)
        {
            _currentPage = currentPage;
            _pageSize = pageSize;
            _groups = groups;
        }

        #region SQL Generation from Model Methods

        public void SelectFromModel<T>()
        {
            Type t = typeof(T);
           
            if ((AuditAttribute) Attribute.GetCustomAttribute(t, typeof(AuditAttribute)) != null)
            {
                _audit = true;
            }

            foreach (var propertyInfo in t.GetProperties())
            {
                if (propertyInfo.CanRead && propertyInfo.CanWrite)
                {
                    var disablesql = Attribute.GetCustomAttribute(propertyInfo, typeof(DisableSqlReadAttribute)) as DisableSqlReadAttribute;

                    if (disablesql == null)
                    {
                        var orderbyattribute = Attribute.GetCustomAttribute(propertyInfo, typeof(OrderByFieldAttribute)) as OrderByFieldAttribute;
                        AddField(propertyInfo.Name);
                        if (orderbyattribute != null)
                        {
                            AddOrderBy(propertyInfo.Name, _baseTable, orderbyattribute.OrderByAssending ? SqlOrders.Ascending : SqlOrders.Descending, orderbyattribute.Index);
                        }
                    }
                }
            }
        }
        public void SelectFromModel(Type modelType)
        {
            if ((AuditAttribute) Attribute.GetCustomAttribute(modelType, typeof(AuditAttribute)) != null)
            {
                _audit = true;
            }

            foreach (var propertyInfo in modelType.GetProperties())
            {
                if (propertyInfo.CanRead && propertyInfo.CanWrite)
                {
                    var excludesql = Attribute.GetCustomAttribute(propertyInfo, typeof(DisableSqlReadAttribute)) as DisableSqlReadAttribute;

                    if (excludesql == null)
                    {
                        var orderbyattribute = Attribute.GetCustomAttribute(propertyInfo, typeof(OrderByFieldAttribute)) as OrderByFieldAttribute;
                        AddField(propertyInfo.Name);
                        if (orderbyattribute != null)
                        {
                            AddOrderBy(propertyInfo.Name, _baseTable, orderbyattribute.OrderByAssending ? SqlOrders.Ascending : SqlOrders.Descending, orderbyattribute.Index);
                        }
                    }
                }
            }
        }
        public void InsertFromModel<T>(T model)
        {
            Type t = typeof(T);
            
            foreach (var propertyInfo in t.GetProperties())
            {
                if (propertyInfo.CanRead && propertyInfo.CanWrite)
                {
                    var excludesql = Attribute.GetCustomAttribute(propertyInfo, typeof(IsExcludeSqlAttribute)) as IsExcludeSqlAttribute;

                    if (excludesql == null)
                    {
                        var keyattribute = Attribute.GetCustomAttribute(propertyInfo, typeof(KeyAttribute)) as KeyAttribute;
                        var isReadOnly = Attribute.GetCustomAttribute(propertyInfo, typeof(System.ComponentModel.ReadOnlyAttribute)) as System.ComponentModel.ReadOnlyAttribute;
                        var dbDataattribute = Attribute.GetCustomAttribute(propertyInfo, typeof(DbPropertiesAttribute)) as DbPropertiesAttribute;
                        object val = propertyInfo.GetValue(model);
                        if (isReadOnly == null && keyattribute == null)
                        {
                            AddField(propertyInfo.Name, null, _baseTable, val, false, false, dbDataattribute.Precision, dbDataattribute.Size, dbDataattribute.Scale, dbDataattribute.DatabaseType);
                        }
                    }
                }
            }
        }

        public void InsertFromExpando(IDictionary<string, object> model, Type t)
        {
            foreach (var propertyInfo in t.GetProperties())
            {
                if (propertyInfo.CanRead && propertyInfo.CanWrite)
                {
                    var excludesql = Attribute.GetCustomAttribute(propertyInfo, typeof(IsExcludeSqlAttribute)) as IsExcludeSqlAttribute;

                    if (excludesql == null)
                    {
                        var keyattribute = Attribute.GetCustomAttribute(propertyInfo, typeof(KeyAttribute)) as KeyAttribute;
                        var isReadOnly = Attribute.GetCustomAttribute(propertyInfo, typeof(System.ComponentModel.ReadOnlyAttribute)) as System.ComponentModel.ReadOnlyAttribute;
                        var dbDataattribute = Attribute.GetCustomAttribute(propertyInfo, typeof(DbPropertiesAttribute)) as DbPropertiesAttribute;
                        object val = model[propertyInfo.Name];
                        if (isReadOnly == null && keyattribute == null)
                        {
                            AddField(propertyInfo.Name, null, _baseTable, val, false, false, dbDataattribute.Precision, dbDataattribute.Size, dbDataattribute.Scale, dbDataattribute.DatabaseType);
                        }
                    }
                }
            }
        }

        public void UpdateFromExpando(IDictionary<string, object> model, Type t)
        {
            string tableName;

            if (_baseTable == null)
            {
                tableName = t.Name;

                if (tableName.EndsWith("MODEL", StringComparison.OrdinalIgnoreCase))
                {
                    tableName = tableName.Remove(tableName.Length - 5, 5);
                }
            }
            else
            {
                tableName = _baseTable;
            }

            foreach (var propertyInfo in t.GetProperties())
            {
                if (propertyInfo.CanRead && propertyInfo.CanWrite)
                {
                    var excludesql = Attribute.GetCustomAttribute(propertyInfo, typeof(IsExcludeSqlAttribute)) as IsExcludeSqlAttribute;

                    if (excludesql == null)
                    {
                        var readonlyattribute = Attribute.GetCustomAttribute(propertyInfo, typeof(System.ComponentModel.ReadOnlyAttribute)) as System.ComponentModel.ReadOnlyAttribute;
                        var dbDataattribute = Attribute.GetCustomAttribute(propertyInfo, typeof(DbPropertiesAttribute)) as DbPropertiesAttribute;
                        object val = model[propertyInfo.Name];
                        if (readonlyattribute == null)
                        {
                            AddField(propertyInfo.Name, null, _baseTable, val, false, false, dbDataattribute.Precision, dbDataattribute.Size, dbDataattribute.Scale, dbDataattribute.DatabaseType);
                        }
                    }
                }
            }
        }

        public void UpdateFromModel<T>(T model)
        {
            Type t = model.GetType();
            string tableName;
            
            if (_baseTable == null)
            {
                tableName = t.Name;

                if (tableName.EndsWith("MODEL", StringComparison.OrdinalIgnoreCase))
                {
                    tableName = tableName.Remove(tableName.Length - 5, 5);
                }
            }
            else
            {
                tableName = _baseTable;
            }

            foreach (var propertyInfo in t.GetProperties())
            {
                if (propertyInfo.CanRead && propertyInfo.CanWrite)
                {
                    var excludesql = Attribute.GetCustomAttribute(propertyInfo, typeof(IsExcludeSqlAttribute)) as IsExcludeSqlAttribute;

                    if (excludesql == null)
                    {
                        var readonlyattribute = Attribute.GetCustomAttribute(propertyInfo, typeof(System.ComponentModel.ReadOnlyAttribute)) as System.ComponentModel.ReadOnlyAttribute;
                        var dbDataattribute = Attribute.GetCustomAttribute(propertyInfo, typeof(DbPropertiesAttribute)) as DbPropertiesAttribute;
                        object val = propertyInfo.GetValue(model);
                        if (readonlyattribute == null)
                        {
                            AddField(propertyInfo.Name, null, _baseTable, val, false, false, dbDataattribute.Precision, dbDataattribute.Size, dbDataattribute.Scale, dbDataattribute.DatabaseType);
                        }
                    }
                }
            }
        }

        public void DeleteFromModel<T>(T model)
        {
            Type t = typeof(T);
            NoTenantAttribute noTenantAttribute = null;
            foreach (var propertyInfo in t.GetProperties())
            {
                if (propertyInfo.CanRead && propertyInfo.CanWrite)
                {
                    var keyattribute = Attribute.GetCustomAttribute(propertyInfo, typeof(KeyAttribute)) as KeyAttribute;
                    var readonlyattribute = Attribute.GetCustomAttribute(propertyInfo, typeof(System.ComponentModel.ReadOnlyAttribute)) as System.ComponentModel.ReadOnlyAttribute;
                    var dbDataattribute = Attribute.GetCustomAttribute(propertyInfo, typeof(DbPropertiesAttribute)) as DbPropertiesAttribute;
                    if(noTenantAttribute == null)
                        noTenantAttribute = Attribute.GetCustomAttribute(propertyInfo, typeof(NoTenantAttribute)) as NoTenantAttribute;
                    object val = propertyInfo.GetValue(model);
                    if (readonlyattribute == null)
                    {
                        if (keyattribute != null)
                        {
                            AddField(propertyInfo.Name, null, _baseTable, val, false, false, dbDataattribute.Precision, dbDataattribute.Size, dbDataattribute.Scale, dbDataattribute.DatabaseType);
                        }
                        if (propertyInfo.Name.Contains("Tenant_Key") && noTenantAttribute == null)
                        {
                            AddField(propertyInfo.Name, null, _baseTable, val, false, false, dbDataattribute.Precision, dbDataattribute.Size, dbDataattribute.Scale, dbDataattribute.DatabaseType);
                            break;
                        }
                    }
                }
            }
        }

        #endregion

        #region Field Methods

        public void AddSubQueryField(string subQuery, string alias)
        {
            AddField(subQuery, alias, _baseTable, null, true);
        }

        public void AddField(string fieldName)
        {
            if (fieldName.Contains(" ")) AddField(fieldName, _baseTable, null, true);
            else AddField(fieldName, _baseTable, null);
        }

        public void AddField(string fieldName, string tableName)
        {
            AddField(fieldName, tableName, null);
        }

        public void AddField(string[] fields, string tableName)
        {
            foreach (string field in fields)
            {
                AddField(field, tableName, null);
            }
        }

        public void AddField(string fieldName, string tableName, object valueAssigned)
        {
            AddField(fieldName, null, tableName, valueAssigned, false);
        }

        public void AddField(string fieldName, string tableName, bool groupby)
        {
            AddField(fieldName, null, tableName, null, false, groupby);
        }

        public void AddField(string fieldName, string tableName, object valueAssigned, ParameterDirection direction, int? size, SqlDbType sqlType)
        {
            AddField(fieldName, null, tableName, valueAssigned, false, false, null, size, null, sqlType, direction);
        }

        public void AddField(string fieldName, string alias, string tableName, object valueAssigned, bool isFunction = false, bool groupBy = false, int? precision = null, int? size = null, int? scale = null, SqlDbType? sqlType = null, ParameterDirection? direction = null)
        {
            foreach (SqlField field in _fields)
            {
                if (field.Name == fieldName && field.Table.Name == tableName)
                {
                    return;
                }
            }

            foreach (SqlTable table in _tables)
            {
                if (table.Name.ToUpper() == tableName.ToUpper())
                {
                    SqlField newfield = new SqlField(fieldName, alias, valueAssigned, table, isFunction, groupBy, precision, size, scale, sqlType, direction);
                    _fields.Add(newfield);
                }
            }
        }

        #endregion

        #region Table Methods

        /// <summary>
        /// Adds a joined table to the SQL with joining this table to the base table
        /// on a common field name. BaseTable.FieldA => NewTable.FieldA
        /// </summary>
        /// <param name="tableName">The new table name being added to the SQL.</param>
        /// <param name="joinType">Join method (LEFT, RIGHT, INNER).</param>
        /// <param name="joinFieldName">Joining field name in both tables.</param>
        public void AddTable(string tableName, SqlJoins joinType, string joinFieldName, bool doNotBracket = false)
        {
            AddTable(tableName, joinType, joinFieldName, _baseTable, joinFieldName, doNotBracket);
        }

        /// <summary>
        /// Adds a joined table to the SQL with joining this table to the base table
        /// on a common field name. BaseTable.FieldA => NewTable.FieldA
        /// </summary>
        /// <param name="tableName">The new table name being added to the SQL.</param>
        /// <param name="joinType">Join method (LEFT, RIGHT, INNER).</param>
        /// <param name="joinFieldNameA">Joining field name in the new table.</param>
        /// <param name="joiningTableName">Existing table to be joined to.</param>
        /// <param name="joinFieldNameB">Joining field name in the existing table.</param>
        public void AddTable(string tableName, SqlJoins joinType, string joinFieldNameA, string joiningTableName, string joinFieldNameB, bool doNotBracket = false)
        {
            foreach (SqlTable table in _tables)
            {
                if (table.Name == tableName)
                {
                    return;
                }
            }

            SqlTable newtable = new SqlTable();
            newtable.Name = tableName;
            newtable.JoinType = GetSqlJoin(joinType);
            newtable.JoinFieldNameA = joinFieldNameA;
            newtable.JoinFieldNameB = joinFieldNameB;
            newtable.JoiningTable = joiningTableName;
            newtable.DoNotBracket = doNotBracket;
            _tables.Add(newtable);
        }

        #endregion

        #region Where Methods
        //TODO: Not using Subwheres with paranthesis

        //private SqlWhere AddSubWhere(SqlWhereAndOrOptions.SqlWhereAndOr andOr, string tableName, string fieldName, object fieldValue, SqlWhereComparison.SqlComparer comparator)
        //{
        //    return AddSubWhere(andOr, tableName, fieldName, fieldValue, null, comparator);
        //}

        //private SqlWhere AddSubWhere(SqlWhereAndOrOptions.SqlWhereAndOr andOr, string tableName, string fieldName, object fieldValue, object fieldValue2, SqlWhereComparison.SqlComparer comparator)
        //{
        //    foreach (var field in _fields)
        //    {
        //        if (field.Name.Equals(fieldName, StringComparison.OrdinalIgnoreCase) && field.Table.Name.Equals(tableName, StringComparison.OrdinalIgnoreCase))
        //        {
        //            return AddSubWhere(andOr, field, fieldValue, fieldValue2, comparator);
        //        }
        //    }

        //    foreach (var table in _tables)
        //    {
        //        if (table.Name.Equals(tableName, StringComparison.OrdinalIgnoreCase))
        //        {
        //            var newfield = new SqlField(fieldName.ToUpper(), null, null, table, false, false);
        //            return AddSubWhere(andOr, newfield, fieldValue, fieldValue2, comparator);
        //        }
        //    }

        //    return null;
        //}

        //private SqlWhere AddSubWhere(SqlWhereAndOrOptions.SqlWhereAndOr andOr, SqlField field, object fieldValue1, object fieldValue2, SqlWhereComparison.SqlComparer comparator)
        //{
        //    SqlWhere newwhere = new SqlWhere(ref _tables, ref _fields);
        //    newwhere.Andor = andOr;
        //    newwhere.Field = field;
        //    newwhere.Value1 = fieldValue1;
        //    newwhere.Value2 = fieldValue2;
        //    newwhere.Comparator = SqlWhereComparison.GetSqlComparer(comparator);
        //    if (newwhere.Comparator == "<>" && fieldValue1 == null)
        //        newwhere.Comparator = "IS NOT";
        //    _wheres[_wheres.Count - 1].InnerWheres.Add(newwhere);
        //    return newwhere;
        //}

        public void AddWhereParameter(SqlWhere whereObject)
        {
            if (whereObject.MockInitialization)
            {
                AddWhereParameter(whereObject.Group1, whereObject.Andor, whereObject.MockTableName, whereObject.MockFieldName, 
                    whereObject.MockValue1, whereObject.MockValue2, SqlWhereComparison.ParseComparer(whereObject.MockComparator), whereObject.Group2);
            }
        }

        public SqlWhere AddWhereParameter(string group1, string tableName, string fieldName, object fieldValue, SqlWhereComparison.SqlComparer comparator, string group2)
        {
            return AddWhereParameter(group1, SqlWhereAndOrOptions.SqlWhereAndOr.And, tableName, fieldName, fieldValue, null, comparator, group2);
        }

        private SqlWhere AddWhereParameter(string group1, SqlWhereAndOrOptions.SqlWhereAndOr andOr, string tableName, string fieldName, object fieldValue, object fieldValue2, SqlWhereComparison.SqlComparer comparator, string group2)
        {
            foreach (SqlField field in _fields)
            {
                if (field.Name.Equals(fieldName, StringComparison.OrdinalIgnoreCase) && field.Table.Name.Equals(tableName, StringComparison.OrdinalIgnoreCase))
                {
                    return AddWhereParameter(group1, group2, andOr, field, fieldValue, fieldValue2, comparator);
                }
            }

            foreach (SqlTable table in _tables)
            {
                if (table.Name.Equals(tableName, StringComparison.OrdinalIgnoreCase))
                {
                    var newfield = new SqlField(fieldName.ToUpper(), null, null, table, false, false, null, null, null, null, null);
                    return AddWhereParameter(group1, group2, andOr, newfield, fieldValue, fieldValue2, comparator);
                }
            }

            return null;
        }

        private SqlWhere AddWhereParameter(string group1, string group2, SqlWhereAndOrOptions.SqlWhereAndOr andOr, SqlField field, object fieldValue1, object fieldValue2, SqlWhereComparison.SqlComparer comparator)
        {
            SqlWhere newwhere = new SqlWhere(ref _tables, ref _fields);
            newwhere.Andor = andOr;
            newwhere.Field = field;
            newwhere.Value1 = fieldValue1;
            newwhere.Value2 = fieldValue2;
            newwhere.Group1 = group1;
            newwhere.Group2 = group2;
            newwhere.Comparator = SqlWhereComparison.GetSqlComparer(comparator);

            if (newwhere.Comparator == "<>" && fieldValue1 == null)
                newwhere.Comparator = "IS NOT";

            _wheres.Add(newwhere);
            return newwhere;
        }


        private SqlField FindOrGenerateField(string fieldName, string tableName)
        {
            foreach (SqlField field in _fields)
            {
                if (field.Name.ToUpper() == fieldName.ToUpper() && field.Table.Name.ToUpper() == tableName.ToUpper())
                {
                    return field;
                }
            }

            foreach (SqlTable table in _tables)
            {
                if (table.Name.ToUpper() == tableName.ToUpper())
                {
                    SqlField newfield = new SqlField(fieldName.ToUpper(), null, null, table, false, false, null, null, null, null, null);
                    return newfield;
                }
            }

            return null;
        }

        #endregion

        #region Order Methods

        /// <summary>
        /// Adds an ORDER BY statement by for the specified field
        /// defaulted to ascending order and for the Base Table.
        /// </summary>
        /// <param name="fieldName">The Base Table field to be used.</param>
        public SqlOrder AddOrderBy(string fieldName)
        {
            return AddOrderBy(fieldName, _baseTable, SqlOrders.Ascending);
        }

        /// <summary>
        /// Adds an ORDER BY statement by for the specified field
        /// defaulted to ascending order.
        /// </summary>
        /// <param name="fieldName">The table field to be used in the SQL.</param>
        /// <param name="tableName">The new table name being added to the SQL.</param>
        /// <param name="direction"></param>
        /// <param name="index"></param>
        private SqlOrder AddOrderBy(string fieldName, string tableName, SqlOrders direction, int index = -1)
        {
            try
            {
                if (tableName.ToUpper() == _baseTable.ToUpper() && _fields.Count == 0)
                {
                    SqlField field = new SqlField(fieldName, null, null, _tables[0], false, false, null, null, null, null, null);
                    SqlOrder order = new SqlOrder(field, GetSqlOrder(direction));
                    if (index >= 0) _orders.Insert(index, order);
                    else _orders.Add(order);

                    return order;
                }

                foreach (SqlOrder order in _orders)
                {
                    if (order.Field.Name == fieldName && order.Field.Table.Name == tableName)
                    {
                        return order;
                    }
                }

                foreach (SqlField field in _fields)
                {
                    if (field.Name.ToUpper() == fieldName.ToUpper() && field.Table.Name.ToUpper() == tableName.ToUpper())
                    {
                        SqlOrder order = new SqlOrder(field, GetSqlOrder(direction));
                        if (index >= 0) _orders.Insert(index, order);
                        else _orders.Add(order);

                        return order;
                    }
                }

                return null;
            }
            catch
            { return null; }
        }

        #endregion

        #region SQL Generation Methods

        public string SqlString
        {
            get
            {
                SqlVariables.Clear(); //Somehow this is getting filled before here so we clear it to make sure we do not have duplicate variables
                string sql = "";

                if (_sqlTypeString == "SELECT")
                {
                    sql += _sqlTypeString.Trim();

                    if (_distinct)
                        sql += " DISTINCT";
                    if (_selectStatementLimit >= 1)
                        sql += " TOP " + _selectStatementLimit + " ";
                    else
                        sql += " ";
                }
                else if (_sqlTypeString == "UPDATE")
                {
                    sql += _baseTable + "_UPDATE ";
                }
                else if (_sqlTypeString == "INSERT")
                {
                    sql += _baseTable + "_INSERT ";
                }
                else if (_sqlTypeString == "DELETE")
                {
                    sql +=  _baseTable + "_DELETE ";
                }

                //SqlVariables.Clear();

                switch (_sqlTypeString)
                {
                    case "COUNT":
                        sql += BuildSqlCount();
                        sql += BuildWhereStatement(_wheres);
                        break;
                    case "SUM":
                        sql += BuildSqlSum();
                        sql += BuildWhereStatement(_wheres);
                        break;
                    case "MIN":
                        sql += BuildSqlMin();
                        sql += BuildWhereStatement(_wheres);
                        break;
                    case "MAX":
                        sql += BuildSqlMax();
                        sql += BuildWhereStatement(_wheres);
                        break;
                    case "AVG":
                        sql += BuildSqlAvg();
                        sql += BuildWhereStatement(_wheres);
                        break;
                    case "SELECT":
                        sql += BuildFieldsSelect();
                        sql += BuildJoinStatement();
                        sql += BuildWhereStatement(_wheres);
                        sql += BuildGroupByStatement();
                        sql += BuildOrderStatement();
                        break;
                    case "UPDATE":
                        BuildUpdateSproc();
                        break;
                    case "INSERT":
                        BuildInsertSproc();
                        break;
                    case "DELETE":
                        BuildDeleteSproc();
                        break;
                }

                if (_pageSize > 0)
                    sql = WrapPaginationSQL(sql);

                return sql;
            }
        }

        public string SqlDebug
        {
            get
            {
                string sql = SqlString;
                foreach (SqlParameter var in SqlVariables)
                {
                    sql = sql.Replace(var.ParameterName, var.Value != null ? WrapSqlValues(var.Value) : "null");
                }
                return sql;
            }
        }

        private string BuildFieldsSelect()
        {
            string fieldStructure = "";

            if (_fields.Count > 0)
            {
                foreach (SqlField field in _fields)
                {
                    if (fieldStructure.Length > 0)
                    {
                        fieldStructure += ", ";
                    }

                    if (field.Function || !_fullyQualifyFields)
                        fieldStructure += field.Name;
                    else if (field.Table.DoNotBracket)
                        fieldStructure += field.Table.Name + "." + field.Name;
                    else
                    {
                       fieldStructure += "[" + field.Table.Name + "].";
                        fieldStructure += (field.Name != "*") ? "[" + field.Name + "]" : "*";
                    }

                    if (field.Alias != null) fieldStructure += " AS " + field.Alias;
                }
            }
            else
            {
                fieldStructure = "[" + _baseTable + "].*";
            }

            return fieldStructure;
        }

        #region Aggregate SQL Funtions
        private string WrapPaginationSQL(string sql)
        {
            Match regMatch = new Regex("ORDER BY ([,#a-zA-Z0-9\\._\\[\\]]+)(\\s[#,a-zA-Z0-9\\._\\[\\]]+)*\\s?(DESC)?(ASC)?", RegexOptions.IgnoreCase).Match(sql);
            int minSeqNumber = _currentPage * _pageSize - _pageSize + 1;
            int maxSeqNumber = _currentPage * _pageSize;
            string order = _baseTable + "_KEY";
            if (_keyFieldName != "")
                order = _keyFieldName;

            System.Text.StringBuilder Sb = new System.Text.StringBuilder(sql);
            Sb.Remove(regMatch.Index, regMatch.Length);
            string newSql = null;
            if(_groups == null || _groups.Count == 0)
            {
                newSql = "SELECT * FROM (SELECT A.*, ROW_NUMBER()  OVER ( ORDER BY " + order + " DESC )  AS seqNumber FROM (" + Sb.ToString() + ") AS A)";
                newSql += " AS temp_result WHERE seqNumber BETWEEN " + minSeqNumber + " AND " + maxSeqNumber;
            }
            else
            {
                if(_groups.Count == 1)
                {
                    order = _groups[0] + " ASC";
                }
                else
                {
                    order = "";
                    foreach (var g in _groups)
                    {
                        order += g + ", ";
                    }
                    order = order.Substring(0, order.Length - 2);
                    order += " ASC";
                }
                
                newSql = "SELECT * FROM (SELECT A.*, ROW_NUMBER()  OVER ( ORDER BY " + order + " )  AS seqNumber FROM (" + Sb.ToString() + ") AS A)";
                newSql += " AS temp_result WHERE seqNumber BETWEEN " + minSeqNumber + " AND " + maxSeqNumber;
            }

            return newSql;
        }

        public string BuildSqlCount()
        {
            if(_keyFieldName == "")
                _keyFieldName = _baseTable + "_Key";
            if (_aggField != "")
                _keyFieldName = _aggField;

            string sql = "SELECT COUNT([" + _keyFieldName + "]) FROM [" + _baseTable + "]";
            return sql;
        }

        public string BuildSqlSum()
        {
            string field = _baseTable + "_Key";
            if (_aggField != "")
                field = _aggField;

            string sql = "SELECT SUM([" + field + "]) FROM [" + _baseTable + "]";
            return sql;
        }

        public string BuildSqlMin()
        {
            string field = _baseTable + "_Key";
            if (_aggField != "")
                field = _aggField;

            string sql = "SELECT MIN([" + field + "]) FROM [" + _baseTable + "]";
            return sql;
        }

        public string BuildSqlMax()
        {
            string field = _baseTable + "_Key";
            if (_aggField != "")
                field = _aggField;

            string sql = "SELECT MAX([" + field + "]) FROM [" + _baseTable + "]";
            return sql;
        }

        public string BuildSqlAvg()
        {
            string field = _baseTable + "_Key";
            if (_aggField != "")
                field = _aggField;

            string sql = "SELECT AVG([" + field + "]) FROM [" + _baseTable + "]";
            return sql;
        }
        #endregion

        private string BuildUpdateSproc()
        {
            var fieldStructure = "";
            AddField("ReturnMsg", _baseTable, "@ReturnMsg", ParameterDirection.Output, 4000, SqlDbType.VarChar);
            //AddField("RecordsAffected", _baseTable, "@RecordsAffected", ParameterDirection.Output, null, SqlDbType.Int);
            //AddField("return_value", _baseTable, "@return_value", ParameterDirection.ReturnValue, null, SqlDbType.BigInt);
            //var ReturnMsg = new SqlParameter
            //{
            //    ParameterName = "@ReturnMsg",
            //    SqlDbType = SqlDbType.VarChar,
            //    Size = 4000,
            //    Direction = ParameterDirection.Output,
            //    Value = "@ReturnMsg"
            //};
            //var RecordsAffected = new SqlParameter
            //{
            //    ParameterName = "@RecordsAffected",
            //    SqlDbType = SqlDbType.Int,
            //    Direction = ParameterDirection.Output
            //};
            //var ReturnValue = new SqlParameter
            //{
            //    ParameterName = "@return_value",
            //    SqlDbType = SqlDbType.BigInt,
            //    Direction = ParameterDirection.ReturnValue
            //};
            //SqlVariables.Add(ReturnMsg);
            //SqlVariables.Add(RecordsAffected);
            //SqlVariables.Add(ReturnValue);

            foreach (SqlField field in _fields)
            {
                //if (fieldStructure.Length > 0)
                //{
                //    fieldStructure += ", ";
                //}
                //if (field.Value == null)
                //    fieldStructure += "@" + field.Name + " = null"; //"@SPROC" + variableName;
                //else if (field.Value.GetType() == typeof(DateTime))
                //    fieldStructure += "@" + field.Name + " = '" + field.Value + "'";
                //else if (field.Value.GetType() == typeof(string))
                //    fieldStructure += "@" + field.Name + " = '" + field.Value + "'";
                //else
                //    fieldStructure += "@" + field.Name + " = " + field.Value; //"@SPROC" + variableName;

                SqlVariables.Add(BuildSqlParameter(field));
            }
            //fieldStructure += " SELECT	@ReturnMsg as N'@ReturnMsg', @RecordsAffected as N'@RecordsAffected' SELECT  'Return Value' = @return_value";

            return fieldStructure;
        }

        private string BuildInsertSproc()
        {
            var fieldStructure = "";
            //AddField("ReturnMsg", _baseTable, "@ReturnMsg", ParameterDirection.Output, 4000, SqlDbType.VarChar);
            //AddField("New_Key", _baseTable, "@New_Key", ParameterDirection.Output, null, SqlDbType.BigInt);
            //AddField("return_value", _baseTable, "@return_value", ParameterDirection.ReturnValue, null, SqlDbType.BigInt);
            //var ReturnMsg = new SqlParameter
            //{
            //    ParameterName = "@ReturnMsg",
            //    SqlDbType = SqlDbType.VarChar,
            //    Size = 4000,
            //    Direction = ParameterDirection.Output,
            //    Value = "@ReturnMsg"
            //};
            //var NewKey = new SqlParameter
            //{
            //    ParameterName = "@New_Key",
            //    SqlDbType = SqlDbType.BigInt,
            //    Direction = ParameterDirection.Output
            //};
            //var ReturnValue = new SqlParameter
            //{
            //    ParameterName = "@return_value",
            //    SqlDbType = SqlDbType.BigInt,
            //    Direction = ParameterDirection.ReturnValue
            //};
            //SqlVariables.Add(ReturnMsg);
            //SqlVariables.Add(NewKey);
            //SqlVariables.Add(ReturnValue);

            foreach (SqlField field in _fields)
            {
                //if (fieldStructure.Length > 0)
                //{
                //    fieldStructure += ", ";
                //}
                //if (field.Value == null)
                //    fieldStructure += "@" + field.Name + " = null";
                //else if (field.Value != null && field.Value.GetType() == typeof(DateTime))
                //    fieldStructure += "@" + field.Name + " = '" + field.Value + "'";
                //else
                //    fieldStructure += "@" + field.Name + " = " + field.Value;

                //var newvar = new SqlParameter
                //{
                //    ParameterName = "@" + field.Name,
                //    Value = field.Value == null ? "NULL" : field.Value
                //};
                SqlVariables.Add(BuildSqlParameter(field));
            }

            return fieldStructure;
        }

        private string BuildDeleteSproc()
        {
            var fieldStructure = "";
            //AddField("ReturnMsg", _baseTable, "@ReturnMsg", ParameterDirection.Output, 4000, SqlDbType.VarChar);
            //AddField("RecordsAffected", _baseTable, "@RecordsAffected", ParameterDirection.Output, null, SqlDbType.Int);
            //AddField("return_value", _baseTable, "@return_value", ParameterDirection.ReturnValue, null, SqlDbType.BigInt);
            //var ReturnMsg = new SqlParameter
            //{
            //    ParameterName = "@ReturnMsg",
            //    SqlDbType = SqlDbType.VarChar,
            //    Size = 4000,
            //    Direction = ParameterDirection.Output,
            //    Value = "@ReturnMsg"
            //};
            //var RecordsAffected = new SqlParameter
            //{
            //    ParameterName = "@RecordsAffected",
            //    SqlDbType = SqlDbType.Int,
            //    Direction = ParameterDirection.Output
            //};
            //var ReturnValue = new SqlParameter
            //{
            //    ParameterName = "@return_value",
            //    SqlDbType = SqlDbType.BigInt,
            //    Direction = ParameterDirection.ReturnValue
            //};
            //SqlVariables.Add(ReturnMsg);
            //SqlVariables.Add(RecordsAffected);
            //SqlVariables.Add(ReturnValue);

            foreach (SqlField field in _fields)
            {
                if (fieldStructure.Length > 0)
                {
                    fieldStructure += ", ";
                }
                if (field.Value == null)
                    fieldStructure += "@" + field.Name + " = null"; //"@SPROC" + variableName;
                //else if (field.Value != null && field.Value.GetType() == typeof(DateTime))
                //    fieldStructure += "@" + field.Name + " = '" + field.Value + "'";
                else
                    fieldStructure += "@" + field.Name + " = " + field.Value; //"@SPROC" + variableName;

                //var newvar = new SqlParameter
                //{
                //    ParameterName = "@" + field.Name,
                //    Value = field.Value == null ? "NULL" : field.Value
                //};
                SqlVariables.Add(BuildSqlParameter(field));
            }

            return fieldStructure;
        }
        
        private string BuildJoinStatement()
        {
            string joinStructure = "";

            if (_baseTable.Contains("."))
                joinStructure += " FROM " + _baseTable;
            else
                joinStructure += " FROM [" + _baseTable + "]";

            foreach (SqlTable table in _tables)
            {
                if (table.JoinType.Length > 0)
                {
                    if (table.DoNotBracket)
                        joinStructure += " " + table.JoinType + " JOIN " + table.Name + " ON " + table.Name + ".[" + table.JoinFieldNameA + "] " + table.JoinComparator + " " + table.JoiningTable + ".[" + table.JoinFieldNameB + "]";
                    else
                        joinStructure += " " + table.JoinType + " JOIN [" + table.Name + "] ON [" + table.Name + "].[" + table.JoinFieldNameA + "] " + table.JoinComparator + " [" + table.JoiningTable + "].[" + table.JoinFieldNameB + "]";
                }
            }

            return joinStructure;
        }

        private string BuildOrderStatement()
        {
            string ordersString = "";

            foreach (SqlOrder order in _orders)
            {
                if (ordersString.Length > 0) ordersString += ", ";

                ordersString += "[" + order.Field.Table.Name + "].[" + order.Field.Name + "] " + order.Direction;
            }

            if (ordersString.Length > 0)
            {
                return " ORDER BY " + ordersString + " ";
            }
            else
            {
                return "";
            }
        }

        private string BuildGroupByStatement()
        {
            string groupByStatment = "";

            foreach (SqlField field in _fields)
            {
                if (field.GroupBy)
                {
                    if (groupByStatment.Length > 0) groupByStatment += ", ";
                    groupByStatment += "[" + field.Table.Name + "].[" + field.Name + "]";
                }
            }

            if (groupByStatment.Length > 0) return " GROUP BY " + groupByStatment; else return "";
        }

        private string BuildWhereStatement(List<SqlWhere> whereList, bool isSubWhere = false)
        {
            string subWhereAndOr = "";

            if (whereList.Count > 0)
            {
                string whereStructure = "";

                foreach (SqlWhere where in whereList)
                {
                    
                    if (whereStructure.Length > 0)
                    {
                        whereStructure += " " + where.Andor + " ";
                    }
                    if (where.Group1 != null)
                        whereStructure += " "+ where.Group1;
                    else
                    {
                        subWhereAndOr = where.Andor.GetSqlWhereAndOr();
                    }

                    bool isBool = Boolean.TryParse(where.Value1.ToString(), out isBool);

                    if (isBool)
                        where.Value1 = (Convert.ToBoolean(where.Value1) == true) ? 1 : 0;

                    if (where.Field.Function || !_fullyQualifyFields)
                        whereStructure += where.Field.Name + " ";
                    else if (where.Field.Table.DoNotBracket)
                        whereStructure += where.Field.Table.Name + ".[" + where.Field.Name + "] ";
                    else
                        whereStructure += "[" + where.Field.Table.Name + "].[" + where.Field.Name + "] ";

                    if (where.Comparator == "CONTAINS" || where.Comparator == "STARTS WITH" || where.Comparator == "ENDS WITH")
                        whereStructure += "LIKE";
                    else
                        whereStructure += where.Comparator;

                    SqlParameter newvar = null;

                    if (where.Value1 != null && where.Value1.ToString().Contains("("))
                    {
                        switch (where.Comparator)
                        {
                            case "LIKE":
                                whereStructure += " " + where.Value1;
                                break;
                            case "BETWEEN":
                                whereStructure += " " + where.Value1 + " AND " + where.Value2;
                                break;
                            case "IS NOT":
                                whereStructure += " NULL";
                                break;
                            default:
                                whereStructure += " " + where.Value1;
                                break;
                        }
                    }
                    else
                    {
                        string whereGuid = Guid.NewGuid().ToString("N");
                        newvar = new SqlParameter();

                        switch (where.Comparator)
                        {
                            case "BETWEEN":
                                SqlParameter newvarbeween = new SqlParameter();
                                string whereGuidbetween = Guid.NewGuid().ToString("N");
                                whereStructure += " @" + whereGuid + " AND @" + whereGuidbetween;
                                newvar.ParameterName = "@" + whereGuid;
                                newvar.Value = where.Value1;
                                newvarbeween.ParameterName = "@" + whereGuidbetween;
                                newvarbeween.Value = where.Value2;
                                SqlVariables.Add(newvarbeween);
                                break;
                            case "IS NOT":
                                whereStructure += " NULL";
                                newvar = null;
                                break;
                            case "IN":
                                string inVars = "";
                                whereStructure += " (";
                                foreach (object inItem in where.InList)
                                {
                                    string newinwhereGuid = Guid.NewGuid().ToString("N");
                                    SqlParameter newinvar = new SqlParameter();

                                    if (inVars.Length > 0) inVars += ", ";

                                    newinvar.ParameterName = "@" + newinwhereGuid;
                                    newinvar.Value = inItem;

                                    SqlVariables.Add(newinvar);

                                    inVars += "@" + newinwhereGuid;
                                }
                                whereStructure += inVars + ")";
                                newvar = null;
                                break;
                            case "LIKE":
                                whereStructure += " '%' + @" + whereGuid + " + '%' ";
                                newvar.ParameterName = "@" + whereGuid;
                                newvar.Value = where.Value1;
                                break;
                       
                            case "STARTS WITH":
                                whereStructure +=  " @" + whereGuid + " + '%' ";
                                newvar.ParameterName = "@" + whereGuid;
                                newvar.Value = where.Value1;
                                break;

                            case "ENDS WITH":
                                whereStructure += " + '%' + @" + whereGuid;
                                newvar.ParameterName = "@" + whereGuid;
                                newvar.Value = where.Value1;
                                break;
                            case "CONTAINS":
                                //Ask Casper how to do this the right way
                                whereStructure += " '%' + @" + whereGuid + " + '%' ";
                                //whereStructure += " '%' + '" + where.Value1 + "' + '%' ";
                                newvar.ParameterName = "@" + whereGuid;
                                newvar.Value =  where.Value1;
                                break;
                            default:
                                whereStructure += " @" + whereGuid;
                                newvar.ParameterName = "@" + whereGuid;
                                newvar.Value = where.Value1;
                                break;
                        }
                    }

                    if (newvar != null)
                        SqlVariables.Add(newvar);

                    if (where.InnerWheres.Count > 0)
                    {
                        whereStructure += BuildWhereStatement(where.InnerWheres, true);
                    }

                    if (where.Group2 != null)
                        whereStructure += where.Group2;
                }
                

                if (!isSubWhere)
                    whereStructure = " WHERE " + whereStructure;
                else
                    whereStructure = " " + subWhereAndOr + " (" + whereStructure + ")";

                return whereStructure;
            }
            else
            {
                return "";
            }
        }

        private SqlParameter BuildSqlParameter(SqlField field)
        {
            SqlParameter sqlParam = new SqlParameter();

            sqlParam.ParameterName = "@" + field.Name;
            sqlParam.Value = field.Value == null ? DBNull.Value : field.Value;

            if(field.Precision != null)
                sqlParam.Precision = (byte)(field.Precision ?? default(int));
            if (field.Scale != null)
                sqlParam.Scale = (byte) (field.Scale ?? default(int));
            if (field.Size != null)
                sqlParam.Size = field.Size ?? default(int);
            if (field.SqlType != null)
                sqlParam.SqlDbType = field.SqlType ?? default(SqlDbType);
            if (field.Direction != null)
                sqlParam.Direction = field.Direction ?? default(ParameterDirection);

            return sqlParam;
        }

        private static string WrapSqlValues(object strValue)
        {
            if (IsNumeric(strValue))
            {
                return strValue.ToString().Length > 0 ? strValue.ToString() : "0";
            }
            else
            {
                return "'" + strValue.ToString().Replace("'", "''") + "'";
            }
        }

        private static bool IsNumeric(object value)
        {
            return value is sbyte
            || value is byte
            || value is short
            || value is ushort
            || value is int
            || value is uint
            || value is long
            || value is ulong
            || value is float
            || value is double
            || value is decimal;
        }

        #endregion

        #region SQL Types Declarations

        public enum SqlTypes
        {
            Select = 1,
            SprocUpdate = 2,
            SprocInsert = 3,
            SprocDelete = 4,
            Sproc = 5,
            Count = 6,
            Sum = 7,
            Min = 8,
            Max = 9,
            Avg = 10,
        }

        readonly static Dictionary<SqlTypes, string> _sqlTypeLookup = new Dictionary<SqlTypes, string>()
        {
            { SqlTypes.Select, "SELECT" },
            { SqlTypes.Sproc, "SPROC" },
            { SqlTypes.Count, "COUNT" },
            { SqlTypes.Sum, "SUM" },
            { SqlTypes.Min, "MIN" },
            { SqlTypes.Max, "MAX" },
            { SqlTypes.Avg, "AVG" },
            { SqlTypes.SprocUpdate, "UPDATE" },
            { SqlTypes.SprocInsert, "INSERT" },
            { SqlTypes.SprocDelete, "DELETE" }
        };

        private static string GetSqlType(SqlTypes c)
        {
            string sql = null;
            if (_sqlTypeLookup.TryGetValue(c, out sql) == false)
                return string.Empty;

            return sql;
        }

        #endregion

        #region SQL Joins Declarations

        public enum SqlJoins
        {
            Inner = 1,
            Outter = 2,
            Left = 3,
            Right = 4
        }

        readonly static Dictionary<SqlJoins, string> sqlJoinsLookup = new Dictionary<SqlJoins, string>()
        {
            { SqlJoins.Inner, "INNER" },
            { SqlJoins.Outter, "OUTTER" },
            { SqlJoins.Left, "LEFT" },
            { SqlJoins.Right, "RIGHT" },
        };

        private string GetSqlJoin(SqlJoins c)
        {
            string sql = null;
            if (sqlJoinsLookup.TryGetValue(c, out sql) == false)
                return string.Empty;

            return sql;
        }

        #endregion

        #region SQL Order Declarations

        private enum SqlOrders
        {
            Ascending = 1,
            Descending = 2
        }

        readonly static Dictionary<SqlOrders, string> _sqlOrderLookup = new Dictionary<SqlOrders, string>()
            {
                { SqlOrders.Ascending, "ASC" },
                { SqlOrders.Descending, "DESC" }
            };

        private string GetSqlOrder(SqlOrders c)
        {
            string sql;
            if (_sqlOrderLookup.TryGetValue(c, out sql) == false)
                return string.Empty;

            return sql;
        }

        public sealed class SqlOrder
        {
            public SqlField Field { get; }
            public string Direction { get; }
            public int Index { get; }

            public SqlOrder(SqlField field, string direction = "Asc", int index = -1)
            {
                this.Field = field;
                this.Direction = direction;
                this.Index = index;
            }
        }

        #endregion

        #region SQL GroupBy Declarations




        #endregion

    }
}
