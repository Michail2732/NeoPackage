using CheckPackage.Configuration.Entities;
using CheckPackage.DownloadSheet.Checks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.DownloadSheet.Configuration
{
    public class LoadlistStructureCheckJson: BaseCheckJson
    {        
        [JsonProperty("columns_filter")]
        [JsonRequired]
        public List<string>? ColumnsFilter { get; set; }

        [JsonProperty("row_filters")]
        [JsonRequired]
        public List<RowFilterJson>? RowsFilter { get; set; }

        [JsonProperty("identifier_columns")]
        [JsonRequired]
        public List<string>? IdentifierColumns { get; set; }
    }
}
