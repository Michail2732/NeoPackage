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
    public class ParameterOuputRulesRepository : RepositoryWithCache<ParameterOutputRule, string>
    {
        private readonly IJsonToCommandBindService _commandBinders;

        public ParameterOuputRulesRepository(IConfigurationReader configuration, IJsonToCommandBindService commandBinders) :
            base(configuration)
        {
            _commandBinders = commandBinders ?? throw new ArgumentNullException(nameof(commandBinders));
        }

        protected override List<ParameterOutputRule> GetAllInternal()
        {
            var rules = _configuration.GetCurrentRule();
            return rules.Output?.ParameterOutputRules.Select(a => new ParameterOutputRule(
                Guid.NewGuid().ToString(),
                a.ParameterId,
                a.IsUserParameter,
                a.State,
                _commandBinders.Bind(a.Output)                
                )).ToList() ?? throw new ConfigurationException($"Could not find parameter check rules in configuration rules id = {rules.Info?.Id}");
        }
    }
}
