using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Base.Configuration
{
    public class ParameterTemplateJson
    {
        [JsonProperty("description")]
        [JsonRequired]
        public string? Description { get; set; }
        [JsonProperty("regex_pattern")]
        [JsonRequired]
        public string? RegexPattern { get; set; }

    }
}
