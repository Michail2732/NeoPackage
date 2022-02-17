using CheckPackage.Configuration.JsonEntities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Package.Configuration.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Configuration.ConfigurationBinders
{
    public class RepositoryConfigurationJsonBinder : IJsonConfigurationBinder<RepositoryConfigurationJson>
    {
        public RepositoryConfigurationJson Get(JObject configurationRoot)
        {
            var properrty = configurationRoot.Property("repository_settings");
            RepositoryConfigurationJson? result = null;
            if (properrty != null)
                result = JsonConvert.DeserializeObject<RepositoryConfigurationJson>(properrty.Value.ToString());
            return result ?? new RepositoryConfigurationJson();
        }

        public void Set(RepositoryConfigurationJson item, JObject configurationRoot)
        {            
            string jsonRaw = JsonConvert.SerializeObject(item, Formatting.None);
            configurationRoot["repository_settings"] = JObject.Parse(jsonRaw);
        }
    }
}
