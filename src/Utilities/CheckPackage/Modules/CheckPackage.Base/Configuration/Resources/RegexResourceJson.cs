using CheckPackage.Configuration.Json.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Base.Configuration
{
    public class RegexResourceJson: ResourceJson
    {
        [JsonProperty("patterns")]
        [JsonRequired]
        public Dictionary<string, string>? Patterns { get; set; }
        [JsonProperty("composite_patterns")]
        [JsonRequired]
        public Dictionary<string, string>? CompositePatterns { get; set; }
    }    
}
