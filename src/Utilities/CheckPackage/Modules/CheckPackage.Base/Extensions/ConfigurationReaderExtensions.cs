using CheckPackage.Configuration.Json.Entities;
using Package.Abstraction.Services;
using Package.Configuration.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Base.Extensions
{
    public static class ConfigurationReaderExtensions
    {
        public static ConfigurationRulesJson GetCurrentRule(this IConfigurationReader reader)
        {
            var packageConfiguration = reader.Get<PackageConfigurationJson>();
            var rulesConfiguration = reader.Get<AggregateConfigurationJson>();
            return rulesConfiguration.Rules?.Find(a => a.Info?.Id == packageConfiguration.RulesId) ??
                throw new ConfigurationException($"Could not find rules with id = {packageConfiguration.RulesId}");            
        }

    }
}
