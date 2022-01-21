using CheckPackage.Configuration.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Base.Configuration
{
    public class RegexExtractJson: BaseExtractJson
    {
        [JsonProperty("regex_template_id")]
        [JsonRequired]
        public string? RegexTemplateId { get; set; }
    }
}
