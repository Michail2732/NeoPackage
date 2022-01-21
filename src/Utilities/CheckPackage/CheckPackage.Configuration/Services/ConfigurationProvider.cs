using CheckPackage.Configuration.Entities;
using CheckPackage.Configuration.Exceptions;
using CheckPackage.Configuration.Utilities;
using Newtonsoft.Json;
using System;
using System.IO;

namespace CheckPackage.Configuration.Services
{
    internal class ConfigurationProvider
    {        
        public event Action? ConfigurationChanged;
        private object _sync = new object();

        public static readonly ConfigurationProvider Instance = new ConfigurationProvider();

        private ConfigurationProvider()
        {
     
        }

        public ConfigurationJson Get()
        {
            try
            {
                lock (_sync)
                {
                    string filePath = GetFilePath();
                    var userConfiguration = JsonConvert.DeserializeObject<ConfigurationJson>(filePath);
                    if (userConfiguration == null)
                    {
                        userConfiguration = ConfigurationJsonInitializer.Default;
                        Set(userConfiguration);
                    }                       
                    return userConfiguration;
                }                
            }
            catch (Exception ex)
            {
                throw new ConfigurationException($"Could not extract configurations: {ex.Message}", ex);                
            }            
        }

        public void Set(ConfigurationJson configuration)
        {
            try
            {
                lock (_sync)
                {
                    string filePath = GetFilePath();
                    string userConfigurationJson = JsonConvert.SerializeObject(configuration);
                    File.WriteAllText(filePath, userConfigurationJson);
                    OnConfigurationChanged();
                }                
            }
            catch (Exception ex)
            {
                throw new ConfigurationException($"Could not save configuration: {ex.Message}", ex);
            }
        }

        private static string GetFilePath()
        {
            string userConfigurationDir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                $"CheckPackage_{Environment.UserName ?? "anonymous"}");
            string useConfigurationPath = Path.Combine(userConfigurationDir,
                            "Checkpackage.json");
            if (!Directory.Exists(userConfigurationDir))
                Directory.CreateDirectory(userConfigurationDir);
            return useConfigurationPath;
        }

        private void OnConfigurationChanged()
        {
            var evnt = ConfigurationChanged;
            if (evnt != null)
                evnt.Invoke();
        }
    }
}
