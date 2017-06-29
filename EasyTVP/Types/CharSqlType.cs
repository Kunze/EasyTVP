using System.Data;
using System.Reflection;
using Microsoft.SqlServer.Server;
using EasyTVP.Types.Interfaces;
using System;

namespace EasyTVP.Types
{
    public class CharSqlType : ISqlType
    {
        public SqlMetaData GetMetadata(PropertyInfo property)
        {
            return new SqlMetaData(property.Name, SqlDbType.VarChar, 1);
        }
    }
}
