using Newtonsoft.Json;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Configuration.Json.Entities
{
    public class ParameterOutputRuleJson
    {
        [JsonProperty("parameter_id")]
        [JsonRequired]
        public string ParameterId { get; set; }
        [JsonProperty("is_user_parameter")]
        [JsonRequired]
        public bool IsUserParameter { get; set; }
        [JsonProperty("state")]
        [JsonRequired]
        public Critical State { get; set; }        
        [JsonProperty("output")]
        [JsonRequired]
        public ParameterOutputJson Output { get; set; }

    }
}
