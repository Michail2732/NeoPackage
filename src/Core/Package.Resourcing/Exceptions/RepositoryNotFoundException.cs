using System;
using System.Collections.Generic;
using System.Text;

namespace Package.Repository.Exceptions
{

    [Serializable]
    public class RepositoryNotFoundException : Exception
    {
        public RepositoryNotFoundException() { }
        public RepositoryNotFoundException(string message) : base(message) { }
        public RepositoryNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected RepositoryNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
