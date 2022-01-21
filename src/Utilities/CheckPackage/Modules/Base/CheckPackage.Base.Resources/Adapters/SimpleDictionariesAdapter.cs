using CheckPackage.Configuration.Converters;
using CheckPackage.Configuration.Exceptions;
using CheckPackage.Configuration.Services;
using CheckPackage.Core.Checks;
using Newtonsoft.Json;
using Package.Localization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckPackage.Base.Resource
{
    public class SimpleDictionariesAdapter : IConfigurationAdapter<Core.Checks.SimpleDictionaryResource>
    {
        private readonly IConfigurationServiceLow _configuration;
        private readonly IJsonConverterFacade _converter;
        private readonly MessagesService _messages;

        public SimpleDictionariesAdapter(IConfigurationServiceLow configuration, 
            IJsonConverterFacade converter, MessagesService messages)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
            _messages = messages ?? throw new ArgumentNullException(nameof(messages));
        }

        public IList<Core.Checks.SimpleDictionaryResource> Get()
        {            
            var dictionaries = _configuration.GetDictionaries();
            if (dictionaries.ContainsKey(ConstantsKeys.SimpleDictionaryDictKey))
                throw new ConfigurationException(_messages.Get(MessageKeys.NotFoundSectionDict,
                    ConstantsKeys.SimpleDictionaryDictKey));
            try
            {
                var converters = _converter.GetConverters();
                var result = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(
                    dictionaries[ConstantsKeys.SimpleDictionaryDictKey].ToString(), converters);
                return result.Select(a => new SimpleDictionaryResource(a.Value, a.Key)).ToList();
            }
            catch (JsonException ex)
            {
                throw new ConfigurationException(_messages.Get(MessageKeys.IncorrectSectionStruct, 
                    ConstantsKeys.SimpleDictionaryDictKey), ex);
            }
        }        
    }
}
