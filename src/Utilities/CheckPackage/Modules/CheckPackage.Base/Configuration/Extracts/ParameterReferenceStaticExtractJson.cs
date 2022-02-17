using CheckPackage.Configuration.Json.Entities;
using Newtonsoft.Json;

namespace CheckPackage.Base.Configuration
{
    public class ParameterReferenceStaticExtractJson: ParameterExtractJson
    {
        [JsonProperty("parameter_id")]
        [JsonRequired]
        public string? ParameterId { get; set; }

        [JsonProperty("static_parameter_id")]
        [JsonRequired]
        public string? StaticParameterId { get; set; }
    }
}
