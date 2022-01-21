using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Configuration.Entities
{
    public class ConfigurationJson
    {
        [JsonProperty("directory_package")]
        [JsonRequired]
        public string? DirectoryPackage { get; set; }
        [JsonProperty("directory_log")]
        [JsonRequired]
        public string? DirectoryLogs { get; set; }
        [JsonProperty("loadlist_template")]
        [JsonRequired]
        public string? LoadListTemplate { get; set; }
        [JsonProperty("check_file_hash")]
        [JsonRequired]
        public bool CheckHash { get; set; }
        [JsonProperty("current_rule_id")]
        [JsonRequired]
        public string? ConfigurationRulesId { get; set; }
        [JsonProperty("current_globalparam_id")]
        [JsonRequired]
        public string? ConfigurationParametersId { get; set; }

        [JsonProperty("projects_delivery_date")]
        [JsonRequired]
        public Dictionary<string, ConfigurationHistoryJson>? SettingsHistory { get; set; }        

        [JsonProperty("configurations")]
        [JsonRequired]
        public List<ConfigurationRulesJson>? ConfigurationRules { get; set; }

        [JsonProperty("project_globalparams")]
        [JsonRequired]
        public List<ConfigurationParametersJson>? ConfigurationParameters { get; set; }
    }
}
