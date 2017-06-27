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
        
        public static IEnumerable<SqlDataRecord> Map<T>(this IEnumerable<T> objects)
        {
            var type = typeof(T);
            var properties = type.GetRuntimeProperties().ToList();
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
            }

            return metadatas;
        }

        private static List<SqlDataRecord> GetRecords<T>(IEnumerable<T> objects, List<PropertyInfo> properties)
        {
            var records = new List<SqlDataRecord>();
            var metadatas = GetMetadata(properties);

            foreach (var @object in objects)
            {
                var record = new SqlDataRecord(metadatas);

                for (int propertyIndex = 0; propertyIndex < properties.Count; propertyIndex++)
                {
                    var property = properties[propertyIndex];

                    for (int sqlIndex = 0; sqlIndex < types.Count; sqlIndex++)
                    {
                        var sqlType = types[sqlIndex];

                        if (sqlType.TrySet(property, @object, record, propertyIndex))
                        {
                            break;
                        }

                        if (sqlIndex == types.Count)
                        {
                            throw new InvalidOperationException($"Não existe um sqlType registrado para { property.GetType().Name }");
                        }
                    }
                }

                records.Add(record);
            }

            return records;
        }
    }
}
