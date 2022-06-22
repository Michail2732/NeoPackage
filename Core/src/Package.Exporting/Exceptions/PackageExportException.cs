using System;
using System.Runtime.Serialization;

namespace Package.Exporting.Exceptions
{
    [Serializable]
    public class PackageExportException : Exception
    {
        public PackageExportException()
        {
        }

        public PackageExportException(string message) : base(message)
        {
        }

        public PackageExportException(string message, Exception inner) : base(message, inner)
        {
        }

        protected PackageExportException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}