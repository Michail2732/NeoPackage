using CheckPackage.Core.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Configuration.Json.Entities
{
    public class EntityCheckJson
    {        
        [JsonProperty("logic")]
        [JsonRequired]
        public Logical Logic { get; set; }

        [JsonProperty("child_checks")]
        public EntityChildChecksJson? ChildChecks { get; set; }

        [JsonProperty("message")]
        [JsonRequired]
        public string? Message { get; set; }

        [JsonProperty("inverse")]
        public bool Inverse { get; set; }
    }


    public class EntityChildChecksJson
    {
        [JsonProperty("levels")]
        [JsonRequired]
        public List<uint> Levels { get; set; }
        [JsonProperty("logic")]
        [JsonRequired]
        public Logical Logic { get; set; }
    }
}
