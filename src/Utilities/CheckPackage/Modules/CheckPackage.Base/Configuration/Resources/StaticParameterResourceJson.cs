using CheckPackage.Configuration.Json.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Base.Configuration
{

    public class StaticParameterResourceJson : ResourceJson
    {
        [JsonProperty("parameters")]
        [JsonRequired]
        public Dictionary<string, StaticParameterResourceItemJson>? Parameters { get; set; }

    }



    public class StaticParameterResourceItemJson
    {        
        [JsonProperty("description")]
        [JsonRequired]
        public string? Description { get; set; }

        [JsonProperty("value")]
        [JsonRequired]
        public string? Value { get; set; }
    }
}
