using CheckPackage.Core.Checks;
using Newtonsoft.Json;

namespace CheckPackage.Configuration.Entities
{
    public class BaseCheckJson
    {
        [JsonProperty("check_id")]
        [JsonRequired]
        public string? CheckId { get; set; }
        [JsonProperty("parameter_id")]
        [JsonRequired]
        public string? ParameterId { get; set; }
        [JsonProperty("logic")]
        [JsonRequired]
        public LogicalOperator Logic { get; set; }
        [JsonProperty("message")]
        [JsonRequired]
        public string? Message { get; set; }
        [JsonProperty("inverse")]
        public bool Inverse { get; set; }        
    }
}
