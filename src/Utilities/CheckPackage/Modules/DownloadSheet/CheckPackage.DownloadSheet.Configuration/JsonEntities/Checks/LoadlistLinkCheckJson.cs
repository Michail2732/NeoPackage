using CheckPackage.Configuration.Entities;
using CheckPackage.DownloadSheet.Checks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.DownloadSheet.Configuration
{
    public class LoadlistLinkCheckJson: BaseCheckJson
    {        
        [JsonProperty("columns_filter")]
        [JsonRequired]
        public List<string>? ColumnsFilter { get; set; }

        [JsonProperty("row_filters")]
        [JsonRequired]
        public List<RowFilterJson>? RowsFilter { get; set; }        

        [JsonProperty("filter_id")]
        [JsonRequired]
        public string? FilterId { get; set; }

        [JsonProperty("filter_id_to")]
        [JsonRequired]
        public string? FilterIdTo { get; set; }

        [JsonProperty("min_count")]
        [JsonRequired]
        public ushort MinCountLink { get; set; }

    }
}
