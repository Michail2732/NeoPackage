using CheckPackage.Configuration.Json.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Base.Configuration
{
    public class EntityTreeValuesCheckJson: EntityCheckJson
    {
        [JsonProperty("tree_id")]
        [JsonRequired]
        public string TreeValuesId { get; set; }

        [JsonProperty("parameter_ids")]
        [JsonRequired]
        public List<string> ParameterIds { get; set; }

    }
}
