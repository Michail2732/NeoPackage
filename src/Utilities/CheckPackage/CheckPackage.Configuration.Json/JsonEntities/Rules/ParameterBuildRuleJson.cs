using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Configuration.Json.Entities
{
    public class ParameterBuildRuleJson
    {        
        [JsonProperty("conditions")]
        public List<EntityConditionJson> Conditions { get; set; }
        [JsonProperty("selector")]        
        public ParametersSelectorJson Selector { get; set; }
        [JsonProperty("extracter")]
        [JsonRequired]
        public ParameterExtractJson Extracter { get; set; }
    }
}
