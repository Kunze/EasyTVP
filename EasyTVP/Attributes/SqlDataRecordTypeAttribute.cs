using System;
using System.Data;
using System.Reflection;

namespace EasyTVP.Attributes
{
    public class SqlDataRecordTypeAttribute: Attribute
    {
        public SqlDbType Type { get; private set; }

        public SqlDataRecordTypeAttribute(SqlDbType type)
        {
            Type = type;
        }

        public static SqlDbType? GetAttributeSqlDbType(PropertyInfo property)
        {
            var sqlDbType = property.GetCustomAttribute<SqlDataRecordTypeAttribute>();

            return sqlDbType?.Type ?? null;
        }
    }
}
