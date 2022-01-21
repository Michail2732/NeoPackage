using System;
using System.Collections.Generic;
using System.Text;
using CheckPackage.Configuration.Entities;
using Newtonsoft.Json;


namespace CheckPackage.Base.Configuration
{
    public class EqualConditionJson: BaseConditionJson
    {
        [JsonProperty("value")]
        [JsonRequired]
        public string? Value { get; set; }

    }
}
