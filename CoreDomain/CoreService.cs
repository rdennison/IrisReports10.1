using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Data;
using SqlComponents;
using IrisAttributes;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text;
using CoreDomain.ViewModels;
using Iris10ReportUI.GridBuilder.Attributes;
using System.Dynamic;
using System.Net.Http;
using System.Web;
using System.Web.Script.Serialization;
using IrisModels.Models;
using System.Web.UI.WebControls;

namespace CoreDomain
{
    public sealed class CoreService
    {
        private const string C_SESSION_USER_INFO_KEY = "IRIS_USER_INFO";
        private const int C_SESSION_LIFESPAN = 15 * 60;
        private const int C_SALT_VALUE_SIZE = 16;

        #region Lookup Functions
        public List<ExpandoObject> LoadLookupData(Type model)
        {
            List<ExpandoObject> o = new List<ExpandoObject>();//Activator.CreateInstance<List<dynamic>>();
            string keyField = "";
            string tableName = "";
            SqlTable[] tables = null;
            List<object> values = new List<object>();
            if (model.GetCustomAttribute<ModelDataBindingsAttribute>() != null)
            {
                keyField = model.GetCustomAttribute<ModelDataBindingsAttribute>().KeyFieldName;
                tableName = model.GetCustomAttribute<ModelDataBindingsAttribute>().TableName;
            }
            else
            {
                foreach (var p in model.GetProperties())
                {
                    if (p.GetCustomAttribute<KeyAttribute>() != null)
                        keyField = p.Name;
                }
                tableName = model.Name.Replace("Model","");
            }
            HttpContext.Current.Session["CurrentLookupTable"] = model as Type;
            DatabaseModelBindings bindings = new DatabaseModelBindings { DatabaseName = "CountyDatabase", KeyFieldName = keyField, TableName = tableName };
            var gen = new SqlGenerator(SqlGenerator.SqlTypes.Select, bindings.TableName, optionalTables:tables);
            SqlDataReader dr = SQLHelper.FetchDataReader(gen, bindings.DatabaseName);
            if (dr != null)
            {
                try
                {
                    while (dr.Read())
                    {
                        ExpandoObject newObject = new ExpandoObject();
                        var expandoDict = newObject as IDictionary<string, object>;

                        for (int i = 0; i < dr.FieldCount; i++)
                        {
                            string name = dr.GetName(i);
                            PropertyInfo propertyInfo = model.GetProperty(name);

                            if (dr[i] != System.DBNull.Value && propertyInfo != null)
                            {
                                object val = dr[i];
                                Type newType = val.GetType();
                                
                                if (propertyInfo.PropertyType.UnderlyingSystemType == typeof(bool))
                                {
                                    if (newType == typeof(bool))
                                        expandoDict.Add(propertyInfo.Name, val);
                                    else if (newType == typeof(byte))
                                        expandoDict.Add(propertyInfo.Name, (byte) val == 1);
                                }
                                else if (propertyInfo.PropertyType.UnderlyingSystemType == typeof(string))
                                {
                                    expandoDict.Add(propertyInfo.Name, val.ToString().Replace("#", "\\#"));
                                }
                                else
                                {
                                    expandoDict.Add(propertyInfo.Name, val);
                                }
                            }
                        }
                        o.Add(newObject);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Connection Failed to Open, Exception is: {ex.Message}");
                }
                finally
                {
                    dr.Close();
                }

            }
            return o;
        }

        public string UpdateLookupModels(IDictionary<string, object> obj, string connectionString = "CountyDatabase")
        {
            Type t = HttpContext.Current.Session["CurrentLookupTable"] as Type;

            string tableName = t.Name;
            
            if (tableName.ToUpper().EndsWith("MODEL"))
            {
                tableName = tableName.Remove(tableName.Length - 5, 5);
            }

            var gen = new SqlGenerator(SqlGenerator.SqlTypes.SprocUpdate, tableName);
            gen.UpdateFromExpando(obj, t);
            
            return SQLHelper.ExecuteSql(gen, connectionString).ToString();
        }

        public string InsertLookupModels(IDictionary<string, object> obj, string connectionString = "CountyDatabase")
        {
            Type t = HttpContext.Current.Session["CurrentLookupTable"] as Type;

            string tableName = t.Name;

            if (tableName.ToUpper().EndsWith("MODEL"))
            {
                tableName = tableName.Remove(tableName.Length - 5, 5);
            }
            

            if (obj[tableName + "_Key"] == null) //Temporary
                obj[tableName + "_Key"] = SQLHelper.GetUniqueKey();

            var gen = new SqlGenerator(SqlGenerator.SqlTypes.SprocInsert, tableName);
            //wheres?.ForEach(w => gen.AddWhereParameter(w));
            gen.InsertFromExpando(obj, t);

            return SQLHelper.ExecuteSql(gen, connectionString).ToString();
        }

        #endregion



        #region Report Related Functions
        public Telerik.Reporting.SqlDataSource SqlReportSource(SqlGenerator gen, string cn = "")
        {
            return SQLHelper.GetSqlSourceObject(gen,cn);
        }

        public List<object> ReportDataTables(bool isFilter, string[] models, string baseTable, string prop, bool allRecords, string userID, List<SqlWhere> wheres = null)
        {
            Dictionary<string, string> typeList = new Dictionary<string, string>();
            DatabaseModelBindings bindings = new DatabaseModelBindings { DatabaseName = "CountyDatabase", KeyFieldName = prop.Split('.')[0] + "_Key", TableName = prop.Split('.')[0] };
            SqlTable[] tables = null;
            List<object> values = new List<object>();
            if (HttpContext.Current.Session["TypeListDictionary"] != null)
                typeList = (Dictionary<string, string>) HttpContext.Current.Session["TypeListDictionary"];

            if (models != null)
            {
                tables = new SqlTable[models.Length]; //no basemodel
                if(models.Length > 0)
                {
                    if(baseTable != prop.Split('.')[0])
                        tables[0] = new SqlTable { JoinType = "INNER", Name = baseTable, JoiningTable = prop.Split('.')[0], JoinFieldNameA = prop.Split('.')[0] + "_Key", JoinFieldNameB = prop.Split('.')[0] + "_Key" };
                    var rem = new List<string>(models);
                    rem.Remove(prop.Split('.')[0]);
                    models = rem.ToArray();
                    int count = 0;
                    for (var i = 0; i < models.Length; i++)
                    {

                        if(tables[count] == null)
                            tables[count] = new SqlTable { JoinType = "INNER", Name = models[i], JoiningTable = baseTable, JoinFieldNameA = models[i] + "_Key", JoinFieldNameB = models[i] + "_Key" };
                        else if(count+1 < tables.Length)
                            tables[count+1] = new SqlTable { JoinType = "INNER", Name = models[i], JoiningTable = baseTable, JoinFieldNameA = models[i] + "_Key", JoinFieldNameB = models[i] + "_Key" };
                        count++;
                    }
                }
            }
            var gen = new SqlGenerator(SqlGenerator.SqlTypes.Select, bindings.TableName, optionalTables:tables);
            wheres?.ForEach(w => gen.AddWhereParameter(w));

            SqlDataReader dr = SQLHelper.FetchDataReader(gen, bindings.DatabaseName);

            if (dr != null)
            {
                try
                {
                    while (dr.Read())
                    {
                        object newObject = Activator.CreateInstance<object>();

                        for (int i = 0; i < dr.FieldCount; i++)
                        {
                            string name = dr.GetName(i);
                            if (dr[i] != DBNull.Value && name == prop.Split('.')[1])
                            {
                                if (!typeList.ContainsKey(name))
                                    typeList.Add(name, dr.GetDataTypeName(i));
                                if (isFilter)
                                {
                                    if (dr.GetDataTypeName(i) == "tinyint")
                                    {
                                        int r;
                                        int.TryParse(dr[i].ToString(), out r);
                                        if (r == 1 && !values.Contains(true))
                                        {
                                            values.Add(true);
                                        }
                                        else if (r == 0 && !values.Contains(false))
                                        {
                                            values.Add(false);
                                        }
                                    }
                                    else
                                        values.Add(dr[i]);
                                }
                                else
                                    values.Add(dr[i]);
                            }
                        }
                    }
                    HttpContext.Current.Session["TypeListDictionary"] = typeList;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Connection Failed to Open, Exception is: {ex.Message}");
                }
                finally
                {
                    dr.Close();
                }
               
            }
            if (allRecords)
                return values.ToList();
            return values.Take(20).ToList();
        }
       
        public string TelerikSqlString(string props, string baseModel, string models, string relations, string userID, string wheres = null)
        {
            Dictionary<string, Guid> modelGuids = (Dictionary<string, Guid>) HttpRuntime.Cache["ModelGuids"];
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<ReportModelListViewModel> mods = (List<ReportModelListViewModel>)serializer.Deserialize(models, typeof(List<ReportModelListViewModel>));
            List<ReportRelationshipViewModel> relationships = (List<ReportRelationshipViewModel>) serializer.Deserialize(relations, typeof(List<ReportRelationshipViewModel>));
            List<SqlWhereViewModel> filters = (List<SqlWhereViewModel>) serializer.Deserialize(wheres, typeof(List<SqlWhereViewModel>));
            StringBuilder sb = new StringBuilder();
            List<StringBuilder> filterStrings = new List<StringBuilder>();
            var prop = props.Split(',');
            sb.Append("SELECT ");
            int count = 0;
            foreach(var p in prop)
            {
                if(count == 0)
                    sb.Append("(" + p + ") ");
                else
                    sb.Append(", (" + p + ")");
                count++;
            }
            sb.Append(" FROM " + modelGuids.FirstOrDefault(k => k.Value == Guid.Parse(baseModel)).Key);
            if(relationships != null)
            {
                foreach (var o in relationships)
                {
                    sb.Append(" " + o.JoinType + " " + modelGuids.FirstOrDefault(k => k.Value == Guid.Parse(o.ToModelGUID)).Key + " ON " + modelGuids.FirstOrDefault(k => k.Value == Guid.Parse(o.ModelGUID)).Key + "." + o.Prop + " = " + modelGuids.FirstOrDefault(k => k.Value == Guid.Parse(o.ToModelGUID)).Key + "." + o.ToProp);
                }
            }
            if(wheres != null && wheres != "null")
            {
                sb.Append(" WHERE ");
                var indexedWhere = 0;
                foreach (var w in filters)
                {
                    StringBuilder temp = new StringBuilder();
                    if(indexedWhere > 0)
                    {
                        if(w.AndOr == SqlWhereAndOrOptions.SqlWhereAndOr.Or)
                        {
                            sb.Append(" OR ");
                            temp.Append(" OR ");
                        }
                        else
                        {
                            sb.Append(" AND ");
                            temp.Append(" AND ");
                        }
                    }
                    if(w.Group1 != null && w.Group1 != "")
                    {
                        sb.Append(" ( ");
                    }
                    if(w.MockValue1?.ToString() == "True")
                    {
                        w.MockValue1 = "1";
                    }else if (w.MockValue1?.ToString() == "False")
                    {
                        w.MockValue1 = "0";
                    }
                    if (w.MockValue2?.ToString() == "True")
                    {
                        w.MockValue2 = "1";
                    }
                    else if (w.MockValue2?.ToString() == "False")
                    {
                        w.MockValue2 = "0";
                    }
                    switch (w.MockComparator)
                    {
                        case "CONTAINS":
                            sb.Append(w.MockTableName + "." + w.MockFieldName + " LIKE " + "'%" + w.MockValue1 + "%'");
                            filterStrings.Add(temp.Append(w.MockTableName + "." + w.MockFieldName + " LIKE " + "'%" + w.MockValue1 + "%'" ));
                            break;
                        case "STARTS WITH":
                            sb.Append(w.MockTableName + "." + w.MockFieldName + " STARTS WITH " + "'" + w.MockValue1 + "%'");
                            filterStrings.Add(temp.Append(w.MockTableName + "." + w.MockFieldName + " STARTS WITH " + "'" + w.MockValue1 + "%'"));
                            break;
                        case "ENDS WITH":
                            sb.Append(w.MockTableName + "." + w.MockFieldName + " ENDS WITH " + "'%" + w.MockValue1 + "'");
                            filterStrings.Add(temp.Append(w.MockTableName + "." + w.MockFieldName + " ENDS WITH " + "'%" + w.MockValue1 + "'"));
                            break;
                        case "BETWEEN":
                            sb.Append(w.MockTableName + "." + w.MockFieldName + " " + w.MockComparator + " '" + w.MockValue1 + "' AND '" + w.MockValue2 + "'");
                            filterStrings.Add(temp.Append(w.MockTableName + "." + w.MockFieldName + " " + w.MockComparator + " '" + w.MockValue1 + "' AND '" + w.MockValue2 + "'"));
                            break;
                        case "LIKE":
                            sb.Append(w.MockTableName + "." + w.MockFieldName + " LIKE " + "'%" + w.MockValue1 + "%'");
                            filterStrings.Add(temp.Append(w.MockTableName + "." + w.MockFieldName + " LIKE " + "'%" + w.MockValue1 + "%'"));
                            break;
                        default:
                            sb.Append(w.MockTableName + "." + w.MockFieldName + " " + w.MockComparator + " " + "'" + w.MockValue1 + "'");
                            filterStrings.Add(temp.Append(w.MockTableName + "." + w.MockFieldName + " " + w.MockComparator + " " + "'" + w.MockValue1 + "'"));
                            break;
                    }
                    if (w.Group2 != null && w.Group2 != "")
                    {
                        sb.Append(" ) ");
                    }
                    indexedWhere++;
                }
                HttpContext.Current.Session["ReportFilterStrings"] = filterStrings;
            }
            //Select props from base inner join model on ... = ... where ...
            return sb.ToString();
        }
        #endregion

        #region Math Functions
        public int RecordCount<T>(List<SqlWhere> wheres = null)
        {
            Type t = typeof(T);
            List<T> o = Activator.CreateInstance<List<T>>();
            int openPcount = 0;
            int closePcount = 0;
            DatabaseModelBindings bindings = t.GetDatabaseBindings();
            var gen = new SqlGenerator(SqlGenerator.SqlTypes.Count, bindings.TableName, bindings.KeyFieldName, tenantkey: false, tenant: HttpContext.Current.Session["CurrentTenantKey"]);
            if (wheres != null)
            {
                foreach (SqlWhere w in wheres)
                {
                    if (w.Group1 != null)
                        openPcount++;
                    if (w.Group2 != null)
                        closePcount++;
                }
            }

            if (openPcount == closePcount)
                wheres?.ForEach(w => gen.AddWhereParameter(w));
            else Debug.WriteLine(openPcount + " " + closePcount);

            int total = SQLHelper.ExecuteSql(gen, connectionName: bindings.DatabaseName);

            return total;
        }

        public int RecordCount(Type model, List<SqlWhere> wheres = null)
        {
            int openPcount = 0;
            int closePcount = 0;
            DatabaseModelBindings bindings = model.GetDatabaseBindings();
            var gen = new SqlGenerator(SqlGenerator.SqlTypes.Count, bindings.TableName, bindings.KeyFieldName, tenantkey: false, tenant: HttpContext.Current.Session["CurrentTenantKey"]);
            if (wheres != null)
            {
                foreach (SqlWhere w in wheres)
                {
                    if (w.Group1 != null)
                        openPcount++;
                    if (w.Group2 != null)
                        closePcount++;
                }
            }

            if (openPcount == closePcount)
                wheres?.ForEach(w => gen.AddWhereParameter(w));
            else Debug.WriteLine(openPcount + " " + closePcount);

            int total = SQLHelper.ExecuteSql(gen, connectionName: bindings.DatabaseName);

            return total;
        }

        public double Aggregate<T>(string type, string field, List<SqlWhere> wheres = null, string conName = "CountyDataBase")
        {
            SqlGenerator.SqlTypes sqlType = SqlGenerator.SqlTypes.Count;
            int openPcount = 0;
            int closePcount = 0;
            switch (type)
            {
                case "Count":
                    sqlType = SqlGenerator.SqlTypes.Count;
                    break;
                case "Maximum":
                    sqlType = SqlGenerator.SqlTypes.Max;
                    break;
                case "Minimum":
                    sqlType = SqlGenerator.SqlTypes.Min;
                    break;
                case "Average":
                    sqlType = SqlGenerator.SqlTypes.Avg;
                    break;
                case "Sum":
                    sqlType = SqlGenerator.SqlTypes.Sum;
                    break;
            }
            Type t = typeof(T);
            List<T> o = Activator.CreateInstance<List<T>>();
            DatabaseModelBindings bindings = t.GetDatabaseBindings();
            var gen = new SqlGenerator(sqlType, bindings.TableName, tenantkey: false, tenant: HttpContext.Current.Session["CurrentTenantKey"], field: field);

            if (wheres != null)
            {
                foreach (SqlWhere w in wheres)
                {
                    if (w.Group1 != null)
                        openPcount++;
                    if (w.Group2 != null)
                        closePcount++;
                }
            }
            if (openPcount == closePcount)
                wheres?.ForEach(w => gen.AddWhereParameter(w));

            var total = SQLHelper.ExecuteSqlFloat(gen, connectionName: conName);
            return total;
        }
        #endregion

        /// <summary>
        /// Uses the SQL Generator to dynamically create a SELECT statement based on 
        /// the properties of the model referenced and returns an array of the
        /// referenced model populated from the API.
        /// </summary>
        public IEnumerable<T> LoadModel<T>(List<SqlWhere> wheres = null, int pageSize = 0, int currentPage = 0, object tenant = null, List<string> groups = null, bool api = false, string conName = null, string database = null, string orderfield = null)
        {
            Type t = typeof(T);
            List<T> o = Activator.CreateInstance<List<T>>();
            int openPcount = 0;
            int closePcount = 0;
            if(HttpContext.Current != null && tenant == null)
            {
                tenant = HttpContext.Current.Session["CurrentTenantKey"];
            }
            DatabaseModelBindings bindings = t.GetDatabaseBindings();

            var gen = new SqlGenerator(SqlGenerator.SqlTypes.Select, bindings.TableName, keyFieldName: bindings.KeyFieldName, tenantkey: false, tenant: tenant);
            gen.SetupPagination(pageSize, currentPage, groups);
           gen.SelectFromModel<T>();

            if(wheres != null)
            {
                foreach (SqlWhere w in wheres)
                {
                    if (w.Group1 != null)
                        openPcount++;
                    if (w.Group2 != null)
                        closePcount++;
                }
            }
            

            if (openPcount == closePcount)
                wheres?.ForEach(w => gen.AddWhereParameter(w));
            else Debug.WriteLine(openPcount + " " + closePcount);

            if(orderfield != null)
                gen.AddOrderBy(orderfield);

            SqlDataReader dr = SQLHelper.FetchDataReader(gen, conName != null ? conName : bindings.DatabaseName, null, api);

            if (dr != null)
            {
                try
                {
                    while (dr.Read())
                    {
                        T newObject = Activator.CreateInstance<T>();

                        for (int i = 0; i < dr.FieldCount; i++)
                        {
                            string name = dr.GetName(i);

                            PropertyInfo propertyInfo = t.GetProperty(name);
                            

                            if (dr[i] != DBNull.Value)
                            {
                                if (propertyInfo?.CanWrite == true)
                                {
                                    object val = dr[i];
                                    Type newType = val.GetType();
                                    
                                    if (propertyInfo.PropertyType.UnderlyingSystemType == typeof(string))
                                    {
                                        propertyInfo.SetValue(newObject, val.ToString().Replace("#", "\\#")); //Required for kendo
                                    }
                                    else
                                    {
                                        propertyInfo.SetValue(newObject, val);
                                    }
                                }
                            }
                        }
                        o.Add(newObject);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Connection Failed to Open, Exception is: {ex.Message}");
                }
                finally
                {
                    dr.Close(); //Added to prevent too many connections being open
                }
            }
            else
            {
                Console.WriteLine();
            }

            return o;
        }
        public DataSet LoadDataSet<T>(object tenant, string database = null, bool api = false)
        {

            Type t = typeof(T);
            DataSet ds = new DataSet();
            string baseTable = t.GetCustomAttribute<APIContainerAttribute>().BaseModel.Name;
            foreach (PropertyInfo p in t.GetProperties())
            {
                DataTable dt = LoadDataTable(p.PropertyType, tenant:tenant,api: api, database:database);
                if (dt == null)
                {
                    dt = new DataTable();
                }

                dt.TableName = p.PropertyType.Name;
                var frc = p.GetCustomAttribute<APIForeignRelationAttribute>();
                ds.Tables.Add(dt);


                if(frc != null)
                {
                    ds.Relations.Add(ds.Tables[baseTable].Columns[frc.PrimaryTableKeyName], ds.Tables[p.PropertyType.Name].Columns[frc.ForeignKeyTableName]);
                }
            }
            return ds;
        }

        public DataTable LoadDataTable(Type t, List<SqlWhere> wheres = null, int pageSize = 0, int currentPage = 0, object tenant = null, List<string> groups = null, bool api = false, string database = null)
        {
            int openPcount = 0;
            int closePcount = 0;
            if (HttpContext.Current != null && tenant == null)
            {
                tenant = HttpContext.Current.Session["CurrentTenantKey"];
            }
            
            DatabaseModelBindings bindings = t.GetDatabaseBindings();

            var gen = new SqlGenerator(SqlGenerator.SqlTypes.Select, bindings.TableName, tenantkey: false, tenant: tenant);
            gen.SetupPagination(pageSize, currentPage, groups);
            gen.SelectFromModel(t);

            if (wheres != null)
            {
                foreach (SqlWhere w in wheres)
                {
                    if (w.Group1 != null)
                        openPcount++;
                    if (w.Group2 != null)
                        closePcount++;
                }
            }
            
            if (openPcount == closePcount)
                wheres?.ForEach(w => gen.AddWhereParameter(w));
            else Debug.WriteLine(openPcount + " " + closePcount);

            DataTable dt = SQLHelper.FetchDataTable(gen, bindings.DatabaseName, database, api);
            
            return dt;
        }
        
        public string SprocUpdate<T>(T model, string connectionString = "CountyDatabase")
        {
            Type t = model.GetType();
            
            string tableName = t.Name;
            
            if (tableName.ToUpper().EndsWith("MODEL"))
            {
                tableName = tableName.Remove(tableName.Length - 5, 5);
            }

            var gen = new SqlGenerator(SqlGenerator.SqlTypes.SprocUpdate, tableName);
            gen.UpdateFromModel(model);
            

            return SQLHelper.ExecuteSql(gen, connectionString).ToString();
        }

        public string SprocInsert<T>(T model, string connectionString = "CountyDatabase")
        {
            Type t = model.GetType();

            string tableName = t.Name;

            if (tableName.ToUpper().EndsWith("MODEL"))
            {
                tableName = tableName.Remove(tableName.Length - 5, 5);
            }

            var gen = new SqlGenerator(SqlGenerator.SqlTypes.SprocInsert, tableName);
            gen.InsertFromModel(model);


            return SQLHelper.ExecuteSql(gen, connectionString).ToString();
        }

        public string SprocDelete<T>(T model, string connectionString = "CountyDatabase")
        {
            try
            {
                Type t = model.GetType();
                string tableName = t.Name;
                
                if (tableName.ToUpper().EndsWith("MODEL"))
                {
                    tableName = tableName.Remove(tableName.Length - 5, 5);
                }

                var gen = new SqlGenerator(SqlGenerator.SqlTypes.SprocDelete, tableName);
                gen.DeleteFromModel(model);
                return SQLHelper.ExecuteSql(gen, connectionString).ToString();

            }
            catch { return ""; }
        }
        
        public bool StartSession<T>(string username, string password, out string token, out DateTime expiration)
        {
            SqlGenerator sqlGen = new SqlGenerator(SqlGenerator.SqlTypes.Select, "IRISUser");
            sqlGen.AddField("*");
            sqlGen.AddWhereParameter(null, "IRISUser", "Email", username, SqlWhereComparison.SqlComparer.Equal, null); //users compare email to login, will be unique

            DatabaseModelBindings ModelBinding = new DatabaseModelBindings();

            ModelBinding = typeof(T).GetDatabaseBindings();
            DataTable dt = SQLHelper.FetchDataTable(sqlGen, ModelBinding.DatabaseName);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["HashPassword"].ToString() == CryptoHelper.ComputeHash(password, dt.Rows[0]["SALT"].ToString()))
                {
                    string sessionKey = Guid.NewGuid().ToString("N");
                    DateTime sessionExpires = DateTime.Now.AddMinutes(C_SESSION_LIFESPAN);

                    IRISUserModel user = LoadModel<IRISUserModel>(conName: "IrisAuth").FirstOrDefault(u => u.User_Key == (int)dt.Rows[0]["User_Key"]);
                    user.AuthGUID = sessionKey;
                    user.AuthDate = sessionExpires;
                    user.UpdatedByUser_Key = user.User_Key;

                    SprocUpdate(user, "IrisAuth");

                    token = sessionKey;
                    expiration = sessionExpires;

                    return true;
                }
            }

            token = null;
            expiration = DateTime.MinValue;
            return false;
        }

        public bool ValidateSessionKey(string sessionGuid)
        {
            SqlGenerator sqlGen = new SqlGenerator(SqlGenerator.SqlTypes.Select, "IRISUser");
            sqlGen.AddField("User_Key");
            sqlGen.AddWhereParameter(null, "IRISUser", "AuthGUID", sessionGuid, SqlWhereComparison.SqlComparer.Equal, null);

            //var ret = SQLHelper.ExecuteSql(sqlGen, "IrisAuth");
            return SQLHelper.FetchValueString(sqlGen, "IrisAuth").ToString().Length > 0;
            //return true;
        }

        public void Terminate<T>(string sessionToken)
        {
            var modelBinding = typeof(T).GetDatabaseBindings();
            IRISUserModel user = LoadModel<IRISUserModel>(conName: "IrisAuth").FirstOrDefault(u => u.AuthGUID == sessionToken);
            user.AuthDate = DateTime.UtcNow.AddYears(-1);
            SprocUpdate(user);
        }

        public Dictionary<string, int> BuildAuthUserInformationModel<T>(string userKey, Dictionary<string, int> roleLookup)
        {
            // BL TODO - reimplement with new security model

            // Lookup roles for this current user

            return roleLookup;
        }

        private int GetCurrentUserKey(HttpSessionStateBase session = null)
        {
            if(session == null)
                return (int) HttpContext.Current.Session["CurrentUserKey"];
            else
                return (int) session["CurrentUserKey"];
        }

        private string GetTableNameOfModel(string modelName)
        {
            if (modelName.ToUpper().EndsWith("MODEL"))
                return modelName.Remove(modelName.Length - 5, 5);
            else
                return modelName;
        }

        public string GetModelKeyValue<T>(T model)
        {
            return typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                   .FirstOrDefault(p => p.GetCustomAttributes(typeof(KeyAttribute), false).Count() == 1).GetValue(model).ToString();

        }

        public string GetModelKeyFieldName<T>(T model)
        {
            return typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                   .FirstOrDefault(p => p.GetCustomAttributes(typeof(KeyAttribute), false).Count() == 1).Name;
        }

        public string GetNewKeyValue<T>(T model)
        {
            if ( typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                   .FirstOrDefault(p => p.GetCustomAttributes(typeof(KeyAttribute)).Count() == 1 && p.GetCustomAttributes(typeof(IsAutoNumberAttribute)).Count() != 1) != null)
            {
                return SQLHelper.GetUniqueKey();
            }
            else
            {
                return null;
            }
        }

        public static string SqlDebug;

        public static object NewKey;

        public void SetSqlVariables(string sqlDebug, object newKey)
        {
            SqlDebug = sqlDebug;
            NewKey = newKey;
        }

        public string GetSqlDebug()
        {
            return SqlDebug;
        }

        public object GetNewKey()
        {
            return NewKey;
        }
    }

}
