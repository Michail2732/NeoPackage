using CheckPackage.Configuration.Json.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CheckPackage.DownloadSheet.Configuration
{

    public class LoadlistColumnMapResourceJson : ResourceJson
    {
        [JsonProperty("column_rules")]
        [JsonRequired]
        public Dictionary<string, LoadlistColumnRuleJson> ColumnRules { get; set; }
    }

    public class LoadlistColumnRuleJson
    {
        [JsonProperty("column")]
        [JsonRequired]
        public string? Column { get; set; }
        [JsonProperty("name")]
        [JsonRequired]
        public string? Name { get; set; }
        [JsonProperty("selector")]
        [JsonRequired]
        public ParametersSelectorJson? Selector { get; set; }
        [JsonProperty("extract")]
        [JsonRequired]
        public ParameterExtractJson? Extractor { get; set; }
    }
}
