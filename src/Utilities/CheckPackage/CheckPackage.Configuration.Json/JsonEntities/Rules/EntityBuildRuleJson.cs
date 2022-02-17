using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Configuration.Json.Entities
{
    public class EntityBuildRuleJson
    {
        [JsonProperty("entity_level")]
        [JsonRequired]
        public uint EntityLevel { get; set; }
        [JsonProperty("priority")]
        [JsonRequired]
        public uint Priority { get; set; }
        [JsonProperty("group_by")]
        [JsonRequired]
        public string GroupBy { get; set; }
        [JsonProperty("conditions")]
        [JsonRequired]
        public List<EntityConditionJson> Conditions { get; set; }
        [JsonProperty("parameter_rules")]
        [JsonRequired]
        public List<ParameterBuildRuleJson> ParameterRules { get; set; }
    }
}
