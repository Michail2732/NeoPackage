using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using Package.Configuration.Services;

namespace Package.Tests.Unit.Configuration
{
    public class StubJsonConfigurationBinder : IJsonConfigurationBinder<string>
    {
        public string Get(JObject configurationRoot)
        {
            return string.Empty;
        }

        public void Set(string item, JObject configuration)
        {
            
        }
    }
}
