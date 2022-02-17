using CheckPackage.Base.Extensions;
using CheckPackage.Base.Repositories;
using CheckPackage.Configuration.Json.Binder;
using CheckPackage.DownloadSheet.Configuration;
using CheckPackage.DownloadSheet.Entities;
using Package.Abstraction.Services;
using Package.Configuration.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CheckPackage.DownloadSheet.Repositories
{
    public class RowMappingRuleRepository : RepositoryWithCache<RowMappingRule, string>
    {
        private readonly IJsonToCommandBindService _binder;

        public RowMappingRuleRepository(IConfigurationReader configuration,
            IJsonToCommandBindService binder) : base(configuration)
        {
            _binder = binder ?? throw new ArgumentNullException(nameof(binder));
        }

        protected override List<RowMappingRule> GetAllInternal()
        {
            var rules = _configuration.GetCurrentRule();
            var mappingRules = (LoadlistRowMapResourceJson?)rules.Resources?.Find(a => a.GetType() == typeof(LoadlistRowMapResourceJson)) ??
                throw new ConfigurationException($"Could not find row maps in rules id = {rules.Info?.Id}");
            return mappingRules.RowRules.Select(a => new RowMappingRule(
                a.Priority,
                a.IsVirtual,
                a.EntityLevel,
                a.Conditions?.Select(b => _binder.Bind(b)).ToList(),
                a.ColumnIds)).ToList();                
        }
    }
}
