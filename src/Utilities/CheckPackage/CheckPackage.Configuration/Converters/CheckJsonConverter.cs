﻿using CheckPackage.Configuration.Entities;
using CheckPackage.Configuration.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace CheckPackage.Configuration.Converters
{
    public class CheckJsonConverter : JsonConverter
    {
        private readonly IJsonConverterFacade _strategyConverter;

        public CheckJsonConverter(IJsonConverterFacade provider)
        {
            _strategyConverter = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(BaseCheckJson);
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            try
            {
                var jObject = JObject.Load(reader);                
                return _strategyConverter.CheckConvert(jObject);                
            }
            catch (Exception ex)
            {
                throw new ConfigurationException($"Cant deserialize check:{ex.Message}", ex);
            }            
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            try 
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                throw new ConfigurationException($"Cant serialize check:{ex.Message}", ex);
            }
        }
    }
}
