using CheckPackage.Configuration.Entities;
using CheckPackage.Configuration.Exceptions;
using Newtonsoft.Json.Linq;
using Package.Localization;
using System;
using System.Collections.Generic;

namespace CheckPackage.Configuration.Services
{
    public sealed class ConfigurationServiceLow : IConfigurationServiceLow
    {       
        private object _sync = new object();
        private readonly MessagesService _messages;
        private readonly ConfigurationProvider _configurationProvider;

        private ConfigurationJson? _configurationJson;
        private ConfigurationJson ConfigurationJson
        {
            get { lock (_sync) { return _configurationJson ??= _configurationProvider.Get(); } }            
        }

        public ConfigurationServiceLow(MessagesService messages)
        {
            _messages = messages ?? throw new ArgumentNullException(nameof(messages));
            _configurationProvider = ConfigurationProvider.Instance ?? throw new ArgumentNullException(nameof(_configurationProvider));
            _configurationProvider.ConfigurationChanged += ConfigurationChanged;
        }        

        public Dictionary<string, JToken> GetChecks()
        {
            string? configurationRulesId = ConfigurationJson.ConfigurationRulesId;
            string? configurationParametersId = ConfigurationJson.ConfigurationParametersId;
            if (!string.IsNullOrEmpty(configurationRulesId))
                return ConfigurationJson.ConfigurationRules?.Find(a => a.Info!.Id == configurationRulesId).Checks ??
                    new Dictionary<string, JToken>();
            return new Dictionary<string, JToken>();
        }

        public Dictionary<string, JToken> GetDictionaries()
        {
            string? configurationRulesId = ConfigurationJson.ConfigurationRulesId;
            string? configurationParametersId = ConfigurationJson.ConfigurationParametersId;
            ConfigurationRulesJson? configurationRules = null;
            ConfigurationParametersJson? configurationParameters = null;
            Dictionary<string, JToken> result = new Dictionary<string, JToken>();
            if (!string.IsNullOrEmpty(configurationRulesId))
            {
                configurationRules = ConfigurationJson.ConfigurationRules?.Find(a => a.Info?.Id == configurationRulesId);
                if (configurationRules?.Dictionary != null)
                    foreach (var dictionary in configurationRules.Dictionary)                
                        result.Add(dictionary.Key, dictionary.Value);                
            }
            if (!string.IsNullOrEmpty(configurationParametersId))
            {
                configurationParameters = ConfigurationJson.ConfigurationParameters?.Find(a => a.Info?.Id == configurationParametersId);
                if (configurationParameters?.Parameters != null)
                    foreach (var parameter in configurationParameters.Parameters)
                        result.Add(parameter.Key, parameter.Value);
                if (configurationParameters?.SetParameters != null)
                foreach (var setParameter in configurationParameters.SetParameters)                
                    result.Add(setParameter.Key, JArray.FromObject(setParameter.Value));                
            }                            
            return result;
        }

        public Dictionary<string, JToken> GetRules()
        {
            string? configurationRulesId = ConfigurationJson.ConfigurationRulesId;
            ConfigurationRulesJson? configurationRules = null;            
            JObject result = new JObject();
            if (!string.IsNullOrEmpty(configurationRulesId))
            {
                configurationRules = ConfigurationJson.ConfigurationRules?.Find(a => a.Info?.Id == configurationRulesId);
                return configurationRules?.Rules ?? new Dictionary<string, JToken>();
            }
            return new Dictionary<string, JToken>();
        }

        public void UpdateRule(string sectionId, JToken newRules)
        {
            var configuration = ConfigurationJson;
            string? configurationRulesId = configuration.ConfigurationRulesId;
            if (string.IsNullOrEmpty(configurationRulesId))
                throw new ConfigurationException(_messages.Get(MessageKeys.NotConfigurationSelected));
            var configurationRules = configuration.ConfigurationRules?.Find(a => a.Info!.Id == configurationRulesId);
            if (!(configurationRules?.Rules?.ContainsKey(sectionId) == true))
                throw new ConfigurationException(_messages.Get(MessageKeys.NotFoundSectionRule, sectionId));
            configurationRules.Rules[sectionId] = newRules;
            OnConfigurationChanged();                        
        }

        public void UpdateDictionary(string sectionId, JToken newDicts)
        {
            var configuration = ConfigurationJson;
            string? configurationRulesId = configuration.ConfigurationRulesId;
            if (string.IsNullOrEmpty(configurationRulesId))
                throw new ConfigurationException(_messages.Get(MessageKeys.NotConfigurationSelected));
            var configurationRules = configuration.ConfigurationRules?.Find(a => a.Info!.Id == configurationRulesId);            
            if (!(configurationRules?.Dictionary?.ContainsKey(sectionId) == true))
                throw new ConfigurationException(_messages.Get(MessageKeys.NotFoundSectionDict, sectionId));
            configurationRules.Dictionary[sectionId] = newDicts;            
            OnConfigurationChanged();
        }

        public void UpdateСheck(string sectionId, JToken newChecks)
        {
            var configuration = ConfigurationJson;
            string? configurationRulesId = configuration.ConfigurationRulesId;
            if (string.IsNullOrEmpty(configurationRulesId))
                throw new ConfigurationException(_messages.Get(MessageKeys.NotConfigurationSelected));
            var configurationRules = configuration.ConfigurationRules?.Find(a => a.Info!.Id == configurationRulesId);
            if (!(configurationRules?.Checks?.ContainsKey(sectionId) == true))
                throw new ConfigurationException(_messages.Get(MessageKeys.NotFoundSectionCheck, sectionId));
            configurationRules.Checks[sectionId] = newChecks;            
            OnConfigurationChanged();
        }

        private void ConfigurationChanged() { lock (_sync) { _configurationJson = null; } }
        private void OnConfigurationChanged() => _configurationProvider.Set(ConfigurationJson);        
    }
}
