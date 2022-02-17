
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Package.Configuration.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;

namespace Package.Configuration.Services
{
    public class JsonConfigurationProvider: IJsonConfigurationProvider
    {                
        private JObject? _configurationJson;
        private object _sync = new object();

        public static readonly JsonConfigurationProvider Instance = new JsonConfigurationProvider();

        private JsonConfigurationProvider()
        {
     
        }

        public JObject Get()
        {
            try
            {
                lock (_sync)
                {
                    if (_configurationJson == null)
                    {
                        string fileContent = File.ReadAllText(GetFilePath());
                        _configurationJson = JsonConvert.DeserializeObject<JObject>(fileContent) ?? new JObject();
                    }                        
                    return _configurationJson;
                }                
            }
            catch (Exception ex)
            {
                throw new ConfigurationException($"Could not extract configurations: {ex.Message}", ex);                
            }            
        }

        public void Set(JObject configuration)
        {
            try
            {
                lock (_sync)
                {
                    string filePath = GetFilePath();                    
                    string userConfigurationJson = JsonConvert.SerializeObject(configuration);
                    File.WriteAllText(filePath, userConfigurationJson);
                    _configurationJson = null;
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
                $"Package_{Environment.UserName ?? "anonymous"}");
            string useConfigurationPath = Path.Combine(userConfigurationDir, "Package.json");
            if (!Directory.Exists(userConfigurationDir))
                Directory.CreateDirectory(userConfigurationDir);
            if (!File.Exists(useConfigurationPath))
                using (File.Create(useConfigurationPath)) { } ;
            return useConfigurationPath;
        }        
    }
}
