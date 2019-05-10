using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlComponents;

namespace Interfaces
{
    public interface ICoreService 
    {
        IEnumerable<T> LoadModel<T>(List<SqlWhere> wheres = null);

        string InsertModel<T>(T model, string connectionString = "CountyDatabase");

        string UpdateModel<T>(T model, string connectionString = "CountyDatabase");
        string DeleteModel<T>(T model, string connectionString = "CountyDatabase");

        bool StartSession<T>(string username, string password, out string token, out DateTime expiration);
    }
}
