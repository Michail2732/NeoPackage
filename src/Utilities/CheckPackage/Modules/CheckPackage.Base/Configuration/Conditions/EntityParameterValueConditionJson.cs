using CheckPackage.Configuration.Json.Entities;
using CheckPackage.Core.Entities;
using Newtonsoft.Json;


namespace CheckPackage.Base.Configuration
{
    public class EntityParameterValueConditionJson: EntityConditionJson
    {
        [JsonProperty("parameter_id")]
        [JsonRequired]
        public string? ParameterId { get; set; }

        [JsonProperty("value")]
        [JsonRequired]
        public string? Value { get; set; }

        [JsonProperty("operator")]
        [JsonRequired]
        public ValueAction Operator { get; set; }

    }
}
