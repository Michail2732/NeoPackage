using System;
using System.Runtime.Serialization;

namespace Package.Checking.Exceptions
{
    [Serializable]
    public class PackageCheckingException : Exception
    {
        public PackageCheckingException()
        {
        }

        public PackageCheckingException(string message) : base(message)
        {
        }

        public PackageCheckingException(string message, Exception inner) : base(message, inner)
        {
        }

        protected PackageCheckingException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }   
}