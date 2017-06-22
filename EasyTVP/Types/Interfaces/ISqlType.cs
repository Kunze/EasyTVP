using Microsoft.SqlServer.Server;
using System.Reflection;

namespace EasyTVP.Types.Interfaces
{
    internal interface ISqlType
    {
        bool TrySet(PropertyInfo propertyInfo, object @object, SqlDataRecord record, int index);
        bool TryGet(PropertyInfo propertyInfo, out SqlMetaData metadata);
    }
}
