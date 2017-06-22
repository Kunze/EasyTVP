using Microsoft.SqlServer.Server;
using EasyTVP.Attributes;
using System;
using System.Data;
using System.Reflection;
using EasyTVP.Types.Interfaces;

namespace EasyTVP.Types
{
    internal abstract class NullableSqlType<T> : ISqlType
    {
        protected abstract SqlMetaData GetSqlMetaData(PropertyInfo property);
        protected abstract void SetRecord(SqlDataRecord record, int index, object value);

        public bool TryGet(PropertyInfo propertyInfo, out SqlMetaData metadata)
        {
            metadata = null;

            if (GetUnderlyingType(propertyInfo) == typeof(T))
            {
                metadata = GetSqlMetaData(propertyInfo);

                return true;
            }

            return false;
        }

        public bool TrySet(PropertyInfo propertyInfo, object @object, SqlDataRecord record, int index)
        {
            if (GetUnderlyingType(propertyInfo) == typeof(T))
            {
                var value = propertyInfo.GetValue(@object);

                if (value == null)
                {
                    record.SetDBNull(index);
                }
                else
                {
                    SetRecord(record, index, value);
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

        protected SqlDbType? GetAttributeSqlDbType(PropertyInfo property)
        {
            var sqlDbType = property.GetCustomAttribute<SqlDataTypeAttribute>();

            return sqlDbType?.Type ?? null;
        }
    }
}
