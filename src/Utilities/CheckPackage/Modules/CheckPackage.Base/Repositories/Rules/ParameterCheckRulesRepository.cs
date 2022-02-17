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
    public class ParameterCheckRulesRepository : RepositoryWithCache<ParameterCheckRule, string>
    {
        private readonly IJsonToCommandBindService _commandBinders;

        public ParameterCheckRulesRepository(IConfigurationReader configuration, IJsonToCommandBindService commandBinders) :
            base(configuration)
        {
            _commandBinders = commandBinders ?? throw new ArgumentNullException(nameof(commandBinders));
        }

        protected override List<ParameterCheckRule> GetAllInternal()
        {
            var rules = _configuration.GetCurrentRule();
            return rules.Check?.ParameterCheckRules.Select(a => new ParameterCheckRule(
                Guid.NewGuid().ToString(),
                a.ParameterId,
                a.Critical,
                a.Checks.Select(b => _commandBinders.Bind(b)).ToList(),
                a.IsUserParameter
                )).ToList() ?? throw new ConfigurationException($"Could not find parameter check rules in configuration rules id = {rules.Info?.Id}");
        }
    }
}
