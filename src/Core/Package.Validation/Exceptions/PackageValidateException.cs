using System;
using System.Collections.Generic;
using System.Text;

namespace Package.Validation.Exceptions
{

    [Serializable]
    public class PackageValidateException : Exception
    {
        public PackageValidateException() { }
        public PackageValidateException(string message) : base(message) { }
        public PackageValidateException(string message, Exception inner) : base(message, inner) { }
        protected PackageValidateException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
