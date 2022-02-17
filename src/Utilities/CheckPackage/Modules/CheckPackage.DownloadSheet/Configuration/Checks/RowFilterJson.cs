using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.DownloadSheet.Configuration
{
    public class RowFilterJson
    {
        [JsonProperty("id")]
        [JsonRequired]
        public string? Id { get; set; }

        [JsonProperty("column_name")]
        [JsonRequired]
        public string? ColumnName { get; set; }

        [JsonProperty("regex_pattern")]
        [JsonRequired]
        public string? RegexPattern { get; set; }
    }
}
