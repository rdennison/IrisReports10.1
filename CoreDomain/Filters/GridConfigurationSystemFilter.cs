

using SqlComponents;

namespace CoreDomain.Filters
{
    public sealed class GridConfigurationSystemFilter : IDataFilter
    {
        public string pageName { get; set; }

        public void Apply(SqlGenerator sqlgen)
        {
            if (string.IsNullOrEmpty(pageName) == false)
            {
                sqlgen.AddWhereParameter(null, "GridConfigurationSystem", "PageName", pageName, SqlWhereComparison.SqlComparer.Equal, null);
            }
        }
    }
}