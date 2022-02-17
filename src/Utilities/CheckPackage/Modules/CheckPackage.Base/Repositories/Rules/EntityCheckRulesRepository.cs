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
    public class EntityCheckRulesRepository : RepositoryWithCache<EntityCheckRule, string>
    {        
        private readonly IJsonToCommandBindService _commandBinders;

        public EntityCheckRulesRepository(IConfigurationReader configuration, IJsonToCommandBindService commandBinders):
            base(configuration)
        {            
            _commandBinders = commandBinders ?? throw new ArgumentNullException(nameof(commandBinders));
        }

        protected override List<EntityCheckRule> GetAllInternal()
        {
            var rules = _configuration.GetCurrentRule();
            return rules.Check?.EntityCheckRules.Select(a => new EntityCheckRule(
                Guid.NewGuid().ToString(),
                (a.Conditions == null ? null : a.Conditions.Select(b => _commandBinders.Bind(b)).ToList()),
                a.Checks.Select(b => _commandBinders.Bind(b)).ToList(),
                a.State
            )).ToList() ?? throw new ConfigurationException($"Could not find entity check rules in configuration rules id = {rules.Info?.Id}"); 
        }
        
    }
}
