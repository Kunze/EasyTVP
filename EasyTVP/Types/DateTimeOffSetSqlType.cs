using System.Data;
using Microsoft.SqlServer.Server;
using System;
using System.Reflection;

namespace EasyTVP.Types
{
    internal class DateTimeOffSetSqlType : NullableSqlType<DateTimeOffset>
    {
        protected override SqlMetaData GetSqlMetaData(PropertyInfo property)
        {
            return new SqlMetaData(property.Name, SqlDbType.DateTimeOffset);
        }

        protected override void SetRecord(SqlDataRecord record, int index, object value)
        {
            record.SetDateTimeOffset(index, (DateTimeOffset)value);
        }
    }
}
