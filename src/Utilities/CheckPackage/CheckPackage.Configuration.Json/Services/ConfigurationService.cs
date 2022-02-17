using CheckPackage.Configuration.Json.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Package.Abstraction.Extensions;
using Package.Localization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CheckPackage.Configuration.Json.Services
{
    //public sealed class ConfigurationService : IConfigurationService
    //{
    //    private readonly MessagesService _messages;
    //    private readonly IConfigurationProvider _configurationProvider;

    //    public ConfigurationService(MessagesService messages, IConfigurationProvider configurationProvider)
    //    {
    //        _messages = messages ?? throw new ArgumentNullException(nameof(messages));
    //        _configurationProvider = configurationProvider ?? throw new ArgumentNullException(nameof(configurationProvider));
    //    }                

    //    public ConfigurationRulesInfo GetConfigurationRules(string id)
    //    {            
    //        try
    //        {
    //            var configuration = _configurationProvider.Get();
    //            var configurationRules = configuration.Rules.FirstOrDefault(a => a.Info!.Id == id);
    //            if (configurationRules == null)
    //                throw new ConfigurationException(_messages.Get(MessageKeys.ConfigurationNotExist, id));
    //            return new ConfigurationRulesInfo((configurationRules.Info?.Id).EmptyIfNull(),
    //                configurationRules.Info?.Version ?? new Version(),
    //                configuration.RulesHashes[configurationRules.Info.Id].EmptyIfNull(),
    //                (configurationRules.Info?.Description).EmptyIfNull());
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new ConfigurationException(_messages.Get(MessageKeys.CouldNotReturnConfiguration), ex);
    //        }
    //    }

    //    public ConfigurationParametersInfo GetConfigurationParameters(string id)
    //    {
    //        try
    //        {
    //            var configuration = _configurationProvider.Get();
    //            var configurationParams = configuration.Parameters.FirstOrDefault(a => a.Info!.Id == id);
    //            if (configurationParams == null)
    //                throw new ConfigurationException(_messages.Get(MessageKeys.ProjectConfigurationNotExist, id));
    //            return new ConfigurationParametersInfo((configurationParams.Info?.Id).EmptyIfNull(),
    //                configurationParams.Info?.Version ?? new Version(), 
    //                configurationParams.ConfigurationRulesId.EmptyIfNull(), 
    //                configuration.ParametersHashes[configurationParams.Info.Id].EmptyIfNull(),
    //                (configurationParams.Info?.Description).EmptyIfNull());
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new ConfigurationException(_messages.Get(MessageKeys.CouldNotReturnProjectConfig), ex);
    //        }
    //    }

    //    public bool HasConfigurationRules(string id)
    //    {            
    //        try
    //        {
    //            var configuration = _configurationProvider.Get();
    //            return configuration.Rules.Any(a => a.Info!.Id == id);
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new ConfigurationException(_messages.Get(MessageKeys.CouldNotCheckExistConfig), ex);
    //        }
    //    }

    //    public bool HasConfigurationParameters(string id)
    //    {            
    //        try
    //        {
    //            var configuration = _configurationProvider.Get();
    //            return configuration.Parameters.Any(a => a.Info!.Id == id);
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new ConfigurationException(_messages.Get(MessageKeys.CouldNotCheckExistProjectConfig), ex);
    //        }
    //    }

    //    public IList<ConfigurationRulesInfo> GetAllConfigurationRules()
    //    {
    //        try
    //        {
    //            var configuration = _configurationProvider.Get();                
    //            return configuration.Rules.Select(a => new ConfigurationRulesInfo(
    //                (a.Info.Id).EmptyIfNull(),
    //                a.Info.Version ?? new Version(),
    //                configuration.RulesHashes[a.Info.Id].EmptyIfNull(),
    //                (a.Info.Description).EmptyIfNull())).ToList();
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new ConfigurationException(_messages.Get(MessageKeys.CouldNotReturnConfigurations), ex);
    //        }
    //    }

    //    public IList<ConfigurationParametersInfo> GetAllConfigurationParameters()
    //    {
    //        try
    //        {
    //            var configuration = _configurationProvider.Get();                
    //            return configuration.Parameters.Select(a => new ConfigurationParametersInfo
    //            ((a.Info?.Id).EmptyIfNull(), 
    //            a.Info?.Version ?? new Version(), 
    //            a.ConfigurationRulesId.EmptyIfNull(),
    //            configuration.ParametersHashes[a.Info.Id].EmptyIfNull(),
    //            (a.Info?.Description).EmptyIfNull())).ToList();
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new ConfigurationException(_messages.Get(MessageKeys.CouldNotReturnProjectConfigs), ex);
    //        }
    //    }

    //    public void AddConfigurationRules(byte[] content)
    //    {            
    //        try
    //        {
    //            var configuration = _configurationProvider.Get();
    //            string contentStr = Encoding.UTF8.GetString(content);
    //            var configurationRules = JsonConvert.DeserializeObject<ConfigurationRulesJson>(contentStr);
    //            if (configuration.Rules.Any(a => a.Info!.Id == configurationRules.Info!.Id))
    //                throw new ConfigurationException(
    //                    _messages.Get(MessageKeys.ConfigurationAlreadyExist, configurationRules.Info!.Id));
    //            configuration.Rules.Add(configurationRules);
    //            configuration.RulesHashes[configurationRules.Info.Id] = CreateHash(content);
    //            _configurationProvider.Set(configuration);                            
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new ConfigurationException(_messages.Get(MessageKeys.CouldNotAddConfiguration), ex);
    //        }
    //    }

    //    public void AddConfigurationRules(string filePath)
    //    {            
    //        byte[]? fileContent = null;
    //        try { fileContent = File.ReadAllBytes(filePath); }
    //        catch (Exception ex)
    //        {
    //            throw new ConfigurationException(_messages.Get(MessageKeys.CouldNotAddConfiguration), ex);
    //        }
    //        AddConfigurationRules(fileContent);
    //    }

    //    public void AddConfigurationParameters(byte[] content)
    //    {            
    //        try
    //        {
    //            var configuration = _configurationProvider.Get();
    //            string contentStr = Encoding.UTF8.GetString(content);
    //            var configurationParams = JsonConvert.DeserializeObject<ConfigurationParametersJson>(contentStr);
    //            if (configuration.Rules.Any(a => a.Info!.Id == configurationParams.Info!.Id))
    //                throw new ConfigurationException(
    //                    _messages.Get(MessageKeys.ProjectConfigurationAlreadyExist, configurationParams.Info!.Id));
    //            configuration.Parameters.Add(configurationParams);
    //            configuration.ParametersHashes[configurationParams.Info.Id] = CreateHash(content);
    //            _configurationProvider.Set(configuration);
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new ConfigurationException(_messages.Get(MessageKeys.CouldNotAddProjectConfig), ex);
    //        }
    //    }

    //    public void AddConfigurationParameters(string filePath)
    //    {            
    //        byte[]? fileContent = null;
    //        try { fileContent = File.ReadAllBytes(filePath); }
    //        catch (Exception ex)
    //        {
    //            throw new ConfigurationException(_messages.Get(MessageKeys.CouldNotAddProjectConfig), ex);
    //        }
    //        AddConfigurationParameters(fileContent);
    //    }

    //    public void RemoveConfigurationRules(string id)
    //    {            
    //        try
    //        {
    //            var configuration = _configurationProvider.Get();
    //            var configurationRules = configuration.Rules.FirstOrDefault(a => a.Info?.Id == id);
    //            if (configurationRules == null)
    //                throw new ConfigurationException(_messages.Get(MessageKeys.ConfigurationNotExist, id));
    //            configuration.Rules.Remove(configurationRules);
    //            if (configuration.RulesHashes.ContainsKey(id))
    //                configuration.RulesHashes.Remove(id);
    //            _configurationProvider.Set(configuration);
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new ConfigurationException(_messages.Get(MessageKeys.CouldNotRemoveConfiguration, ex));
    //        }
    //    }

    //    public void RemoveConfigurationParameters(string id)
    //    {            
    //        try
    //        {
    //            var configuration = _configurationProvider.Get();
    //            var configurationParams = configuration.Parameters.FirstOrDefault(a => a.Info!.Id == id);
    //            if (configurationParams == null)
    //                throw new ConfigurationException(_messages.Get(MessageKeys.ProjectConfigurationNotExist, id));
    //            configuration.Parameters.Remove(configurationParams);
    //            if (configuration.ParametersHashes.ContainsKey(id))
    //                configuration.ParametersHashes.Remove(id);
    //            _configurationProvider.Set(configuration);
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new ConfigurationException(_messages.Get(MessageKeys.CouldNotRemoveProjectConfig), ex);
    //        }
    //    }

    //    private string CreateHash(byte[] bytes)
    //    {            
    //       string hashStr = "";
    //       using (SHA256 sha = SHA256.Create())
    //       {
    //           var hash = sha.ComputeHash(bytes);
    //           hashStr = BitConverter.ToString(hash).Replace("-", "");
    //       }
    //       return hashStr;
    //    }

    //}
}
