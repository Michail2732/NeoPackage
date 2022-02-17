using CheckPackage.Configuration.Json.Entities;
using Newtonsoft.Json;

namespace CheckPackage.Base.Configuration
{
    public class ParameterSubstringExtractJson: ParameterExtractJson
    {        
        [JsonProperty("regex_template")]
        [JsonRequired]
        public string? RegexTemplate { get; set; }

        [JsonProperty("group_name")]
        [JsonRequired]
        public string? GroupName { get; set; }
    }
}
