using CheckPackage.Configuration.Json.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Package.Configuration.Services;
using System.Collections.Generic;

namespace CheckPackage.Configuration.ConfigurationBinders
{
    public class PackageConfigurationJsonBinder : IJsonConfigurationBinder<PackageConfigurationJson>
    {
        public PackageConfigurationJson Get(JObject configurationRoot)
        {
            var properrty = configurationRoot.Property("package_settings");
            PackageConfigurationJson? result = null;
            if (properrty != null)
                result = JsonConvert.DeserializeObject<PackageConfigurationJson>(properrty.Value.ToString());
            return result ?? new PackageConfigurationJson();
        }

        public void Set(PackageConfigurationJson item, JObject configurationRoot)
        {            
            string jsonRaw = JsonConvert.SerializeObject(item, Formatting.None);
            configurationRoot["package_settings"] = JObject.Parse(jsonRaw);
        }
    }
}
