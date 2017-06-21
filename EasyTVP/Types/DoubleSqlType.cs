﻿using System.Data;
using Microsoft.SqlServer.Server;
using System;
using System.Reflection;

namespace EasyTVP.Types
{
    public class DoubleSqlType : NullableSqlType<double>
    {
        protected override SqlMetaData GetSqlMetaData(PropertyInfo property)
        {
            return new SqlMetaData(property.Name, SqlDbType.Float);
        }

        protected override void SetRecord(SqlDataRecord record, int index, object value)
        {
            record.SetDouble(index, (double)value);
        }
    }
}
