using System.Data;
using Microsoft.SqlServer.Server;
using System.Reflection;
using EasyTVP.Types.Interfaces;
using System;

namespace EasyTVP.Types
{
    public class SingleSqlType : ISqlType
    {
        public SqlMetaData GetMetadata(PropertyInfo property)
        {
            return new SqlMetaData(property.Name, SqlDbType.Real);
        }
    }
}
