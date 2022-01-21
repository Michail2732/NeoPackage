using CheckPackage.Configuration.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Base.Configuration
{
    public class StaticValueExtractJson: BaseExtractJson
    {
        [JsonProperty("parameter_value")]
        [JsonRequired]
        public string? ParameterValue { get; set; }
    }
}
