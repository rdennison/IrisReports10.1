


using SqlComponents;

namespace CoreDomain.Filters
{
    public sealed class ResourceClassFilter : IDataFilter
    {
        public string pageName { get; set; }
        public string resourceTypeKey { get; set; }

        public void Apply(SqlGenerator sqlgen)
        {
            
                if (string.IsNullOrEmpty(resourceTypeKey) == false)
                {
                    sqlgen.AddField("ResourceClass_Key");
                    sqlgen.AddField("NameDesc");
                    sqlgen.AddOrderBy("NameDesc");
                    sqlgen.AddWhereParameter(null, "ResourceClass", "Resource_Type_Key", resourceTypeKey, SqlWhereComparison.SqlComparer.Equal, null);
                }
            }
        }
    }
//}