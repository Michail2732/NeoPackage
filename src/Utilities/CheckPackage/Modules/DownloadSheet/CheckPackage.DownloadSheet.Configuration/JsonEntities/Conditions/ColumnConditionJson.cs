using System;
using System.Collections.Generic;
using System.Text;
using CheckPackage.Configuration.Entities;
using CheckPackage.DownloadSheet.Checks;
using Newtonsoft.Json;


namespace CheckPackage.DownloadSheet.Configuration
{
    public class ColumnConditionJson: BaseConditionJson
    {
        [JsonProperty("column")]
        [JsonRequired]
        public string? Column { get; set; }
        [JsonProperty("value")]
        [JsonRequired]
        public string? Value  { get; set; }

    }
}
