using Newtonsoft.Json;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Configuration.Json.Entities
{
    public class ParameterCheckRuleJson
    {        
        [JsonProperty("parameter_id")]
        [JsonRequired]
        public string ParameterId { get; set; }
        [JsonProperty("is_user_parameter")]        
        public bool IsUserParameter { get; set; }
        [JsonProperty("critical")]        
        public Critical Critical { get; set; }
        [JsonProperty("checks")]
        [JsonRequired]
        public List<ParameterCheckJson> Checks { get; set; }
    }
}
