using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;
using System.Data;

namespace EasyTVP.Types
{
    public class ByteSqlType : NullableSqlType<Byte>
    {
        protected override SqlMetaData GetSqlMetaData(PropertyInfo property)
        {
            return new SqlMetaData(property.Name, SqlDbType.TinyInt);
        }

        protected override void SetRecord(SqlDataRecord record, int index, object value)
        {
            record.SetByte(index, (byte)value);
        }
    }
}
