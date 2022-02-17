using CheckPackage.Configuration.Json.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CheckPackage.DownloadSheet.Configuration
{
    public class LoadlistTreeValuesCheckJson: ParameterCheckJson
    {        
        [JsonProperty("columns_filter")]
        [JsonRequired]
        public List<string>? ColumnsFilter { get; set; }

        [JsonProperty("row_filters")]
        [JsonRequired]
        public List<RowFilterJson>? RowsFilter { get; set; }        

        [JsonProperty("tree_id")]
        [JsonRequired]
        public string? TreeId { get; set; }
    }
}
