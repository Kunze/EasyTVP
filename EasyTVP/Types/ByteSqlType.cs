using System;
using System.Reflection;
using Microsoft.SqlServer.Server;
using System.Data;

namespace EasyTVP.Types
{
    internal class ByteSqlType : NullableSqlType<Byte>
    {
        protected override SqlMetaData GetSqlMetaData(PropertyInfo property)
        {
            return new SqlMetaData(property.Name, SqlDbType.TinyInt);
        }

        protected override void SetRecord(SqlDataRecord record, int index, object value)
        {
            record.SetByte(index, (byte)value);
        }
    }
}
