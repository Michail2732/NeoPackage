using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Configuration.Json.Entities
{
    public class ConfigurationParametersJson
    {
        [JsonProperty("info")]
        [JsonRequired]
        public ConfigurationInfoJson? Info { get; private set; }        
        [JsonProperty("configuration_version")]
        [JsonRequired]
        public string? ConfigurationRulesId { get; private set; }
        [JsonProperty("parameters")]
        [JsonRequired]
        public Dictionary<string, string>? Parameters { get; private set; }
        [JsonProperty("set_parameters")]
        [JsonRequired]
        public Dictionary<string, List<string>>? SetParameters { get; private set; }

    }
}
