using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Configuration.Json.Entities
{
    public class ParameterOutputJson
    {        
        [JsonProperty("is_user_parameter")]
        public bool IsUserParameter { get; set; }
    }
}
