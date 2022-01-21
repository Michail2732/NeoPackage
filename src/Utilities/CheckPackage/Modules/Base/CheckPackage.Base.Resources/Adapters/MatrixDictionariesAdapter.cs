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
    public class MatrixDictionariesAdapter : IConfigurationAdapter<MatrixDictionary>
    {
        private readonly IConfigurationServiceLow _configuration;
        private readonly IJsonConverterFacade _converter;
        private readonly MessagesService _messages;

        public MatrixDictionariesAdapter(IConfigurationServiceLow configuration, 
            IJsonConverterFacade converter, MessagesService messages)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
            _messages = messages ?? throw new ArgumentNullException(nameof(messages));
        }

        public IList<MatrixDictionary> Get()
        {            
            var dictionaries = _configuration.GetDictionaries();            
            if (dictionaries.ContainsKey(ConstantsKeys.MatrixDictionaryDictKey))
                throw new ConfigurationException(_messages.Get(MessageKeys.NotFoundMatrhxDicts,
                    ConstantsKeys.MatrixDictionaryDictKey));
            try
            {
                return dictionaries[ConstantsKeys.MatrixDictionaryDictKey]
                    .ToObject<Dictionary<string, Dictionary<string, List<string>>>>()
                    .Select(a => new MatrixDictionary(a.Value, a.Key)).ToList();
            }
            catch (JsonException ex)
            {
                throw new ConfigurationException(_messages.Get(MessageKeys.IncorrectSectionStruct, 
                    ConstantsKeys.MatrixDictionaryDictKey, ex));
            }
        }
        
    }
}
