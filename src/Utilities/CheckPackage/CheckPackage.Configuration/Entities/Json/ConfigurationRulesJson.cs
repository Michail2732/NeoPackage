using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Configuration.Entities
{
    public class ConfigurationRulesJson
    {
        [JsonProperty("info")]
        [JsonRequired]
        public ConfigurationInfoJson? Info { get; private set; }
        [JsonProperty("dictionaries")]
        [JsonRequired]
        public Dictionary<string, JToken>? Dictionary { get; private set; }
        [JsonProperty("rules")]
        [JsonRequired]
        public Dictionary<string, JToken>? Rules { get; private set; }
        [JsonProperty("checks")]
        [JsonRequired]
        public Dictionary<string, JToken>? Checks { get; private set; }
    }
}
