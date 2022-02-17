using Newtonsoft.Json;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Configuration.Json.Entities
{
    public class EntityOutputRuleJson
    {
        [JsonProperty("state")]
        [JsonRequired]
        public Critical State { get; set; }
        [JsonProperty("conditions")]
        [JsonRequired]
        public List<EntityConditionJson> Conditions { get; set; }
        [JsonProperty("output")]
        [JsonRequired]
        public EntityOutputJson Output { get; set; }
    }
}
