using CheckPackage.Configuration.Entities;
using CheckPackage.Core.Validation;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CheckPackage.Base.Configuration
{
    public class EntityCheckRuleJson
    {        
        [JsonProperty("critical")]
        public CriticalType Critical { get; set; }        
        [JsonProperty("conditions")]
        public List<BaseConditionJson>? Conditions { get; set; }
        [JsonProperty("checks")]
        [JsonRequired]
        public List<BaseCheckJson>? Checks { get; set; }
    }
}
