using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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

        protected override void SetRecord(SqlDataRecord record, int index, object value)
        {
            record.SetString(index, value.ToString());
        }
    }
}
