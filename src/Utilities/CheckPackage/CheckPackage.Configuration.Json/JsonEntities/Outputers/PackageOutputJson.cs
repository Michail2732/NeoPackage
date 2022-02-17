using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Configuration.Json.Entities
{
    public class PackageOutputJson
    {
        [JsonProperty("message")]
        [JsonRequired]
        public string Message { get; set; }
    }
}
