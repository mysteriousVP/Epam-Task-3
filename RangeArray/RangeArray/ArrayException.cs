using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RangeArray
{
    [Serializable]
    public class ArrayException : Exception
    {
        public ArrayException()
        {

        }

        public ArrayException(string message) : base(message)
        {

        }

        public ArrayException(string message, Exception innerException) : base(message, innerException)
        {

        }

        public ArrayException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
