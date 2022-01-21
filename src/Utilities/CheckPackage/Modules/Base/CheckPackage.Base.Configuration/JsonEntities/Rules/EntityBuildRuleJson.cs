using CheckPackage.Configuration.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Base.Configuration
{
    public class EntityBuildRuleJson
    {
        [JsonProperty("entity_level")]
        [JsonRequired]
        public uint EntityLevel { get; set; }
        [JsonProperty("priority")]
        [JsonRequired]
        public uint Priority { get; set; }
        [JsonProperty("group_by")]
        [JsonRequired]
        public string? GroupBy { get; set; }
        [JsonProperty("conditions")]
        [JsonRequired]
        public List<BaseConditionJson>? Conditions { get; set; }
        [JsonProperty("parameters")]
        [JsonRequired]
        public ParametersBuildRuleJson? Parameters { get; set; }
    }

    public class ParameterRuleJson
    {
        [JsonProperty("conditions")]
        [JsonRequired]
        public List<BaseConditionJson>? Conditions { get; set; }
        [JsonProperty("extracter")]
        [JsonRequired]
        public BaseExtractJson? Extracter { get; set; }
    }

    public class ParametersBuildRuleJson
    {
        [JsonProperty("static")]
        [JsonRequired]
        public List<StaticParameterRuleJson>? Statics { get; set; }
        [JsonProperty("extract")]
        [JsonRequired]
        public List<ParameterRuleJson>? Extracts { get; set; }
    }

    public class StaticParameterRuleJson
    {
        [JsonProperty("parameter_id")]
        [JsonRequired]
        public string? ParameterId { get; set; }
        [JsonProperty("value")]
        [JsonRequired]
        public string? Value { get; set; }
        [JsonProperty("conditions")]
        [JsonRequired]
        public List<BaseConditionJson>? Conditions { get; set; }
    }

}
