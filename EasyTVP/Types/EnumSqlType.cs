using System.Data;
using Microsoft.SqlServer.Server;
using System.Reflection;
using EasyTVP.Types.Interfaces;
using System;
using System.Collections.Generic;

namespace EasyTVP.Types
{
    public class EnumSqlType : ISqlType
    {
        //https://www.sqlservercentral.com/Forums/Topic366659-8-1.aspx
        private static Dictionary<Type, SqlDbType> enumTypes = new Dictionary<Type, SqlDbType>
        {
            { typeof(byte), SqlDbType.TinyInt },
            { typeof(sbyte), SqlDbType.SmallInt },
            { typeof(short), SqlDbType.SmallInt },
            { typeof(ushort), SqlDbType.Int },
            { typeof(int), SqlDbType.Int },
            { typeof(uint), SqlDbType.BigInt },
            { typeof(long), SqlDbType.BigInt },
            { typeof(ulong), SqlDbType.Decimal }
        };

        public bool TryGet(PropertyInfo propertyInfo, out SqlMetaData metadata)
        {
            metadata = null;
            var type = GetUnderlyingType(propertyInfo);

            if (type.GetTypeInfo().IsEnum)
            {
                var enumType = Enum.GetUnderlyingType(type);

                metadata = new SqlMetaData(propertyInfo.Name, enumTypes[enumType]);

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
                    record.SetValue(index, value);
                }

                return true;
            }

            return false;
        }

        private static Type GetUnderlyingType(PropertyInfo propertyInfo)
        {
            var type = propertyInfo.PropertyType;
            type = Nullable.GetUnderlyingType(type) ?? type;

            return type;
        }
    }
}
