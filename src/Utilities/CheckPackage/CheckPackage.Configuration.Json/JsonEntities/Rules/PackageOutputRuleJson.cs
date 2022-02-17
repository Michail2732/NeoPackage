using Newtonsoft.Json;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Configuration.Json.Entities
{
    public class PackageOutputRuleJson
    {
        [JsonProperty("state")]
        [JsonRequired]
        public Critical State { get; set; }
        [JsonProperty("conditions")]
        [JsonRequired]
        public List<PackageConditionJson>? Conditions { get; set; }
        [JsonProperty("output")]
        [JsonRequired]
        public PackageOutputJson? Output { get; set; }
    }
}
