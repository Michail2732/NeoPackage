using CheckPackage.Base.Configuration;
using CheckPackage.Configuration.Converters;
using CheckPackage.Configuration.Exceptions;
using CheckPackage.Configuration.Services;
using CheckPackage.Core.Regex;
using Newtonsoft.Json;
using Package.Localization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckPackage.Base.Resource
{
    public class ParameterTemplatesAdapter : IConfigurationAdapter<Core.Regex.ParameterTemplateResource>
    {
        private readonly IConfigurationServiceLow _configuration;
        private readonly IJsonConverterFacade _converter;
        private readonly MessagesService _messages;

        public ParameterTemplatesAdapter(IConfigurationServiceLow configuration, 
            IJsonConverterFacade converter, MessagesService messages)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
            _messages = messages ?? throw new ArgumentNullException(nameof(messages));
        }

        public IList<Core.Regex.ParameterTemplateResource> Get()
        {            
            var dictionaries = _configuration.GetDictionaries();
            if (dictionaries.ContainsKey(ConstantsKeys.ParameterTemplateyDictKey))
                throw new ConfigurationException(_messages.Get(MessageKeys.NotFoundParameterTemplates,
                    ConstantsKeys.ParameterTemplateyDictKey));
            try
            {
                var converters = _converter.GetConverters();
                var result = JsonConvert.DeserializeObject<Dictionary<string, ParameterTemplateJson>>(
                    dictionaries[ConstantsKeys.ParameterTemplateyDictKey].ToString(), converters);
                return result.Select(a => new ParameterTemplateResource(a.Key,
                    a.Value.RegexPattern!, a.Value.Description!)).ToList();
            }
            catch (JsonException ex)
            {
                throw new ConfigurationException(_messages.Get(MessageKeys.IncorrectSectionStruct, 
                    ConstantsKeys.ParameterTemplateyDictKey), ex);
            }
        }        
    }
}
