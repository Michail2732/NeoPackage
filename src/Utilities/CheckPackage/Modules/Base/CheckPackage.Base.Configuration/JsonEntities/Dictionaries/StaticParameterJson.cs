using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Base.Configuration
{
    public class StaticParameterJson
    {
        [JsonProperty("description")]
        [JsonRequired]
        public string? Description { get; set; }
        [JsonProperty("value")]
        [JsonRequired]
        public string? Value { get; set; }
    }
}
