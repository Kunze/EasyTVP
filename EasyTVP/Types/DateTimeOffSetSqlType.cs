using System.Data;
using Microsoft.SqlServer.Server;
using System;
using System.Reflection;

namespace EasyTVP.Types
{
    public class DateTimeOffSetSqlType : NullableSqlType<DateTimeOffset>
    {
        protected override SqlMetaData GetSqlMetaData(PropertyInfo property)
        {
            return new SqlMetaData(property.Name, SqlDbType.DateTimeOffset);
        }
    }
}
