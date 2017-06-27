using System;
using System.Reflection;
using Microsoft.SqlServer.Server;
using System.Data;

namespace EasyTVP.Types
{
    public class ByteSqlType : NullableSqlType<Byte>
    {
        protected override SqlMetaData GetSqlMetaData(PropertyInfo property)
        {
            return new SqlMetaData(property.Name, SqlDbType.TinyInt);
        }
    }
}
