using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Configuration.Entities
{
    public class ConfigurationParametersInfo
    {
        public string Id { get; private set; }
        public Version Version { get; private set; }
        public string Description { get; private set; }
        public string ConfigurationRulesId { get; private set; }

        public ConfigurationParametersInfo(string id, Version version, 
            string configurationRulesId, string description = "")
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Version = version ?? throw new ArgumentNullException(nameof(version));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            ConfigurationRulesId = configurationRulesId;
        }
    }
}
