using System;
using CheckPackage.Configuration.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.DownloadSheet.Configuration
{
    public class LoadlistColumnRuleJson
    {
        [JsonIgnore]
        public string? Id { get; set; } = Guid.NewGuid().ToString();
        [JsonProperty("column")]
        [JsonRequired]
        public string? Column { get; set; }
        [JsonProperty("name")]
        [JsonRequired]
        public string? Name { get; set; }
        [JsonProperty("conditions")]        
        public List<BaseConditionJson>? Conditions { get; set; }
        [JsonProperty("extracts")]
        [JsonRequired]
        public List<BaseExtractJson>? Extracts { get; set; }
    }
}
