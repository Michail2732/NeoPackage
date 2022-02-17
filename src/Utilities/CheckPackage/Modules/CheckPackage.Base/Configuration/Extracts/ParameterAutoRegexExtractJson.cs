using CheckPackage.Configuration.Json.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Base.Configuration
{
    public class ParameterAutoRegexExtractJson : ParameterExtractJson
    {
        [JsonProperty("parameter_id")]        
        public string? ParameterId { get; set; }
    }
}
