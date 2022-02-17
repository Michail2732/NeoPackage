using CheckPackage.Configuration.Json.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CheckPackage.DownloadSheet.Configuration
{
    public class LoadlistLinkCheckJson: ParameterCheckJson
    {        
        [JsonProperty("columns_filter")]
        [JsonRequired]
        public List<string>? ColumnsFilter { get; set; }

        [JsonProperty("row_filter_from")]
        [JsonRequired]
        public RowFilterJson? RowsFilterFrom { get; set; }

        [JsonProperty("row_filter_to")]
        [JsonRequired]
        public RowFilterJson? RowsFilterTo { get; set; }

        [JsonProperty("min_count")]
        [JsonRequired]
        public ushort MinCountLink { get; set; }
    }
}
