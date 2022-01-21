﻿using CheckPackage.Configuration.Entities;
using Newtonsoft.Json;

namespace CheckPackage.Base.Configuration
{
    public class EntityParameterLengthCheckJson: BaseCheckJson
    {        
        [JsonProperty("min_length")]
        [JsonRequired]
        public uint MinLength { get; set; }
        [JsonProperty("max_length")]
        [JsonRequired]
        public uint MaxLength { get; set; }
        
    }
}