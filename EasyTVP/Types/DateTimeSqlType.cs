using System.Data;
using Microsoft.SqlServer.Server;
using System;
using System.Reflection;
using EasyTVP.Attributes;
using EasyTVP.Types.Interfaces;

namespace EasyTVP.Types
{
    public class DateTimeSqlType : ISqlType
    {
        public SqlMetaData GetMetadata(PropertyInfo property)
        {
            var type = property.GetCustomAttribute<SqlDataRecordTypeAttribute>()?.Type ?? SqlDbType.DateTime;
            
            return new SqlMetaData(property.Name, type);
        }
    }
}
