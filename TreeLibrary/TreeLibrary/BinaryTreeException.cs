using System;
using System.Runtime.Serialization;

namespace TreeLibrary
{
    public class BinaryTreeException : Exception
    {
        public BinaryTreeException()
        {

        }

        public BinaryTreeException(string message) : base(message)
        {

        }

        public BinaryTreeException(string message, Exception innerException) : base(message, innerException)
        {

        }

        public BinaryTreeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
