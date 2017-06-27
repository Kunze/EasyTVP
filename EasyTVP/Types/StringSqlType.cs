using System.Data;
using System.Reflection;
using Microsoft.SqlServer.Server;
using EasyTVP.Attributes;
using System;

namespace EasyTVP.Types
{
    public class StringSqlType : NullableSqlType<string>
    {
        public static int DefaultMaxLength = 1000;

        protected override SqlMetaData GetSqlMetaData(PropertyInfo property)
        {
            var type = SqlDataRecordTypeAttribute.GetAttributeSqlDbType(property) ?? SqlDbType.VarChar;
            var maxLength = SqlDataRecordMaxLengthAttribute.GetSqlMaxLengthAttribute(property) ?? DefaultMaxLength;

            return new SqlMetaData(property.Name, type, maxLength);
        }
    }
}
