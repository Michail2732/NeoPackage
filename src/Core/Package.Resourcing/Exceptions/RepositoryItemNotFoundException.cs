using System;
using System.Collections.Generic;
using System.Text;

namespace Package.Repository.Exceptions
{

    [Serializable]
    public class RepositoryItemNotFoundException : Exception
    {
        public RepositoryItemNotFoundException() { }
        public RepositoryItemNotFoundException(string message) : base(message) { }
        public RepositoryItemNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected RepositoryItemNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
