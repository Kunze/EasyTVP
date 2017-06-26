using System.Data;
using Microsoft.SqlServer.Server;
using System.Reflection;
using EasyTVP.Types.Interfaces;
using System;

namespace EasyTVP.Types
{
    internal class EnumSqlType : ISqlType
    {
        public bool TryGet(PropertyInfo propertyInfo, out SqlMetaData metadata)
        {
            metadata = null;

            if (GetUnderlyingType(propertyInfo).GetTypeInfo().IsEnum)
            {
                metadata = new SqlMetaData(propertyInfo.Name, SqlDbType.Int);

                return true;
            }

            return false;
        }

        public bool TrySet(PropertyInfo propertyInfo, object @object, SqlDataRecord record, int index)
        {
            if (GetUnderlyingType(propertyInfo).GetTypeInfo().IsEnum)
            {
                var value = propertyInfo.GetValue(@object);

                if (value == null)
                {
                    record.SetDBNull(index);
                }
                else
                {
                    record.SetInt32(index, (int)value);
                }

                return true;
            }

            return false;
        }

        private Type GetUnderlyingType(PropertyInfo propertyInfo)
        {
            var type = propertyInfo.PropertyType;
            type = Nullable.GetUnderlyingType(type) ?? type;

            return type;
        }
    }
}
