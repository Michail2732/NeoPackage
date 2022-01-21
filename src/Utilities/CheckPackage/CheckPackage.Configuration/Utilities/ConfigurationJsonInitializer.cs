using CheckPackage.Configuration.Entities;
using System.Collections.Generic;
using System.IO;

namespace CheckPackage.Configuration.Utilities
{
    internal class ConfigurationJsonInitializer
    {
        public static ConfigurationJson Default =>
            new ConfigurationJson
            {                
                ConfigurationRules = new List<ConfigurationRulesJson>(),
                DirectoryLogs = Directory.GetCurrentDirectory(),
                ConfigurationParameters = new List<ConfigurationParametersJson>(),
                SettingsHistory = new Dictionary<string, ConfigurationHistoryJson>()
            };
    }
}
