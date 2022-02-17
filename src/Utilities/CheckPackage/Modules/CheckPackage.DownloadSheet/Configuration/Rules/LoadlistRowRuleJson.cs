using CheckPackage.Configuration.Json.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CheckPackage.DownloadSheet.Configuration
{

    public class LoadlistRowMapResourceJson : ResourceJson
    {
        [JsonProperty("row_rules")]
        [JsonRequired]
        public List<LoadlistRowRuleJson> RowRules { get; set; }
    }

    public class LoadlistRowRuleJson
    {        
        [JsonProperty("priority")]
        [JsonRequired]
        public int Priority { get; set; }
        [JsonProperty("is_virtual")]
        [JsonRequired]
        public bool IsVirtual { get; set; }
        [JsonProperty("entity_level")]
        [JsonRequired]
        public int EntityLevel { get; set; }
        [JsonProperty("conditions")]
        [JsonRequired]
        public List<EntityConditionJson>? Conditions { get; set; }        
        [JsonProperty("column_ids")]
        [JsonRequired]
        public List<string>? ColumnIds { get; set; }

    }
}
