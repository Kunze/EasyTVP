using System;
using System.Data;
using System.Reflection;
using Microsoft.SqlServer.Server;

namespace EasyTVP.Types
{
    public class Int32SqlType : NullableSqlType<int>
    {
        protected override SqlMetaData GetSqlMetaData(PropertyInfo property)
        {
            return new SqlMetaData(property.Name, SqlDbType.Int);
        }

        protected override void SetRecord(SqlDataRecord record, int index, object value)
        {
            record.SetInt32(index, (int)value);
        }
    }
}
