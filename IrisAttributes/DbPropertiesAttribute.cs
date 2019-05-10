using System;
using System.Data;

namespace IrisAttributes
{
    public class DbPropertiesAttribute : Attribute
    {
        public SqlDbType DatabaseType { get; set; }

        public int Precision { get; set; }

        public int Scale { get; set; }

        public int Size { get; set; }

        public DbPropertiesAttribute()
        {
            
        }
    }
}
