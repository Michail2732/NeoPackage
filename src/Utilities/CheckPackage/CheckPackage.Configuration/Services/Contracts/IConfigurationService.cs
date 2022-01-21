using CheckPackage.Configuration.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Configuration.Services
{
    public interface IConfigurationService
    {
        ConfigurationInfo GetConfiguration();
        bool TryUpdateConfiguration(ConfigurationInfo configuration, out string? error);
        void UpdateConfiguration(ConfigurationInfo configuration);
        ConfigurationRulesInfo GetConfigurationRules(string id);
        ConfigurationParametersInfo GetConfigurationParameters(string id);
        bool HasConfigurationRules(string id);
        bool HasConfigurationParameters(string id);
        IList<ConfigurationRulesInfo> GetAllConfigurationRules();
        IList<ConfigurationParametersInfo> GetAllConfigurationParameters();
        void AddConfigurationRules(byte[] content);
        void AddConfigurationRules(string filePath);
        void AddConfigurationParameters(byte[] content);
        void AddConfigurationParameters(string filePath);
        void RemoveConfigurationRules(string id);
        void RemoveConfigurationParameters(string id);        
        
    }
}
