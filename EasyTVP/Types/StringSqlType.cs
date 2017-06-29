using System.Data;
using System.Reflection;
using Microsoft.SqlServer.Server;
using EasyTVP.Attributes;
using System;
using EasyTVP.Types.Interfaces;

namespace EasyTVP.Types
{
    public class StringSqlType : ISqlType
    {
        public static int DefaultMaxLength = 1000;

        public SqlMetaData GetMetadata(PropertyInfo property)
        {
            var type = property.GetCustomAttribute<SqlDataRecordTypeAttribute>()?.Type ?? SqlDbType.VarChar;
            var maxLength = property.GetCustomAttribute<SqlDataRecordMaxLengthAttribute>()?.MaxLength ?? DefaultMaxLength;

            return new SqlMetaData(property.Name, type, maxLength);
        }
    }
}
