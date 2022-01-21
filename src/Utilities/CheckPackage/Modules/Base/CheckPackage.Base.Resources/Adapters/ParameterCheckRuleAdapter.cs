using CheckPackage.Base.Configuration;
using CheckPackage.Configuration.Converters;
using CheckPackage.Configuration.Exceptions;
using CheckPackage.Configuration.Services;
using CheckPackage.Core.Extensions;
using CheckPackage.Core.Validation;
using Newtonsoft.Json;
using Package.Localization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckPackage.Base.Resource
{
    public class ParameterCheckRuleAdapter : IConfigurationAdapter<ParameterCheckRule>
    {
        private readonly IConfigurationServiceLow _configuration;
        private readonly IJsonConverterFacade _converter;
        private readonly MessagesService _messages;

        public ParameterCheckRuleAdapter(IConfigurationServiceLow configuration, 
            IJsonConverterFacade converter, MessagesService messages)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
            _messages = messages ?? throw new ArgumentNullException(nameof(messages));
        }

        public IList<ParameterCheckRule> Get()
        {            
            var checks = _configuration.GetChecks();            
            if (checks.ContainsKey(ConstantsKeys.ParameterCheckKey))
                throw new ConfigurationException(_messages.Get(MessageKeys.NotFoundParameterCheck, ConstantsKeys.ParameterCheckKey));
            try
            {
                var converters = _converter.GetConverters();
                var result = JsonConvert.DeserializeObject<List<ParameterCheckRuleJson>>(
                    checks[ConstantsKeys.ParameterCheckKey].ToString(), converters);
                return result.Select(a => new ParameterCheckRule
                (
                    id : Guid.NewGuid().ToString(),   
                    critical: a.Critical,
                    parameterId : a.ParameterId ?? throw new JsonException(_messages.Get(MessageKeys.NotSetProperty, "ParameterId")),
                    checks: a.Checks.Select(b => _converter.CheckConvert(b)).RollUp() ?? throw new JsonException(_messages.Get(MessageKeys.NotSetProperty, "Checks")), 
                    isCustomParameter: a.IsCustomParameter
                )).ToList();

            }
            catch (JsonException ex)
            {
                throw new ConfigurationException(_messages.Get(MessageKeys.IncorrectSectionStruct, 
                    ConstantsKeys.ParameterCheckKey), ex);
            }
        }
        
    }
}
