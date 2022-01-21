using CheckPackage.Configuration.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Base.Configuration
{
    public class SubstringExtractJson: BaseExtractJson
    {
        [JsonProperty("regex_template")]
        [JsonRequired]
        public string? RegexTemplate { get; set; }
        [JsonProperty("group_name")]
        [JsonRequired]
        public string? GroupName { get; set; }
    }
}
