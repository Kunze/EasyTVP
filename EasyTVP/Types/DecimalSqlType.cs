﻿using System.Data;
using Microsoft.SqlServer.Server;
using System.Reflection;
using EasyTVP.Attributes;

namespace EasyTVP.Types
{
    public class DecimalSqlType : NullableSqlType<decimal>
    {
        protected override SqlMetaData GetSqlMetaData(PropertyInfo property)
        {
            var type = SqlDataRecordTypeAttribute.GetAttributeSqlDbType(property) ?? SqlDbType.Decimal;

            return new SqlMetaData(property.Name, type);
        }
    }
}
