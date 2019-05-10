using SqlComponents;

namespace CoreDomain.Filters
{
    public interface IDataFilter
    {
        void Apply(SqlGenerator sqlGen);
    }
}
