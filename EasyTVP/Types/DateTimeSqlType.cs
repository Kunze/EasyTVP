﻿using System.Data;
using Microsoft.SqlServer.Server;
using System;
using System.Reflection;
using EasyTVP.Attributes;

namespace EasyTVP.Types
{
    public class DateTimeSqlType : NullableSqlType<DateTime>
    {
        protected override SqlMetaData GetSqlMetaData(PropertyInfo property)
        {
            var type = SqlDataRecordTypeAttribute.GetAttributeSqlDbType(property) ?? SqlDbType.DateTime;
            
            return new SqlMetaData(property.Name, type);
        }
    }
}
