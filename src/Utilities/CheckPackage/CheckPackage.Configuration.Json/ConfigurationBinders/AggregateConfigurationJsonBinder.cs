using CheckPackage.Configuration.Json.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Package.Configuration.Services;
using System.Collections.Generic;

namespace CheckPackage.Configuration.ConfigurationBinders
{
    public class AggregateConfigurationJsonBinder : IJsonConfigurationBinder<AggregateConfigurationJson>
    {
        public AggregateConfigurationJson Get(JObject configurationRoot)
        {
            var properrty = configurationRoot.Property("rules_parameters_settings");
            AggregateConfigurationJson? result = null;
            if (properrty != null)
                result = JsonConvert.DeserializeObject<AggregateConfigurationJson>(properrty.Value.ToString(), new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Auto });            
            return result ?? new AggregateConfigurationJson
            {
                Parameters = new List<ConfigurationParametersJson>(),
                Rules = new List<ConfigurationRulesJson>(),
                ParametersHashes = new Dictionary<string, string>(),
                RulesHashes = new Dictionary<string, string>()
            };
            ;
        }

        public void Set(AggregateConfigurationJson item, JObject configurationRoot)
        {            
            string jsonRaw = JsonConvert.SerializeObject(item, Formatting.None, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects });
            configurationRoot["rules_parameters_settings"] = JObject.Parse(jsonRaw);
        }
    }
}
