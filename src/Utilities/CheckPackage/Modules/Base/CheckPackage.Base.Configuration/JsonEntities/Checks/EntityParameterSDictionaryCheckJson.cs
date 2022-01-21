using System;
using System.Collections.Generic;
using System.Text;
using CheckPackage.Configuration.Entities;
using Newtonsoft.Json;

namespace CheckPackage.Base.Configuration
{
    public class EntityParameterSDictionaryCheckJson: BaseCheckJson
    {        
        [JsonProperty("dictionary_name")]
        [JsonRequired]
        public string? DictionaryName { get; set; }        
    }
}
