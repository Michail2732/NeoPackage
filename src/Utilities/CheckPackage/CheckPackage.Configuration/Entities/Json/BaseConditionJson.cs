using CheckPackage.Core.Checks;
using Newtonsoft.Json;

namespace CheckPackage.Configuration.Entities
{
    public class BaseConditionJson
    {
        [JsonProperty("condition_id")]
        [JsonRequired]
        public string? ConditionId { get; set; }

        [JsonProperty("parameter_id")]
        [JsonRequired]
        public string? ParameterId { get; set; }

        [JsonProperty("logic")]
        [JsonRequired]
        public LogicalOperator Logic { get; set; }        

        [JsonProperty("inverse")]        
        public bool Inverse { get; set; }

        [JsonProperty("recurse")]        
        public bool Recurse { get; set; }
    }
}
