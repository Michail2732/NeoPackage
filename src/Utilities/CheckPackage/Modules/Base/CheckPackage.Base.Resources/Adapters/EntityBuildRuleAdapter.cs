using CheckPackage.Base.Configuration;
using CheckPackage.Base.Extracters;
using CheckPackage.Configuration.Converters;
using CheckPackage.Configuration.Exceptions;
using CheckPackage.Configuration.Services;
using CheckPackage.Core.Building;
using CheckPackage.Core.Extensions;
using Newtonsoft.Json;
using Package.Abstraction.Extensions;
using Package.Localization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckPackage.Base.Resource
{
    public class EntityBuildRuleAdapter : IConfigurationAdapter<Core.Building.EntityBuildRuleResource>
    {
        private readonly IConfigurationServiceLow _configuration;
        private readonly IJsonConverterFacade _converter;
        private readonly MessagesService _messages;

        public EntityBuildRuleAdapter(IConfigurationServiceLow configuration, 
            IJsonConverterFacade converter, MessagesService messages)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
            _messages = messages ?? throw new ArgumentNullException(nameof(messages));
        }

        public IList<Core.Building.EntityBuildRuleResource> Get()
        {            
            var rules = _configuration.GetRules();                      
            if (rules.ContainsKey(ConstantsKeys.EntityBuildRuleKey))
                throw new ConfigurationException(_messages.Get(MessageKeys.NotFoundEntityBuild, ConstantsKeys.EntityBuildRuleKey));
            try
            {
                var result = JsonConvert.DeserializeObject<List<EntityBuildRuleJson>>(
                    rules[ConstantsKeys.EntityBuildRuleKey].ToString(), _converter.GetConverters());                    
                return result.Select(a => new Core.Building.EntityBuildRuleResource(Guid.NewGuid().ToString(),
                    a.EntityLevel, a.Priority, a.GroupBy,
                    a.Conditions.Select<CheckPackage.Configuration.Entities.BaseConditionJson,ConditionInfo>(b => _converter.ConditionConvert(b)).RollUp(),
                    a.Parameters!.Extracts.Select(b => 
                            new ParameterBuildRule(
                                     b.Conditions.Select<CheckPackage.Configuration.Entities.BaseConditionJson,ConditionInfo>(c => _converter.ConditionConvert(c)).RollUp(),
                                    _converter.ExtractConvert(b.Extracter!), Guid.NewGuid().ToString())).ToList()
                                            .AppendToEnd( c => c.Statics.Select(d =>
                                                new ParameterBuildRule(d.Conditions
                                                    .Select<CheckPackage.Configuration.Entities.BaseConditionJson,ConditionInfo>(e => _converter.ConditionConvert(e)).RollUp()
                                                    , new StaticParameterValueExtractDto(
                                                        d.ParameterId ?? throw new JsonException(_messages.Get(MessageKeys.NotSetProperty, "ParameterId")), 
                                                        string.Empty,
                                                        d.Value.EmptyIfNull()
                                                        ), Guid.NewGuid().ToString())),
                                              a.Parameters))).ToList();                

            }
            catch (JsonException ex)
            {
                throw new ConfigurationException(_messages.Get(MessageKeys.IncorrectSectionStruct, 
                    ConstantsKeys.EntityBuildRuleKey), ex);
            }
        }        
    }
}
