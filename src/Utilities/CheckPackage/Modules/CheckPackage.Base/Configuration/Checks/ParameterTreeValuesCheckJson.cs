using System;
using Newtonsoft.Json;
using CheckPackage.Configuration.Json.Entities;

namespace CheckPackage.Base.Configuration
{
    public class ParameterTreeValuesCheckJson: ParameterCheckJson
    {        
        [JsonProperty("tree_id")]
        [JsonRequired]
        public string? TreeId { get; set; }        
    }
}
