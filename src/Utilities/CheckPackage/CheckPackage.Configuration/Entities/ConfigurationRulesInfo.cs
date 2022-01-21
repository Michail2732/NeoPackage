using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Configuration.Entities
{
    public class ConfigurationRulesInfo
    {
        public string Id { get; private set; }
        public Version Version { get; private set; }
        public string Description { get; private set; }

        public ConfigurationRulesInfo(string id, Version version, string description = "")
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Version = version ?? throw new ArgumentNullException(nameof(version));
            Description = description;
        }
    }
}
