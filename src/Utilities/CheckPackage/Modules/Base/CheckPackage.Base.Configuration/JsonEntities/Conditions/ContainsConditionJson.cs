using CheckPackage.Configuration.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CheckPackage.Base.Configuration
{
    public class ContainsConditionJson: BaseConditionJson
    {
        [JsonProperty("values")]
        [JsonRequired]
        public List<string>? Values { get; set; }

    }
}
