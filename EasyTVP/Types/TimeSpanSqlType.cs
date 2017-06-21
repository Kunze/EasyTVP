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
    public class TimeSpanSqlType : NullableSqlType<TimeSpan>
    {
        protected override SqlMetaData GetSqlMetaData(PropertyInfo property)
        {
            return new SqlMetaData(property.Name, SqlDbType.Time);
        }

        protected override void SetRecord(SqlDataRecord record, int index, object value)
        {
            record.SetTimeSpan(index, (TimeSpan)value);
        }
    }
}
