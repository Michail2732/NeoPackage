using System;
using System.Runtime.Serialization;

namespace Package.Domain.Exceptions
{

    [Serializable]
    public class AlreadyExistException : Exception
    {
        public AlreadyExistException()
        {
        }

        public AlreadyExistException(string message) : base(message)
        {
        }

        public AlreadyExistException(string message, Exception inner) : base(message, inner)
        {
        }

        protected AlreadyExistException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}