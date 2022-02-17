using CheckPackage.Configuration.Json.Entities;
using CheckPackage.Core.Resources;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Base.Configuration
{

    public class ValueTreeResourceJson : ResourceJson
    {
        [JsonProperty("trees")]
        [JsonRequired]
        public Dictionary<string, ValueTreeNode> Trees { get; set; }

    }
    
}
