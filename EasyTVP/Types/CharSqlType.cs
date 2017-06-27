using System.Data;
using System.Reflection;
using Microsoft.SqlServer.Server;

namespace EasyTVP.Types
{
    public class CharSqlType : NullableSqlType<char>
    {
        //não existe Char, somente Char[]
        protected override SqlMetaData GetSqlMetaData(PropertyInfo property)
        {
            return new SqlMetaData(property.Name, SqlDbType.VarChar, 1);
        }
    }
}
