using System;
using System.Data;

namespace EasyTVP.Attributes
{
    public class SqlDataTypeAttribute: Attribute
    {
        public SqlDbType Type { get; private set; }

        public SqlDataTypeAttribute(SqlDbType type)
        {
            Type = type;
        }
    }
}
