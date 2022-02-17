using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Configuration.Json.Entities
{
    public class AggregateChecksJson
    {
        [JsonProperty("parameters")]
        [JsonRequired]
        public List<ParameterCheckRuleJson>? ParameterCheckRules { get; set; }
        [JsonProperty("entities")]
        [JsonRequired]
        public List<EntityCheckRuleJson>? EntityCheckRules { get; set; }
        [JsonProperty("package")]
        [JsonRequired]
        public List<PackageCheckRuleJson>? PackageCheckRules { get; set; }
    }
}
