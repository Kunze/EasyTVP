using System.Data;
using System.Reflection;
using Microsoft.SqlServer.Server;
using EasyTVP.Attributes;

namespace EasyTVP.Types
{
    internal class StringSqlType : NullableSqlType<string>
    {
        protected override SqlMetaData GetSqlMetaData(PropertyInfo property)
        {
            var maxLengthAttribute = property.GetCustomAttribute<SqlMaxLengthAttribute>();
            var maxLength = maxLengthAttribute?.MaxLength ?? 1000;

            return new SqlMetaData(property.Name, GetAttributeSqlDbType(property) ?? SqlDbType.VarChar, maxLength);
        }

        protected override void SetRecord(SqlDataRecord record, int index, object value)
        {
            record.SetString(index, value as string);
        }
    }
}
