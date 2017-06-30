using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using EasyTVP.Types;
using EasyTVP.Types.Interfaces;
using EasyTVP.Attributes;

namespace EasyTVP
{
    internal class PropertyMetadata
    {
        private static Dictionary<Type, ISqlType> sqlTypes = new Dictionary<Type, ISqlType>
        {
            [typeof(string)] = new StringSqlType(),
            [typeof(Int16)] = new Int16SqlType(),
            [typeof(Int32)] = new Int32SqlType(),
            [typeof(Int64)] = new Int64SqlType(),
            [typeof(bool)] = new BooleanSqlType(),
            [typeof(char)] = new CharSqlType(),
            [typeof(DateTime)] = new DateTimeSqlType(),
            [typeof(decimal)] = new DecimalSqlType(),
            [typeof(double)] = new DoubleSqlType(),
            [typeof(Single)] = new SingleSqlType(),
            [typeof(TimeSpan)] = new TimeSpanSqlType(),
            [typeof(DateTimeOffset)] = new DateTimeOffSetSqlType(),
            [typeof(byte)] = new ByteSqlType()
        };

        public readonly Type Type;
        private readonly PropertyInfo Property;
        private readonly ISqlType SqlType;

        public PropertyMetadata(PropertyInfo property)
        {
            Type = GetUnderlyingType(property);
            Property = property;

            if(!sqlTypes.ContainsKey(Type))
            {
                throw new ArgumentException($"Does not exist a {typeof(ISqlType).FullName} for {property.PropertyType} (property: {property.Name})");
            }

            SqlType = sqlTypes[Type];
        }

        public SqlMetaData GetMetadata()
        {
            return SqlType.GetMetadata(Property);
        }

        public object GetValue(object @object)
        {
            return Property.GetValue(@object);
        }

        private static Type GetUnderlyingType(PropertyInfo propertyInfo)
        {
            var type = propertyInfo.PropertyType;
            type = Nullable.GetUnderlyingType(type) ?? type;

            if (type.GetTypeInfo().IsEnum)
            {
                type = Enum.GetUnderlyingType(type);
            }

            return type;
        }
    }

    public static class TVP
    {
        public static IEnumerable<SqlDataRecord> Map<T>(this IEnumerable<T> objects) where T: class
        {
            var type = typeof(T);
            var properties = type.GetRuntimeProperties();
            var orderedProperties = properties.OrderBy(x => x.GetCustomAttribute<SqlDataRecordOrderAttribute>()?.Index);
            var propertiesMetadata = orderedProperties.Select(x => new PropertyMetadata(x)).ToArray();
            var metadatas = propertiesMetadata.Select(x => x.GetMetadata()).ToArray();

            foreach (var @object in objects)
            {
                var record = new SqlDataRecord(metadatas);

                for (int propertyIndex = 0; propertyIndex < propertiesMetadata.Length; propertyIndex++)
                {
                    var propertyMetadata = propertiesMetadata[propertyIndex];

                    var value = propertyMetadata.GetValue(@object);
                    record.SetValue(propertyIndex, value);
                }

                yield return record;
            }
        }
    }
}
