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
    public class RegexTemplatesAdapter : IConfigurationAdapter<Core.Regex.GroupParametersTemplateResource>
    {
        private readonly IConfigurationServiceLow _configuration;
        private readonly IJsonConverterFacade _converter;
        private readonly MessagesService _messages;

        public RegexTemplatesAdapter(IConfigurationServiceLow configuration, 
            IJsonConverterFacade converter, MessagesService messages)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
            _messages = messages ?? throw new ArgumentNullException(nameof(messages));
        }

        public IList<Core.Regex.GroupParametersTemplateResource> Get()
        {            
            var dictionaries = _configuration.GetDictionaries();
            if (dictionaries.ContainsKey(ConstantsKeys.RegexTemplateDictKey))
                throw new ConfigurationException(_messages.Get(MessageKeys.NotFoundRegexTemplates,
                    ConstantsKeys.RegexTemplateDictKey));
            try
            {
                var converters = _converter.GetConverters();
                var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(
                    dictionaries[ConstantsKeys.RegexTemplateDictKey].ToString(), converters);
                return result.Select(a => new GroupParametersTemplateResource(a.Key, a.Value)).ToList();
            }
            catch (JsonException ex)
            {
                throw new ConfigurationException($"Incorrect struct of section" +
                    $" \"{ConstantsKeys.RegexTemplateDictKey}\"", ex);
            }
        }        
    }
}
