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
    public static class TVP
    {
        private static List<ISqlType> types = new List<ISqlType>
        {
            new StringSqlType(),
            new Int16SqlType(),
            new Int32SqlType(),
            new Int64SqlType(),
            new BooleanSqlType(),
            new CharSqlType(),
            new DateTimeSqlType(),
            new DecimalSqlType(),
            new DoubleSqlType(),
            new SingleSqlType(),
            new TimeSpanSqlType(),
            new EnumSqlType(),
            new DateTimeOffSetSqlType(),
            new ByteSqlType()
        };

        public static IEnumerable<SqlDataRecord> Map<T>(this IEnumerable<T> objects) where T: class
        {
            var type = typeof(T);
            var properties = type.GetRuntimeProperties();
            var orderedProperties = properties.OrderBy(x => x.GetCustomAttribute<SqlDataRecordOrderAttribute>()?.Index).ToList();

            return GetRecords(objects, orderedProperties);
        }

        private static SqlMetaData[] GetMetadata(List<PropertyInfo> properties)
        {
            var metadatas = new SqlMetaData[properties.Count];

            for (int propertyIndex = 0; propertyIndex < properties.Count; propertyIndex++)
            {
                var property = properties[propertyIndex];

                foreach (var sqlType in types)
                {
                    if (sqlType.TryGet(property, out SqlMetaData metadata))
                    {
                        metadatas[propertyIndex] = metadata;
                        break;
                    }
                }

                if(metadatas[propertyIndex] == null)
                {
                    throw new InvalidOperationException($"Does not exist a SqlDbType for type { property.PropertyType.ToString() } of property {property.Name }.");
                }
            }

            return metadatas;
        }

        private static IEnumerable<SqlDataRecord> GetRecords<T>(IEnumerable<T> objects, List<PropertyInfo> properties)
        {
            var metadatas = GetMetadata(properties);

            foreach (var @object in objects)
            {
                var record = new SqlDataRecord(metadatas);

                for (int propertyIndex = 0; propertyIndex < properties.Count; propertyIndex++)
                {
                    var property = properties[propertyIndex];

                    if (SqlTypeCache.TryGet(property, out ISqlType sqlCachedType))
                    {
                        sqlCachedType.TrySet(property, @object, record, propertyIndex);

                        continue;
                    }

                    for (int sqlIndex = 0; sqlIndex < types.Count; sqlIndex++)
                    {
                        var sqlType = types[sqlIndex];

                        if (sqlType.TrySet(property, @object, record, propertyIndex))
                        {
                            SqlTypeCache.TryAdd(property, sqlType);
                            break;
                        }
                    }
                }

                yield return record;
            }
        }
    }

    internal static class SqlTypeCache
    {
        private static Dictionary<PropertyInfo, ISqlType> Cache = new Dictionary<PropertyInfo, ISqlType>();

        internal static bool TryGet(PropertyInfo property, out ISqlType sqlType)
        {
            return Cache.TryGetValue(property, out sqlType);
        }

        internal static bool TryAdd(PropertyInfo property, ISqlType sqlType)
        {
            if (!Cache.ContainsKey(property))
            {
                Cache.Add(property, sqlType);
                return true;
            }

            return false;
        }
    }
}
