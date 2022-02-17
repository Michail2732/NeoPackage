using CheckPackage.Configuration.Json.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CheckPackage.Base.Configuration
{
    public class ParameterRegexExtractJson: ParameterExtractJson
    {
        [JsonProperty("regex_template_id")]
        [JsonRequired]
        public string? RegexTemplateId { get; set; }        
    }
}
