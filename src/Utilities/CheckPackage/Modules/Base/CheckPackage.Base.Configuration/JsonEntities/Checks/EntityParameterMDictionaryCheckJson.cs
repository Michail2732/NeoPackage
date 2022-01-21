using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using CheckPackage.Configuration.Entities;

namespace CheckPackage.Base.Configuration
{
    public class EntityParameterMDictionaryCheckJson: BaseCheckJson
    {        
        [JsonProperty("dictionary_name")]
        [JsonRequired]
        public string? DictionaryName { get; set; }
        [JsonProperty("key_parameter_id")]
        [JsonRequired]
        public string? KeyParameterId { get; set; }        
    }
}
