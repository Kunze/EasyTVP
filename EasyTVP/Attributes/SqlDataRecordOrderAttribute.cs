using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTVP.Attributes
{
    public class SqlDataRecordOrderAttribute: Attribute
    {
        public readonly int Index;

        public SqlDataRecordOrderAttribute(int index)
        {
            Index = index;
        }
    }
}
