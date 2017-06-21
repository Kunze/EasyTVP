using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
