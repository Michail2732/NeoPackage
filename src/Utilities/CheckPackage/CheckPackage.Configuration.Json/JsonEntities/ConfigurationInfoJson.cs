using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Configuration.Json.Entities
{
    public class ConfigurationInfoJson
    {
        [JsonProperty("id")]
        [JsonRequired]
        public string? Id { get; set; }
        [JsonProperty("version")]
        [JsonRequired]
        public Version? Version { get; set; }
        [JsonProperty("description")]
        [JsonRequired]
        public string? Description { get; set; }
    }
}
