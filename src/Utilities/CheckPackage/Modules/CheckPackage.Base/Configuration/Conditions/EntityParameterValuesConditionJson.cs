using CheckPackage.Configuration.Json.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Base.Configuration
{
    public class EntityParameterValuesConditionJson : EntityConditionJson
    {
        [JsonProperty("parameter_id")]
        [JsonRequired]
        public string ParameterId { get; set; }
        [JsonProperty("values")]
        [JsonRequired]
        public string[] Values { get; set; }
        [JsonProperty("is_contains")]
        [JsonRequired]
        public bool IsContains { get; set; }
    }
}
