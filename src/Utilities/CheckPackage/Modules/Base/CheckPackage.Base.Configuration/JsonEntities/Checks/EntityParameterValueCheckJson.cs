using CheckPackage.Base.Checks;
using CheckPackage.Configuration.Entities;
using Newtonsoft.Json;

namespace CheckPackage.Base.Configuration
{
    public class EntityParameterValueCheckJson: BaseCheckJson
    {        
        [JsonProperty("value")]
        [JsonRequired]
        public string? Value { get; set; }
        [JsonProperty("operator")]
        [JsonRequired]
        public CheckOperatorType Operator { get; set; }        
    }
}
