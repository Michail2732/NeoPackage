using CheckPackage.Configuration.Entities;
using CheckPackage.Core.Validation;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CheckPackage.Base.Configuration
{
    public class ParameterCheckRuleJson
    {
        [JsonProperty("parameter_id")]
        [JsonRequired]
        public string? ParameterId { get; set; }
        [JsonProperty("critical")]
        public CriticalType Critical { get; set; }
        [JsonProperty("is_custom_parameter")]
        public bool IsCustomParameter { get; set; }
        [JsonProperty("checks")]
        [JsonRequired]
        public List<BaseCheckJson>? Checks { get; set; }
    }
}
