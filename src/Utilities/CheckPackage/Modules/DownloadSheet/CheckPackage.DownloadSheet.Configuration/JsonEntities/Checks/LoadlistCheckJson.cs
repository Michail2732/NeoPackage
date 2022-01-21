using CheckPackage.Configuration.Entities;
using CheckPackage.DownloadSheet.Checks;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CheckPackage.DownloadSheet.Configuration
{
    public class LoadlistCheckJson: BaseCheckJson
    {        
        [JsonProperty("check_type")]
        [JsonRequired]
        public CheckType CheckType { get; set; }

        [JsonProperty("columns_filter")]
        [JsonRequired]
        public List<string>? ColumnsFilter { get; set; }

        [JsonProperty("row_filters")]
        [JsonRequired]
        public List<RowFilterJson>? RowsFilter { get; set; }        
    }
}
