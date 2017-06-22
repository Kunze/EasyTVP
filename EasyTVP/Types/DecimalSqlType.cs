using System.Data;
using Microsoft.SqlServer.Server;
using System.Reflection;

namespace EasyTVP.Types
{
    internal class DecimalSqlType : NullableSqlType<decimal>
    {
        protected override SqlMetaData GetSqlMetaData(PropertyInfo property)
        {
            //<TODO> usar attributo para pegar decimal, money, numeric ou smallmoney
            return new SqlMetaData(property.Name, GetAttributeSqlDbType(property) ?? SqlDbType.Decimal);
        }

        protected override void SetRecord(SqlDataRecord record, int index, object value)
        {
            record.SetDecimal(index, (decimal)value);
        }
    }
}
