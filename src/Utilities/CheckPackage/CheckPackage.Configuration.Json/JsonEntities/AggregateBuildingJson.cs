using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Configuration.Json.Entities
{
    public class AggregateBuildingJson
    {
        [JsonProperty("entities")]
        [JsonRequired]
        public List<EntityBuildRuleJson>? EntityBuildRules { get; set; }
    }
}
