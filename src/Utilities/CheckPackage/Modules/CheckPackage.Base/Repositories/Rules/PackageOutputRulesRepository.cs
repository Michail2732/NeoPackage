using CheckPackage.Base.Extensions;
using CheckPackage.Configuration.Json.Binder;
using CheckPackage.PackageOutput.Rules;
using Package.Abstraction.Services;
using Package.Configuration.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckPackage.Base.Repositories
{
    public class PackageOutputRulesRepository : RepositoryWithCache<PackageOutputRule, string>
    {
        private readonly IJsonToCommandBindService _commandBinders;

        public PackageOutputRulesRepository(IConfigurationReader configuration, IJsonToCommandBindService commandBinders) :
            base(configuration)
        {
            _commandBinders = commandBinders ?? throw new ArgumentNullException(nameof(commandBinders));
        }

        protected override List<PackageOutputRule> GetAllInternal()
        {
            var rules = _configuration.GetCurrentRule();
            return rules.Output?.PackageOutputRules.Select(a => new PackageOutputRule(
                Guid.NewGuid().ToString(),
                a.Conditions?.Select(b => _commandBinders.Bind(b)).ToList(),
                _commandBinders.Bind(a.Output),
                a.State
                )).ToList() ?? throw new ConfigurationException($"Could not find package output rules in configuration rules id = {rules.Info?.Id}");
        }
    }
}
