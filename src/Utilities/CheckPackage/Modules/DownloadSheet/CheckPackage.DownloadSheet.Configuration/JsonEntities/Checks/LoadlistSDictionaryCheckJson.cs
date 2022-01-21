using CheckPackage.Configuration.Entities;
using CheckPackage.DownloadSheet.Checks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.DownloadSheet.Configuration
{
    public class LoadlistSDictionaryCheckJson: BaseCheckJson
    {        
        [JsonProperty("columns_filter")]
        [JsonRequired]
        public List<string>? ColumnsFilter { get; set; }

        [JsonProperty("row_filters")]
        [JsonRequired]
        public List<RowFilterJson>? RowsFilter { get; set; }        

        [JsonProperty("dictionary_id")]
        [JsonRequired]
        public string? DictionaryId { get; set; }
    }
}
