using CheckPackage.Configuration.Entities;
using CheckPackage.Configuration.Exceptions;
using Newtonsoft.Json;
using Package.Abstraction.Extensions;
using Package.Localization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CheckPackage.Configuration.Services
{
    public sealed class ConfigurationService : IConfigurationService
    {
        private readonly MessagesService _messages;
        private readonly ConfigurationProvider _configurationProvider;
        private object _sync = new object();

        private ConfigurationJson? _configurationJson;

        public ConfigurationService(MessagesService messages)
        {
            _messages = messages ?? throw new ArgumentNullException(nameof(messages));
            _configurationProvider = ConfigurationProvider.Instance ?? throw new ArgumentNullException(nameof(_configurationProvider));
            _configurationProvider.ConfigurationChanged += () =>
            {
                lock (_sync)
                {
                    _configurationJson = null;
                }
            };
        }        

        private ConfigurationJson ConfigurationJson
        {
            get { lock (_sync) { return _configurationJson ??= _configurationProvider.Get(); } }            
        }                                                    

        public ConfigurationInfo GetConfiguration()
        {
            var configuration = ConfigurationJson;
            return new ConfigurationInfo(configuration.DirectoryPackage.EmptyIfNull(),
                configuration.DirectoryLogs.EmptyIfNull(), configuration.CheckHash, 
                configuration.ConfigurationRulesId.EmptyIfNull(),
                configuration.ConfigurationParametersId.EmptyIfNull(),
                configuration.LoadListTemplate.EmptyIfNull());
        }

        public bool TryUpdateConfiguration(ConfigurationInfo configuration, out string? error)
        {
            error = null;
            var configurationLcl = ConfigurationJson;
            var validationResult = configuration.Validate(configurationLcl, _messages);
            if (!validationResult.IsSuccess)
            {
                error = validationResult.Details ?? "";
                return false;
            }
            configurationLcl.DirectoryPackage = configuration.DirectoryPackage;
            configurationLcl.DirectoryLogs = configuration.DirectoryLogs;
            configurationLcl.CheckHash = configuration.CheckHash;
            configurationLcl.LoadListTemplate = configuration.LoadListTemplate;
            configurationLcl.ConfigurationRulesId = configuration.ConfigurationRulesId;
            configurationLcl.ConfigurationParametersId = configuration.ConfigurationParametersId;
            OnConfigurationChanged();
            return true;
        }

        public void UpdateConfiguration(ConfigurationInfo configuration)
        {
            var configurationLcl = ConfigurationJson;
            var validationResult = configuration.Validate(configurationLcl, _messages);
            if (!validationResult.IsSuccess)
                throw new ConfigurationException(validationResult.Details ?? "");
            configurationLcl.DirectoryPackage = configuration.DirectoryPackage;
            configurationLcl.DirectoryLogs = configuration.DirectoryLogs;
            configurationLcl.CheckHash = configuration.CheckHash;
            configurationLcl.LoadListTemplate = configuration.LoadListTemplate;
            configurationLcl.ConfigurationRulesId = configuration.ConfigurationRulesId;
            configurationLcl.ConfigurationParametersId = configuration.ConfigurationParametersId;
            OnConfigurationChanged();
        }

        public ConfigurationRulesInfo GetConfigurationRules(string id)
        {
            if (id is null)
                throw new ArgumentNullException(nameof(id));
            try
            {
                var configurationRules = ConfigurationJson.ConfigurationRules.FirstOrDefault(a => a.Info!.Id == id);
                if (configurationRules == null)
                    throw new ConfigurationException(_messages.Get(MessageKeys.ConfigurationNotExist, id));
                return new ConfigurationRulesInfo((configurationRules.Info?.Id).EmptyIfNull(),
                    configurationRules.Info?.Version ?? new Version(),
                    (configurationRules.Info?.Description).EmptyIfNull());
            }
            catch (Exception ex)
            {
                throw new ConfigurationException(_messages.Get(MessageKeys.CouldNotReturnConfiguration), ex);
            }
        }

        public ConfigurationParametersInfo GetConfigurationParameters(string id)
        {
            try
            {
                var configuration = ConfigurationJson.ConfigurationParameters.
                    FirstOrDefault(a => a.Info!.Id == id);
                if (configuration == null)
                    throw new ConfigurationException(_messages.Get(MessageKeys.ProjectConfigurationNotExist, id));
                return new ConfigurationParametersInfo((configuration.Info?.Id).EmptyIfNull(),
                    configuration.Info?.Version ?? new Version(), 
                    configuration.ConfigurationRulesId.EmptyIfNull(), 
                    (configuration.Info?.Description).EmptyIfNull());
            }
            catch (Exception ex)
            {
                throw new ConfigurationException(_messages.Get(MessageKeys.CouldNotReturnProjectConfig), ex);
            }
        }

        public bool HasConfigurationRules(string id)
        {
            if (id is null)
                throw new ArgumentNullException(nameof(id));
            try
            {
                return ConfigurationJson.ConfigurationRules.Any(a => a.Info!.Id == id);
            }
            catch (Exception ex)
            {
                throw new ConfigurationException(_messages.Get(MessageKeys.CouldNotCheckExistConfig), ex);
            }
        }

        public bool HasConfigurationParameters(string id)
        {
            if (id is null)
                throw new ArgumentNullException(nameof(id));
            try
            {
                return ConfigurationJson.ConfigurationParameters.Any(a => a.Info!.Id == id);
            }
            catch (Exception ex)
            {
                throw new ConfigurationException(_messages.Get(MessageKeys.CouldNotCheckExistProjectConfig), ex);
            }
        }

        public IList<ConfigurationRulesInfo> GetAllConfigurationRules()
        {
            try
            {
                var allConfigurationRules = ConfigurationJson.ConfigurationRules;
                return allConfigurationRules.Select(a => new ConfigurationRulesInfo(
                    (a.Info?.Id).EmptyIfNull(),
                    a.Info?.Version ?? new Version(),
                    (a.Info?.Description).EmptyIfNull())).ToList();
            }
            catch (Exception ex)
            {
                throw new ConfigurationException(_messages.Get(MessageKeys.CouldNotReturnConfigurations), ex);
            }
        }

        public IList<ConfigurationParametersInfo> GetAllConfigurationParameters()
        {
            try
            {
                var configurations = ConfigurationJson.ConfigurationParameters;
                return configurations.Select(a => new ConfigurationParametersInfo
                ((a.Info?.Id).EmptyIfNull(), a.Info?.Version ?? new Version(), 
                a.ConfigurationRulesId.EmptyIfNull(), (a.Info?.Description).EmptyIfNull())).ToList();
            }
            catch (Exception ex)
            {
                throw new ConfigurationException(_messages.Get(MessageKeys.CouldNotReturnProjectConfigs), ex);
            }
        }

        public void AddConfigurationRules(byte[] content)
        {
            if (content is null)
                throw new ArgumentNullException(nameof(content));
            try
            {
                var configuration = ConfigurationJson;
                string contentStr = Encoding.UTF8.GetString(content);
                var configurationRules = JsonConvert.DeserializeObject<ConfigurationRulesJson>(contentStr);
                if (configuration.ConfigurationRules.Any(a => a.Info!.Id == configurationRules.Info!.Id))
                    throw new ConfigurationException(
                        _messages.Get(MessageKeys.ConfigurationAlreadyExist, configurationRules.Info!.Id));
                configuration.ConfigurationRules!.Add(configurationRules);
                OnConfigurationRulesChanged(configurationRules, false);
                if (configuration != ConfigurationJson)
                {
                    try { AddConfigurationRules(content); }
                    catch (ConfigurationException ex) { throw ex.InnerException; }
                }
            }
            catch (Exception ex)
            {
                throw new ConfigurationException(_messages.Get(MessageKeys.CouldNotAddConfiguration), ex);
            }
        }

        public void AddConfigurationRules(string filePath)
        {
            if (filePath is null)
                throw new ArgumentNullException(nameof(filePath));
            byte[]? fileContent = null;
            try { fileContent = File.ReadAllBytes(filePath); }
            catch (Exception ex)
            {
                throw new ConfigurationException(_messages.Get(MessageKeys.CouldNotAddConfiguration), ex);
            }
            AddConfigurationRules(fileContent);
        }

        public void AddConfigurationParameters(byte[] content)
        {
            if (content is null)
                throw new ArgumentNullException(nameof(content));
            try
            {
                var configuration = ConfigurationJson;
                string contentStr = Encoding.UTF8.GetString(content);
                var configurationParameters = JsonConvert.DeserializeObject<ConfigurationParametersJson>(contentStr);
                if (configuration.ConfigurationRules.Any(a => a.Info!.Id == configurationParameters.Info!.Id))
                    throw new ConfigurationException(
                        _messages.Get(MessageKeys.ProjectConfigurationAlreadyExist, configurationParameters.Info!.Id));
                configuration.ConfigurationParameters!.Add(configurationParameters);
                OnConfigurationParametersChanged(configurationParameters, false);
                if (configuration != ConfigurationJson)
                {
                    try { AddConfigurationParameters(content); }
                    catch (ConfigurationException ex) { throw ex.InnerException; }
                }
            }
            catch (Exception ex)
            {
                throw new ConfigurationException(_messages.Get(MessageKeys.CouldNotAddProjectConfig), ex);
            }
        }

        public void AddConfigurationParameters(string filePath)
        {
            if (filePath is null)
                throw new ArgumentNullException(nameof(filePath));
            byte[]? fileContent = null;
            try { fileContent = File.ReadAllBytes(filePath); }
            catch (Exception ex)
            {
                throw new ConfigurationException(_messages.Get(MessageKeys.CouldNotAddProjectConfig), ex);
            }
            AddConfigurationParameters(fileContent);
        }

        public void RemoveConfigurationRules(string id)
        {
            if (id is null)
                throw new ArgumentNullException(nameof(id));
            try
            {
                var configuration = ConfigurationJson;
                var configurationRules = configuration.ConfigurationRules.
                    FirstOrDefault(a => a.Info?.Id == id);
                if (configurationRules == null)
                    throw new ConfigurationException(_messages.Get(MessageKeys.ConfigurationNotExist, id));
                configuration.ConfigurationRules!.Remove(configurationRules);
                OnConfigurationRulesChanged(configurationRules, true);
                if (configuration != ConfigurationJson)
                {
                    try { RemoveConfigurationRules(id); }
                    catch (ConfigurationException ex) { throw ex.InnerException; }
                }
            }
            catch (Exception ex)
            {
                throw new ConfigurationException(_messages.Get(MessageKeys.CouldNotRemoveConfiguration, ex));
            }
        }

        public void RemoveConfigurationParameters(string id)
        {
            if (id is null)
                throw new ArgumentNullException(nameof(id));
            try
            {
                var userConfiguration = ConfigurationJson;
                var projectConfiguration = userConfiguration.ConfigurationParameters.
                    FirstOrDefault(a => a.Info!.Id == id);
                if (projectConfiguration == null)
                    throw new ConfigurationException(_messages.Get(MessageKeys.ProjectConfigurationNotExist, id));
                userConfiguration.ConfigurationParameters!.Remove(projectConfiguration);
                OnConfigurationParametersChanged(projectConfiguration, true);
                if (userConfiguration != ConfigurationJson)
                {
                    try { RemoveConfigurationParameters(id); }
                    catch (ConfigurationException ex) { throw ex.InnerException; }
                }
            }
            catch (Exception ex)
            {
                throw new ConfigurationException(_messages.Get(MessageKeys.CouldNotRemoveProjectConfig), ex);
            }
        }

        #region OnChanged 
        private void OnConfigurationRulesChanged(ConfigurationRulesJson configurationRules, bool isRemove)
        {
            var configurationJson = ConfigurationJson;
            if (isRemove && configurationJson.ConfigurationRulesId == configurationRules.Info?.Id)
                configurationJson.ConfigurationRulesId = null;
            else if (!isRemove && !string.IsNullOrEmpty(configurationJson.ConfigurationParametersId)
                && configurationJson.ConfigurationParameters.First(a => a.Info?.Id ==
                    configurationJson.ConfigurationParametersId).ConfigurationRulesId == configurationRules.Info?.Id)
                configurationJson.ConfigurationRulesId = configurationRules.Info?.Id;
            OnConfigurationChanged();
        }

        private void OnConfigurationParametersChanged(ConfigurationParametersJson configurationParameters, bool isRemove)
        {
            var configurationJson = ConfigurationJson;
            if (isRemove && !string.IsNullOrEmpty(configurationJson.ConfigurationParametersId)
                && configurationJson.ConfigurationParametersId == configurationParameters.Info?.Id)
                configurationJson.ConfigurationParametersId = null;
            OnConfigurationChanged();
        }

        private void OnConfigurationChanged() => _configurationProvider.Set(ConfigurationJson);
        #endregion 
    }
}
