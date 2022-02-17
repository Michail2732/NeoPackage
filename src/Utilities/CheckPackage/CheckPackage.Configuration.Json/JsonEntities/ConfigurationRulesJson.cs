using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Configuration.Json.Entities
{
    public class ConfigurationRulesJson
    {
        [JsonProperty("info")]
        [JsonRequired]
        public ConfigurationInfoJson? Info { get;  set; }
        [JsonProperty("checks")]
        [JsonRequired]
        public AggregateChecksJson? Check { get;  set; }
        [JsonProperty("buildings")]
        [JsonRequired]
        public AggregateBuildingJson? Building { get;  set; }
        [JsonProperty("outputs")]
        [JsonRequired]
        public AggregateOutputJson? Output { get;  set; }
        [JsonProperty("resources")]
        [JsonRequired]
        public List<ResourceJson>? Resources { get;  set; }
    }
}
