using System.Data;
using Microsoft.SqlServer.Server;
using System;
using System.Reflection;

namespace EasyTVP.Types
{
    public class Int64SqlType : NullableSqlType<Int64>
    {
        protected override SqlMetaData GetSqlMetaData(PropertyInfo property)
        {
            return new SqlMetaData(property.Name, SqlDbType.BigInt);
        }

        protected override void SetRecord(SqlDataRecord record, int index, object value)
        {
            record.SetInt64(index, (Int64)value);
        }
    }
}
