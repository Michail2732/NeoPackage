using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Configuration.Entities
{
    public class ConfigurationHistoryJson
    {
        [JsonProperty("project_parameter_id")]
        public string ProjectParametersId { get; set; }
        [JsonProperty("delivery_date")]
        public DateTime DelivaryDate { get; set; }
    }
}
