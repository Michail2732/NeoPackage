using CheckPackage.Configuration.Json.Entities;
using CheckPackage.Core.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Base.Configuration
{
    public class EntityParameterValueCheckJson: EntityCheckJson
    {
        [JsonProperty("parameter_id")]
        [JsonRequired]
        public string? ParameterId { get; set; }

        [JsonProperty("value")]
        [JsonRequired]
        public string? Value { get; set; }

        [JsonProperty("operator")]
        [JsonRequired]
        public ValueAction Operator { get; set; }

    }
}
