﻿using System.Data;
using Microsoft.SqlServer.Server;
using System;
using System.Reflection;
using EasyTVP.Types.Interfaces;

namespace EasyTVP.Types
{
    public class DateTimeOffSetSqlType : ISqlType
    {
        public SqlMetaData GetMetadata(PropertyInfo property)
        {
            return new SqlMetaData(property.Name, SqlDbType.DateTimeOffset);
        }
    }
}
