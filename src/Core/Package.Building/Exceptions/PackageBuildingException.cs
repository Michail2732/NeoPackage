using System;
using System.Collections.Generic;
using System.Text;

namespace Package.Building.Extensions
{

    [Serializable]
    public class PackageBuildingException : Exception
    {
        public PackageBuildingException() { }
        public PackageBuildingException(string message) : base(message) { }
        public PackageBuildingException(string message, Exception inner) : base(message, inner) { }
        protected PackageBuildingException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
