using CheckPackage.Core.Checks;
using CheckPackage.Core.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Configuration.Json.Entities
{
    public class ParameterCheckJson 
    {                
        [JsonProperty("logic")]        
        public Logical Logic { get; set; }

        [JsonProperty("message")]
        [JsonRequired]
        public string? Message { get; set; }        

        [JsonProperty("inverse")]
        public bool Inverse { get; set; }
    }
}
