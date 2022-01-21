using CheckPackage.Base.Configuration;
using CheckPackage.Configuration.Converters;
using CheckPackage.Configuration.Exceptions;
using CheckPackage.Configuration.Services;
using CheckPackage.Core.Validation;
using CheckPackage.Core.Extensions;
using Newtonsoft.Json;
using Package.Localization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckPackage.Base.Resource
{
    /// todo: IsPackageCheck в класск проверки, надо добавить ещё раздел в конфиг для проверки пакета целиком
    public class EntityCheckRuleAdapter : IConfigurationAdapter<EntityCheckRule>
    {
        private readonly IConfigurationServiceLow _configuration;
        private readonly IJsonConverterFacade _converter;
        private readonly MessagesService _messages;

        public EntityCheckRuleAdapter(IConfigurationServiceLow configuration, 
            IJsonConverterFacade converter, MessagesService messages)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
            _messages = messages ?? throw new ArgumentNullException(nameof(messages));
        }

        public IList<EntityCheckRule> Get()
        {            
            var checks = _configuration.GetChecks();            
            if (checks.ContainsKey(ConstantsKeys.EntityCheckKey))
                throw new ConfigurationException(_messages.Get(MessageKeys.NotFoundEntityChecks,
                    ConstantsKeys.EntityCheckKey));
            try
            {
                var converters = _converter.GetConverters();
                var result = JsonConvert.DeserializeObject<List<EntityCheckRuleJson>>(
                    checks[ConstantsKeys.EntityCheckKey].ToString(), converters);
                return result.Select(a => new EntityCheckRule(
                    id: Guid.NewGuid().ToString(),
                    a.Critical,
                    conditions: a.Conditions.Select(b => _converter.ConditionConvert(b)).RollUp(),
                    checks: a.Checks.Select(b => _converter.CheckConvert(b)).RollUp() ?? throw new JsonException(_messages.Get(MessageKeys.NotSetProperty, "Checks")),
                    false
                    )).ToList();                
            }
            catch (JsonException ex)
            {
                throw new ConfigurationException(_messages.Get(MessageKeys.IncorrectSectionStruct,
                    ConstantsKeys.EntityCheckKey), ex);
            }
        }        
    }
}
