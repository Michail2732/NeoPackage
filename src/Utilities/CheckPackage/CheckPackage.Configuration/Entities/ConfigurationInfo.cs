using CheckPackage.Configuration.Entities;
using Package.Abstraction.Entities;
using Package.Abstraction.Extensions;
using Package.Localization;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace CheckPackage.Configuration.Entities
{
    public class ConfigurationInfo
    {                
        public string DirectoryPackage { get; }        
        public string DirectoryLogs { get; }        
        public string? LoadListTemplate { get; }        
        public bool CheckHash { get; }        
        public string ConfigurationRulesId { get; }        
        public string ConfigurationParametersId { get; }

        public ConfigurationInfo(string directoryPackage, string directoryLogs, 
            bool checkHash, string configurationRulesId, string configurationParametersId,
            string? loadListTemplate = null)
        {
            DirectoryPackage = directoryPackage ?? throw new ArgumentNullException(nameof(directoryPackage));
            DirectoryLogs = directoryLogs ?? throw new ArgumentNullException(nameof(directoryLogs));
            LoadListTemplate = loadListTemplate;
            CheckHash = checkHash;
            ConfigurationRulesId = configurationRulesId ?? throw new ArgumentNullException(nameof(configurationRulesId));
            ConfigurationParametersId = configurationParametersId ?? throw new ArgumentNullException(nameof(configurationParametersId));
        }

        public Result Validate(ConfigurationJson configuration, MessagesService messages)
        {
            StringBuilder sb = new StringBuilder();                                                
            if (!Directory.Exists(DirectoryLogs))
                sb.Append(messages.Get(MessageKeys.DirectoryNotExist, DirectoryLogs) + "\n");
            if (!Directory.Exists(DirectoryPackage))
                sb.Append(messages.Get(MessageKeys.DirectoryNotExist, DirectoryPackage) + "\n");
            if (!string.IsNullOrEmpty(LoadListTemplate) && !File.Exists(LoadListTemplate))
                sb.Append(messages.Get(MessageKeys.FileNotExist, LoadListTemplate) + "\n");
            var configurationRules = configuration.ConfigurationRules?.FirstOrDefault(a => a.Info?.Id == ConfigurationRulesId);
            var configurationParameters = configuration.ConfigurationParameters?.FirstOrDefault(a => a.Info!.Id == ConfigurationParametersId);
            if (configurationRules == null)
                sb.Append(messages.Get(MessageKeys.ConfigurationNotExist, ConfigurationRulesId) + "\n");
            if (configurationParameters == null)
                sb.Append(messages.Get(MessageKeys.ProjectConfigurationNotExist, ConfigurationParametersId) + "\n");
            if (configurationRules != null && configurationParameters != null &&
                configurationParameters.ConfigurationRulesId != configurationRules.Info?.Id)
                sb.Append(messages.Get(MessageKeys.ConfigurationNotReferToProjectConfig,
                    (configurationParameters.Info?.Id).EmptyIfNull(), (configurationRules.Info?.Id).EmptyIfNull()) + "\n");
            return new Result(sb.Length == 0, sb.ToString());
        }
    }
}
