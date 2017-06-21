using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using EasyTVP.Types;

namespace EasyTVP
{
    public interface ISqlType
    {
        bool TrySet(PropertyInfo propertyInfo, object @object, SqlDataRecord record, int index);
        bool TryGet(PropertyInfo propertyInfo, out SqlMetaData metadata);
    }

    public class TVP
    {
        private static List<ISqlType> types = new List<ISqlType>
        {
            new StringSqlType(),
            new Int16SqlType(),
            new Int32SqlType(),
            new Int64SqlType(),
            new BooleanSqlType(),
            new ByteSqlType(),
            new CharSqlType(),
            new DateTimeOffSetSqlType(),
            new DateTimeSqlType(),
            new DecimalSqlType(),
            new DoubleSqlType(),
            new SingleSqlType(),
            new TimeSpanSqlType()
        };

        public static SqlDataRecord[] Map<T>(IEnumerable<T> objects)
        {
            var list = objects.ToList();
            var type = typeof(T);
            var properties = type.GetRuntimeProperties().ToList();
            var metadatas = new SqlMetaData[properties.Count];

            SetMetadata(properties, metadatas);

            return GetRecords(list, properties, metadatas);
        }

        private static void SetMetadata(List<PropertyInfo> properties, SqlMetaData[] metadatas)
        {
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
        }

        private static SqlDataRecord[] GetRecords<T>(List<T> list, List<PropertyInfo> properties, SqlMetaData[] metadatas)
        {
            var records = new SqlDataRecord[list.Count];

            for (int objectIndex = 0; objectIndex < list.Count; objectIndex++)
            {
                var @object = list[objectIndex];
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

                records[objectIndex] = record;
            }

            return records;
        }
    }
}
