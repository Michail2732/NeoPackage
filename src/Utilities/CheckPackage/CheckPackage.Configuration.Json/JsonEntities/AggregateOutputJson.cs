using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Configuration.Json.Entities
{
    public class AggregateOutputJson
    {
        [JsonProperty("parameters")]
        [JsonRequired]
        public List<ParameterOutputRuleJson>? ParameterOutputRules { get; set; }
        [JsonProperty("entities")]
        [JsonRequired]
        public List<EntityOutputRuleJson>? EntitiesOutputRules { get; set; }
        [JsonProperty("package")]
        [JsonRequired]
        public List<PackageOutputRuleJson>? PackageOutputRules { get; set; }
    }
}
