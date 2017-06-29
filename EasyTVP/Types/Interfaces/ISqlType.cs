using Microsoft.SqlServer.Server;
using System.Reflection;

namespace EasyTVP.Types.Interfaces
{
    internal interface ISqlType
    {
        SqlMetaData GetMetadata(PropertyInfo property);
    }
}
