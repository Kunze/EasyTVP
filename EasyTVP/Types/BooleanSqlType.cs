using System.Data;
using Microsoft.SqlServer.Server;
using System.Reflection;

namespace EasyTVP.Types
{
    internal class BooleanSqlType : NullableSqlType<bool>
    {
        protected override SqlMetaData GetSqlMetaData(PropertyInfo property)
        {
            return new SqlMetaData(property.Name, SqlDbType.Bit);
        }

        protected override void SetRecord(SqlDataRecord record, int index, object value)
        {
            record.SetBoolean(index, (bool)value);
        }
    }
}
