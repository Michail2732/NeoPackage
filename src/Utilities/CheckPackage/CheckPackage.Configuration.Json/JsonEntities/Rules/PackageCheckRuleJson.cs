using Newtonsoft.Json;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Configuration.Json.Entities
{
    public class PackageCheckRuleJson
    {
        [JsonProperty("state")]
        [JsonRequired]
        public Critical State { get; set; }
        [JsonProperty("conditions")]
        [JsonRequired]
        public IReadOnlyList<PackageConditionJson> Conditions { get; set; }
        [JsonProperty("checks")]
        [JsonRequired]
        public IReadOnlyList<PackageCheckJson> Checks { get; set; }

    }
}
