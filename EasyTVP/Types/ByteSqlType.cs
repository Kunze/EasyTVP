﻿using System;
using System.Reflection;
using Microsoft.SqlServer.Server;
using System.Data;
using EasyTVP.Types.Interfaces;

namespace EasyTVP.Types
{
    public class ByteSqlType : ISqlType
    {
        public SqlMetaData GetMetadata(PropertyInfo property)
        {
            return new SqlMetaData(property.Name, SqlDbType.TinyInt);
        }
    }
}
