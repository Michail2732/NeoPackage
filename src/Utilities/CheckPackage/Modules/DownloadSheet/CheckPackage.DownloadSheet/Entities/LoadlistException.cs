using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.DownloadSheet.Mapping
{

    [Serializable]
    public class LoadlistException : Exception
    {
        public LoadlistException() { }
        public LoadlistException(string message) : base($"Error in loadlist module: {message}") { }
        public LoadlistException(string message, Exception inner) : base($"Error in loadlist module: {message}", inner) { }
        protected LoadlistException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
