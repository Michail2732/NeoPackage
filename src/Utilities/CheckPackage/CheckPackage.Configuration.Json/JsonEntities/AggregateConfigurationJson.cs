using Newtonsoft.Json;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Configuration.Json.Entities
{
    public class AggregateConfigurationJson 
    {        
        [JsonProperty("rules")]
        [JsonRequired]
        public List<ConfigurationRulesJson>? Rules { get; set; }
        [JsonProperty("parameters")]
        [JsonRequired]
        public List<ConfigurationParametersJson>? Parameters { get; set; }
        [JsonProperty("rules_hash")]
        [JsonRequired]
        public Dictionary<string, string>? RulesHashes { get; set; }
        [JsonProperty("parameters_hash")]
        [JsonRequired]
        public Dictionary<string, string>? ParametersHashes { get; set; }

    }
}
