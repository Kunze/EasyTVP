using System.Data;
using Microsoft.SqlServer.Server;
using System;
using System.Reflection;

namespace EasyTVP.Types
{
    public class Int16SqlType : NullableSqlType<Int16>
    {
        protected override SqlMetaData GetSqlMetaData(PropertyInfo property)
        {
            return new SqlMetaData(property.Name, SqlDbType.SmallInt);
        }

        protected override void SetRecord(SqlDataRecord record, int index, object value)
        {
            record.SetInt16(index, (Int16)value);
        }
    }
}
