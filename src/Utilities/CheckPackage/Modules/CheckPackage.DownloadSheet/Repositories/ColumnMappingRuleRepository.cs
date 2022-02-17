using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CheckPackage.Base.Extensions;
using CheckPackage.Base.Repositories;
using CheckPackage.Configuration.Json.Binder;
using CheckPackage.DownloadSheet.Configuration;
using CheckPackage.DownloadSheet.Entities;
using Package.Abstraction.Services;
using Package.Configuration.Exceptions;

namespace CheckPackage.DownloadSheet.Repositories
{
    public class ColumnMappingRuleRepository : RepositoryWithCache<ColumnMappingRule, string>
    {
        private readonly IJsonToCommandBindService _binder;

        public ColumnMappingRuleRepository(IConfigurationReader configuration, 
            IJsonToCommandBindService binder) : base(configuration)
        {
            _binder = binder ?? throw new ArgumentNullException(nameof(binder));
        }

        protected override List<ColumnMappingRule> GetAllInternal()
        {
            var rules = _configuration.GetCurrentRule();
            var mappingRules = (LoadlistColumnMapResourceJson?)rules.Resources?.Find(a => a.GetType() == typeof(LoadlistColumnMapResourceJson)) ??
                throw new ConfigurationException($"Could not find column maps in rules id = {rules.Info?.Id}");
            return mappingRules.ColumnRules.Select(a => new ColumnMappingRule(
                a.Key, 
                a.Value.Column, 
                a.Value.Name, 
                _binder.Bind(a.Value.Selector),
                _binder.Bind(a.Value.Extractor))).ToList();
        }
    }
}
