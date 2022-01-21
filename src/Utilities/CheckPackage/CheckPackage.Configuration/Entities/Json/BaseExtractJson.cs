using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Configuration.Entities
{
    public class BaseExtractJson
    {
        [JsonProperty("extract_id")]
        [JsonRequired]
        public string? ExtractId { get; set; }        
        [JsonProperty("entity_parameter_id")]
        [JsonRequired]
        public string? EntityParameterId { get; set; }
        [JsonProperty("parameter_id")]        
        public string? ParameterId { get; set; }
        [JsonProperty("in_all_entities")]        
        public bool InAllEntities { get; set; }
    }
}
