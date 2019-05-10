

using SqlComponents;

namespace CoreDomain.Filters
{
    public sealed class IRISReleaseDetailFilter : IDataFilter
    {
        public int? IrisReleaseKey { get; set; }

        public void Apply(SqlGenerator sqlgen)
        {
            if (IrisReleaseKey == null)
            {
                sqlgen.AddWhereParameter(null, "IRISReleaseDetail", "IRISRelease_Key", IrisReleaseKey, SqlWhereComparison.SqlComparer.Equal, null);
            }
        }
    }
}