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
    public class EntityOuputRulesRepository : RepositoryWithCache<EntityOutputRule, string>
    {        
        private readonly IJsonToCommandBindService _commandBinders;

        public EntityOuputRulesRepository(IConfigurationReader configuration, IJsonToCommandBindService commandBinders):
            base(configuration)
        {            
            _commandBinders = commandBinders ?? throw new ArgumentNullException(nameof(commandBinders));
        }

        protected override List<EntityOutputRule> GetAllInternal()
        {
            var rules = _configuration.GetCurrentRule();
            return rules.Output?.EntitiesOutputRules.Select(a => new EntityOutputRule(
                Guid.NewGuid().ToString(),
                a.State,
                a.Conditions?.Select(b => _commandBinders.Bind(b)).ToList(),
                _commandBinders.Bind(a.Output)
            )).ToList() ?? throw new ConfigurationException($"Could not find entity output rules in configuration rules id = {rules.Info?.Id}"); 
        }
    }
}
