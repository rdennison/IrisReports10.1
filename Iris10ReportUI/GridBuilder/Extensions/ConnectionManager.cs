using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
namespace Iris10ReportUI.GridBuilder.Extensions
{
    public class ConnectionManager
    {
        public string ConnectionStrings(string user, string type)
        {
            return ConfigurationManager.ConnectionStrings[user].ConnectionString + ";Application Name=" + HttpContext.Current.Session["CurrentUserName"].ToString() + " " + type;

        }
    }
}