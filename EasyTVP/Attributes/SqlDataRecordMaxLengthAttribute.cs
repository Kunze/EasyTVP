using System;
using System.Reflection;

namespace EasyTVP.Attributes
{
    public class SqlDataRecordMaxLengthAttribute: Attribute
    {
        public long MaxLength { get; private set; }

        public SqlDataRecordMaxLengthAttribute(long maxLength)
        {
            if(maxLength == 0)
            {
                throw new ArgumentException("MaxLength não pode ser 0.");
            }

            MaxLength = maxLength;
        }

        public static long? GetSqlMaxLengthAttribute(PropertyInfo property)
        {
            return property.GetCustomAttribute<SqlDataRecordMaxLengthAttribute>()?.MaxLength;
        }
    }
}
