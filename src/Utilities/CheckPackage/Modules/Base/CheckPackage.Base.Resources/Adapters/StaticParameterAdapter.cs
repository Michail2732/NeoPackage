using CheckPackage.Base.Configuration;
using CheckPackage.Configuration.Converters;
using CheckPackage.Configuration.Exceptions;
using CheckPackage.Configuration.Services;
using CheckPackage.Core.Extracts;
using Newtonsoft.Json;
using Package.Localization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckPackage.Base.Resource
{
    public class StaticParameterAdapter : IConfigurationAdapter<StaticParameter>
    {
        private readonly IConfigurationServiceLow _configuration;
        private readonly IJsonConverterFacade _converter;
        private readonly MessagesService _messages;

        public StaticParameterAdapter(IConfigurationServiceLow configuration,
            IJsonConverterFacade converter, MessagesService messages)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
            _messages = messages ?? throw new ArgumentNullException(nameof(messages));
        }

        public IList<StaticParameter> Get()
        {            
            var dictionaries = _configuration.GetDictionaries();
            if (dictionaries.ContainsKey(ConstantsKeys.StaticPatameterDictKey))
                throw new ConfigurationException(_messages.Get(MessageKeys.NotFoundSectionDict, 
                    ConstantsKeys.StaticPatameterDictKey));
            try
            {
                var converters = _converter.GetConverters();
                var result = JsonConvert.DeserializeObject<Dictionary<string, StaticParameterJson>>(
                    dictionaries[ConstantsKeys.StaticPatameterDictKey].ToString(), converters);
                return result.Select(a => new StaticParameter(a.Key, a.Value.Value!,
                            a.Value.Description!)).ToList();
            }
            catch (JsonException ex)
            {
                throw new ConfigurationException( _messages.Get(MessageKeys.IncorrectSectionStruct, 
                    ConstantsKeys.StaticPatameterDictKey), ex);
            }
        }

        //public void Set(StaticParameter item)
        //{
        //    using (var context = _contextBuilder.BuildLow())
        //    {
        //        var dictionaries = context.GetDictionaratis();
        //        if (dictionaries.ContainsKey(ConstantsKeys.StaticPatameterDictKey))
        //            throw new ConfigurationException(_messages.Get(MessageKeys.NotFoundSectionDict,
        //                ConstantsKeys.StaticPatameterDictKey));
        //        var serializeSettings = _converterFacade.CreateSettings();
        //        var result = JsonConvert.DeserializeObject<Dictionary<string, StaticParameterJson>>(
        //            dictionaries[ConstantsKeys.StaticPatameterDictKey].ToString(), serializeSettings);
        //        if (!result!.ContainsKey(item.Id))
        //            throw new ArgumentException(_messages.Get(MessageKeys.NotFoundStaticParameter, item.Id));
        //        result[item.Id].Description = item.Description;
        //        result[item.Id].Value = item.Value;
        //        context.UpdateDictionary(ConstantsKeys.StaticPatameterDictKey, JObject.FromObject(result));
        //    }
        //}

        //public void Set(IEnumerable<StaticParameter> items)
        //{
        //    using (var context = _contextBuilder.BuildLow())
        //    {
        //        var dictionaries = context.GetDictionaratis();
        //        if (dictionaries.ContainsKey(ConstantsKeys.StaticPatameterDictKey))
        //            throw new ConfigurationException(_messages.Get(MessageKeys.NotFoundSectionDict,
        //                ConstantsKeys.StaticPatameterDictKey));
        //        var serializeSettings = _converterFacade.CreateSettings();
        //        var result = JsonConvert.DeserializeObject<Dictionary<string, StaticParameterJson>>(
        //            dictionaries[ConstantsKeys.StaticPatameterDictKey].ToString(), serializeSettings);
        //        foreach (var item in items)
        //        {
        //            if (!result!.ContainsKey(item.Id))
        //                throw new ArgumentException(_messages.Get(MessageKeys.NotFoundStaticParameter, item.Id));
        //            result[item.Id].Description = item.Description;
        //            result[item.Id].Value = item.Value;                    
        //        }
        //        context.UpdateDictionary(ConstantsKeys.StaticPatameterDictKey, JObject.FromObject(result!));
        //    }
        //}
    }
}
