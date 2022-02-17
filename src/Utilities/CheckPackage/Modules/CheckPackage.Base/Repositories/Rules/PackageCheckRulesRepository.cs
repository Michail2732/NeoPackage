using CheckPackage.Base.Extensions;
using CheckPackage.Configuration.Json.Binder;
using CheckPackage.PackageValidation.Rules;
using Package.Abstraction.Services;
using Package.Configuration.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckPackage.Base.Repositories
{
    public class PackageCheckRulesRepository : RepositoryWithCache<PackageCheckRule, string>
    {        
        private readonly IJsonToCommandBindService _commandBinders;

        public PackageCheckRulesRepository(IConfigurationReader configuration, IJsonToCommandBindService commandBinders): 
            base(configuration)
        {     
            _commandBinders = commandBinders ?? throw new ArgumentNullException(nameof(commandBinders));
        }

        protected override List<PackageCheckRule> GetAllInternal()
        {
            var rules = _configuration.GetCurrentRule();
            return rules.Check?.PackageCheckRules.Select(a => new PackageCheckRule(
                Guid.NewGuid().ToString(),
                a.Conditions?.Select(b => _commandBinders.Bind(b)).ToList(),
                a.Checks.Select(b => _commandBinders.Bind(b)).ToList(),
                a.State
             )).ToList() ?? throw new ConfigurationException($"Could not find package check rules in configuration rules id = {rules.Info?.Id}");
        }        
    }
}
