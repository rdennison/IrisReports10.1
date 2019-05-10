using SqlComponents;
using System;

namespace CoreDomain.Filters
{
    public sealed class GridConfigurationFilter : IDataFilter
    {
        public string pageName { get; set; }
        public int gridConfigurationKey { get; set; }

        public void Apply(SqlGenerator sqlgen)
        {
            //Option to retreive all grid configurations for a page
            //if (string.IsNullOrEmpty(pageName) == false)
            //    {
            //        using (UserService us = new UserService())
            //        {
            //            sqlgen.AddWhereParameter("GridConfiguration", "pageName", pageName, SqlWhereComparison.SqlComparer.Equal);
            //            sqlgen.AddWhereParameter("GridConfiguration", "User_Key", us.CurrentUser(), SqlWhereComparison.SqlComparer.Equal);
            //        }
            //    }
            //    // Option to retreive a specific all grid configurations by its key
            //    else
            //    {
            //        sqlgen.AddWhereParameter("GridConfiguration", "GridConfiguration_Key", gridConfigurationKey, SqlWhereComparison.SqlComparer.Equal);
            //    }
        }
    }
}

