using CheckPackage.Configuration.Json.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CheckPackage.DownloadSheet.Configuration
{
    public class LoadlistStructureCheckJson: PackageCheckJson
    {                
        [JsonProperty("identifier_columns")]
        [JsonRequired]
        public List<string>? IdentifierColumns { get; set; }
    }
}
