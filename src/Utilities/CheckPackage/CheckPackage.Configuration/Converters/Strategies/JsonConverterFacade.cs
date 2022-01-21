using CheckPackage.Configuration.Entities;
using CheckPackage.Configuration.Exceptions;
using CheckPackage.Core.Checks;
using CheckPackage.Core.Condition;
using CheckPackage.Core.Extracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace CheckPackage.Configuration.Converters
{
    public class JsonConverterFacade : IJsonConverterFacade
    {
        private readonly List<IExtractConvertStrategy> _extractStrategies;
        private readonly List<ICheckConvertStrategy> _checkStrategies;
        private readonly List<IConditionConvertStrategy> _conditionStrategies;

        internal JsonConverterFacade(List<IExtractConvertStrategy> extractStrategies, 
            List<ICheckConvertStrategy> checkStrategies, List<IConditionConvertStrategy> conditionStrategies)
        {
            _extractStrategies = extractStrategies ?? throw new ArgumentNullException(nameof(extractStrategies));
            _checkStrategies = checkStrategies ?? throw new ArgumentNullException(nameof(checkStrategies));
            _conditionStrategies = conditionStrategies ?? throw new ArgumentNullException(nameof(conditionStrategies));
        }

        public CheckInfo CheckConvert(JObject jsonCheck)
        {
            foreach (var checkStrategy in _checkStrategies)            
                if (checkStrategy.IsMatch(jsonCheck))
                {
                    var jsonCheckBase = checkStrategy.ToJsonBase(jsonCheck);
                    return checkStrategy.ToModelBase(jsonCheckBase);
                }                               
            throw new ConfigurationException($"Could not found strategy for deserialize of type {jsonCheck.GetType()}");
        }

        public CheckInfo CheckConvert(BaseCheckJson jsonCheck)
        {
            foreach (var checkStrategy in _checkStrategies)            
                if (checkStrategy.IsMatch(jsonCheck))                                    
                    return checkStrategy.ToModelBase(jsonCheck);                            
            throw new ConfigurationException($"Could not found strategy for deserialize of type {jsonCheck.GetType()}");
        }

        public ConditionInfo ConditionConvert(JObject jsonCondition)
        {
            foreach (var conditionStrategy in _conditionStrategies)            
                if (conditionStrategy.IsMatch(jsonCondition))
                {
                    var conditionJsonBase = conditionStrategy.ToJsonBase(jsonCondition);
                    return conditionStrategy.ToModelBase(conditionJsonBase);
                }                                
            throw new ConfigurationException($"Could not found strategy for deserialize of type {jsonCondition.GetType()}");
        }

        public ConditionInfo ConditionConvert(BaseConditionJson jsonCondition)
        {
            foreach (var conditionStrategy in _conditionStrategies)
                if (conditionStrategy.IsMatch(jsonCondition))                                    
                    return conditionStrategy.ToModelBase(jsonCondition);                
            throw new ConfigurationException($"Could not found strategy for deserialize of type {jsonCondition.GetType()}");
        }

        public ExtractInfo ExtractConvert(JObject jsonExtract)
        {
            foreach (var extractStrategy in _extractStrategies)            
                if (extractStrategy.IsMatch(jsonExtract))
                {
                    var extractJsonBase = extractStrategy.ToJsonBase(jsonExtract);
                    return extractStrategy.ToModelBase(extractJsonBase);
                }                                
            throw new ConfigurationException($"Could not found strategy for deserialize of type {jsonExtract.GetType()}");
        }

        public ExtractInfo ExtractConvert(BaseExtractJson jsonExtract)
        {
            foreach (var extractStrategy in _extractStrategies)
                if (extractStrategy.IsMatch(jsonExtract))                                    
                    return extractStrategy.ToModelBase(jsonExtract);                
            throw new ConfigurationException($"Could not found strategy for deserialize of type {jsonExtract.GetType()}");
        }

        public JsonConverter[] GetConverters()
        {
            return new JsonConverter[]
            {
                new CheckJsonConverter(this),
                new ConditionJsonConverter(this),
                new ExtractJsonConverter(this)
            };
        }
    }
}
