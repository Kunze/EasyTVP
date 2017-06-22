using System;
using System.Reflection;
using Microsoft.SqlServer.Server;
using System.Data;

namespace EasyTVP.Types
{
    internal class TimeSpanSqlType : NullableSqlType<TimeSpan>
    {
        protected override SqlMetaData GetSqlMetaData(PropertyInfo property)
        {
            return new SqlMetaData(property.Name, SqlDbType.Time);
        }

        protected override void SetRecord(SqlDataRecord record, int index, object value)
        {
            record.SetTimeSpan(index, (TimeSpan)value);
        }
    }
}
