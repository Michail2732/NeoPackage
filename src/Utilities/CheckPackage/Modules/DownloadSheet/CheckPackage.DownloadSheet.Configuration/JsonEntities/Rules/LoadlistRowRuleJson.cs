using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using CheckPackage.Configuration.Entities;
using CheckPackage.DownloadSheet.Checks;
using System.Text;

namespace CheckPackage.DownloadSheet.Configuration
{
    public class LoadlistRowRuleJson
    {
        [JsonIgnore]
        public string Id { get; set; } = Guid.NewGuid().ToString();
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
        public List<BaseConditionJson>? Conditions { get; set; }
        [JsonProperty("entity_name_column")]
        [JsonRequired]
        public string? EntityNameColumn { get; set; }
        [JsonProperty("column_names")]
        [JsonRequired]
        public List<string>? ColumnNames { get; set; }

    }
}
