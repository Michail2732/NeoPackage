using CheckPackage.Configuration.Json.Entities;
using Newtonsoft.Json;

namespace CheckPackage.DownloadSheet.Configuration
{
    public class LoadlistExtractJson: ParameterExtractJson
    {
        [JsonProperty("parameter_id")]
        [JsonRequired]
        public string? ParameterId { get; set; }
    }
}
