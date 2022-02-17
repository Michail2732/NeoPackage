using CheckPackage.Configuration.Json.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CheckPackage.Base.Configuration
{
    public class EntityTreeValuesConditionJson: EntityConditionJson
    {
        [JsonProperty("tree_id")]
        [JsonRequired]
        public string TreeValuesId { get; set; }        

        [JsonProperty("parameter_ids")]
        [JsonRequired]
        public List<string> ParameterIds { get; set; }

    }
}
