using System.Data;
using Microsoft.SqlServer.Server;
using System;
using System.Reflection;

namespace EasyTVP.Types
{
    internal class DateTimeSqlType : NullableSqlType<DateTime>
    {
        protected override SqlMetaData GetSqlMetaData(PropertyInfo property)
        {
            //<TODO> usar attribute para pegar date, datetime, datetime2 ou smalldatetime
            return new SqlMetaData(property.Name, GetAttributeSqlDbType(property) ?? SqlDbType.DateTime);
        }

        protected override void SetRecord(SqlDataRecord record, int index, object value)
        {
            record.SetDateTime(index, (DateTime)value);
        }
    }
}
