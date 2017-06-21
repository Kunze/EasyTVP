using System.Data;
using Microsoft.SqlServer.Server;
using System;
using System.Reflection;

namespace EasyTVP.Types
{
    public class SingleSqlType : NullableSqlType<float>
    {
        protected override SqlMetaData GetSqlMetaData(PropertyInfo property)
        {
            return new SqlMetaData(property.Name, SqlDbType.Real);
        }

        protected override void SetRecord(SqlDataRecord record, int index, object value)
        {
            record.SetFloat(index, (float)value);
        }
    }
}
