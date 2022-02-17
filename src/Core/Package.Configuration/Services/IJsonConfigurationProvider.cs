using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Package.Configuration.Services
{
    public interface IJsonConfigurationProvider
    {
        JObject Get();
        void Set(JObject configuration);
    }
}
