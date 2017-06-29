using System.Data;
using Microsoft.SqlServer.Server;
using System.Reflection;
using EasyTVP.Attributes;
using EasyTVP.Types.Interfaces;
using System;

namespace EasyTVP.Types
{
    public class DecimalSqlType : ISqlType
    {
        public SqlMetaData GetMetadata(PropertyInfo property)
        {
            var type = property.GetCustomAttribute<SqlDataRecordTypeAttribute>()?.Type ?? SqlDbType.Decimal;

            return new SqlMetaData(property.Name, type);
        }
    }
}
