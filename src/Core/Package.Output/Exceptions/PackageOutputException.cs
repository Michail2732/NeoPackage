using System;
using System.Collections.Generic;
using System.Text;

namespace Package.Output.Exceptions
{

    [Serializable]
    public class PackageOutputException : Exception
    {
        public PackageOutputException() { }
        public PackageOutputException(string message) : base(message) { }
        public PackageOutputException(string message, Exception inner) : base(message, inner) { }
        protected PackageOutputException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
