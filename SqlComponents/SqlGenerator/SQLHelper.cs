using System.Data.SqlClient;
using System.Data;
using System;
using System.Configuration;
using System.Web;

namespace SqlComponents
{
    public static class SQLHelper
    {
        //5 minute timeout, 300 seconds
        private const int MintCommandTimeout = 300;

        public static bool AddConnectionString(string connectionName, string connectionString)
        {
            try
            {
                ConfigurationManager.ConnectionStrings.Add(new ConnectionStringSettings(connectionName, connectionString));
                return true;
            }
            catch
            {
                return false;
            }
        }

        #region  Fetch Data Methods ****************************************************************************

        public static DataTable FetchDataTable(SqlGenerator gen, string connectionName = "CountyDatabase", string database = null, bool api = false)
        {
            try
            {
                SqlDataAdapter objDA = null;
                DataTable objDT = new DataTable();

                if (connectionName == "CountyDatabase" && api == true)
                    objDA = new SqlDataAdapter(gen.SqlString, database);
                else
                    objDA = new SqlDataAdapter(gen.SqlString, ConfigurationManager.ConnectionStrings[connectionName].ConnectionString);
                
                objDA.SelectCommand.CommandTimeout = MintCommandTimeout;

                foreach (SqlParameter param in gen.SqlVariables)
                {
                    objDA.SelectCommand.Parameters.Add(param);
                }

                objDA.Fill(objDT);

                return objDT;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public static SqlDataReader FetchDataReader(SqlGenerator gen, string connectionName = "CountyDatabase", string database = null, bool api = false)
        {
            
            SqlCommand objCommand = null;
            SqlConnection conDataReader = null;

            try
            {
                if (connectionName == "CountyDatabase" && api == true)
                    conDataReader = new SqlConnection(database);
                else
                    conDataReader = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionName].ConnectionString);

                try
                {
                    //Fetch datareader is first called when starting app.  If tcpip is not enabled or it cannot connect to server or database, we need to show a messagebox with error.
                    conDataReader.Open();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Connection Failed to Open, Exception is: {ex.Message}");
                    throw;
                }

                objCommand = new SqlCommand(gen.SqlString, conDataReader);
                foreach (SqlParameter param in gen.SqlVariables)
                {
                    objCommand.Parameters.Add(param);
                }
                objCommand.CommandTimeout = MintCommandTimeout;

                return objCommand.ExecuteReader(CommandBehavior.CloseConnection);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        #endregion

        #region  Single Value Return Functions ***************************************************************** 

        public static string FetchValueString(SqlGenerator gen, string connectionName = "CountyDatabase")
        {
            string returnValue = null;
            SqlDataReader myReader = null;

            try
            {
                myReader = FetchDataReader(gen, connectionName);

                while (myReader.Read())
                {
                    returnValue = myReader[0].ToString();
                }

            }
            catch (Exception)
            {

            }
            finally
            {
                if (myReader != null)
                {
                    if (!myReader.IsClosed)
                    {
                        myReader.Close();
                    }
                }
            }

            return returnValue ?? "";
        }

        public static string GetUniqueKey(string connectionName = "CountyDatabase")
	    {
		    string tempGetUniqueKey = null;
		    SqlConnection conConnection = null;
		    SqlDataReader myReader = null;

		    try
		    {
                if (connectionName == "CountyDatabase")
                    conConnection = new SqlConnection(HttpContext.Current.Session["CountyDatabase"].ToString());
                else
                    conConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionName].ConnectionString);


                var comCommand = new SqlCommand("GetnextKeyUSP", conConnection)
		        {
		            CommandType = CommandType.StoredProcedure,
		            CommandTimeout = MintCommandTimeout
		        };
		        conConnection.Open();
			    myReader = comCommand.ExecuteReader();

			    while (myReader.Read())
			    {
				    tempGetUniqueKey = myReader.GetString(0).ToString();
			    }

		    }
		    catch (Exception)
		    {
		    }
		    finally
		    {
			    if (myReader != null)
			    {
				    if (!myReader.IsClosed)
				    {
					    myReader.Close();
				    }
			    }

		        if (conConnection?.State == ConnectionState.Open)
		        {
		            conConnection.Close();
		        }
		    }

		    return tempGetUniqueKey;
	    }

        #endregion

        #region  Execute Methods ******************************************************************************* 

        public static int ExecuteSql(SqlGenerator gen, string connectionName = "CountyDatabase")
        {
            int tempExecuteSql = 0;
            object sqlRet;
            string sql = gen.SqlString;
            SqlConnection objCon = null;

            objCon = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionName].ConnectionString);

            using (objCon)
            {
                var objCommand = new SqlCommand(sql, objCon);
                if (sql.Contains("_INSERT") || sql.Contains("_UPDATE") || sql.Contains("_DELETE"))
                    objCommand.CommandType = CommandType.StoredProcedure;

                foreach (var param in gen.SqlVariables)
                {
                    objCommand.Parameters.Add(param);
                }
                objCommand.CommandTimeout = MintCommandTimeout;
                

                if (objCon.State != ConnectionState.Open)
                {
                    objCon.Open();
                }
                
                try
                {
                    tempExecuteSql = Convert.ToInt32(objCommand.ExecuteScalar());
                    try
                    {
                        var return_msg = "";
                        var rows_affected = 0;
                        var return_code = 0;
                        var new_key = 0;
                        if (objCommand.Parameters.Contains("@ReturnMsg") && objCommand.Parameters["@ReturnMsg"] != null)
                            return_msg = objCommand.Parameters["@ReturnMsg"].Value.ToString();
                        if (objCommand.Parameters.Contains("@RecordsAffected") && objCommand.Parameters["@RecordsAffected"] != null)
                            rows_affected = (int) objCommand.Parameters["@RecordsAffected"].Value;
                        if (objCommand.Parameters.Contains("@return_value") && objCommand.Parameters["@return_value"] != null)
                            return_code = (int) objCommand.Parameters["@return_value"].Value;
                        if (objCommand.Parameters.Contains("@New_Key") && objCommand.Parameters["@New_Key"] != null)
                            new_key = (int) objCommand.Parameters["@New_Key"].Value;
                    }
                    catch (Exception e)
                    {
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }

                
            }
            return tempExecuteSql;
        }

        public static double ExecuteSqlFloat(SqlGenerator gen, string connectionName = "CountyDatabase")
        {
            double tempExecuteSql = 0;
            string sql = gen.SqlString;
            SqlConnection objCon = null;

            if (connectionName == "CountyDatabase")
                objCon = new SqlConnection(HttpContext.Current.Session["CountyDatabase"].ToString());
            else
                objCon = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionName].ConnectionString);
            
            using (objCon)
            {
                var objCommand = new SqlCommand(sql, objCon);

                foreach (var param in gen.SqlVariables)
                {
                    objCommand.Parameters.Add(param);
                }

                objCommand.CommandTimeout = MintCommandTimeout;

                if (objCon.State != ConnectionState.Open)
                {
                    objCon.Open();
                }
                try
                {
                    tempExecuteSql = Convert.ToDouble(objCommand.ExecuteScalar());
                }
                catch (Exception)
                {
                    tempExecuteSql = -1;
                }
                
            }
            return tempExecuteSql;
        }

        #endregion


    }
}