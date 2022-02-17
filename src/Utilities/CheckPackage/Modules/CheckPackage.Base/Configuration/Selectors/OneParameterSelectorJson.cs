using CheckPackage.Configuration.Json.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Base.Configuration
{
    public class OneParameterSelectorJson: ParametersSelectorJson
    {
        [JsonProperty("parameter_id")]
        [JsonRequired]
        public string? ParameterId { get; set; }
        [JsonProperty("is_user_parameter")]
        public bool IsUserParameter { get; set; }
    }
}
