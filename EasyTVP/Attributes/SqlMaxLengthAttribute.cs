using System;

namespace EasyTVP.Attributes
{
    public class SqlMaxLengthAttribute: Attribute
    {
        public long MaxLength { get; private set; }

        public SqlMaxLengthAttribute(long maxLength)
        {
            if(maxLength == 0)
            {
                throw new ArgumentException("MaxLength não pode ser 0.");
            }

            MaxLength = maxLength;
        }
    }
}
