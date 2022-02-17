using CheckPackage.Configuration.Json.Entities;
using Newtonsoft.Json;

namespace CheckPackage.Base.Configuration
{
    public class ParameterStaticValueExtractJson: ParameterExtractJson
    {
        [JsonProperty("parameter_id")]
        [JsonRequired]
        public string? ParameterId { get; set; }

        [JsonProperty("parameter_value")]
        [JsonRequired]
        public string? ParameterValue { get; set; }
    }
}
