using CheckPackage.Configuration.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Base.Configuration
{
    public class StaticExtractJson: BaseExtractJson
    {
        [JsonProperty("static_parameter_id")]
        [JsonRequired]
        public string? StaticParameterId { get; set; }
    }
}
