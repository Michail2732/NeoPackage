using CheckPackage.Configuration.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Base.Configuration
{
    public class RegexMatchConditionJson: BaseConditionJson
    {
        [JsonProperty("regex_template")]
        [JsonRequired]
        public string? RegexTemplate { get; set; }
    }
}
