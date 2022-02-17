using CheckPackage.Base.Extensions;
using CheckPackage.Configuration.Json.Binder;
using CheckPackage.PackageBuilding.Rules;
using Package.Abstraction.Services;
using Package.Configuration.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckPackage.Base.Repositories
{
    public class EntityBuildRulesRepository : RepositoryWithCache<EntityBuildRule, string>
    {        
        private readonly IJsonToCommandBindService _commandBinders;

        public EntityBuildRulesRepository(IConfigurationReader configuration, IJsonToCommandBindService commandBinders):
            base(configuration)
        {            
            _commandBinders = commandBinders ?? throw new ArgumentNullException(nameof(commandBinders));
        }

        protected override List<EntityBuildRule> GetAllInternal()
        {
            var rules = _configuration.GetCurrentRule();
            return rules.Building?.EntityBuildRules.Select(a => 
                new EntityBuildRule
                (
                    Guid.NewGuid().ToString(),
                    a.EntityLevel, 
                    a.Priority,
                    a.GroupBy,
                    a.Conditions.Select( b => _commandBinders.Bind(b)).ToList(),
                    a.ParameterRules.Select(b => 
                        new ParameterBuildRule
                        (
                            Guid.NewGuid().ToString(), 
                            b.Conditions?.Select(c => _commandBinders.Bind(c)).ToList(), 
                            _commandBinders.Bind(b.Selector),
                            _commandBinders.Bind(b.Extracter)
                         )
                    ).ToList()
                 )).ToList() ?? throw new ConfigurationException($"Could not find entity building rules in configuration rules id = {rules.Info?.Id}");
        }
        
    }
}
