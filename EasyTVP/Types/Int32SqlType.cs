using System.Data;
using System.Reflection;
using Microsoft.SqlServer.Server;

namespace EasyTVP.Types
{
    public class Int32SqlType : NullableSqlType<int>
    {
        protected override SqlMetaData GetSqlMetaData(PropertyInfo property)
        {
            return new SqlMetaData(property.Name, SqlDbType.Int);
        }
    }
}
