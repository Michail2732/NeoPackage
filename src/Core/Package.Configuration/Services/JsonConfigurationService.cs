using Newtonsoft.Json;
using Package.Abstraction.Services;
using Package.Abstraction.Entities;
using Package.Configuration.Exceptions;
using Package.Abstraction.Extensions;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Package.Configuration.Services
{
    public class JsonConfigurationService : IConfigurationReader, IConfigurationWriter
    {
        private readonly IJsonConfigurationProvider _provider;
        private readonly Dictionary<Type, object> _binders;

        internal JsonConfigurationService(IJsonConfigurationProvider provider, Dictionary<Type, object> binders)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
            _binders = binders ?? throw new ArgumentNullException(nameof(binders));
            if (_binders.Values.Any(a => !a.GetType().IsAssignableToGenericType(typeof(IJsonConfigurationBinder<>))))
                throw new ArgumentException("Incorrect binder instance");
            if (_binders.Any(a =>
                {
                    var genericArg = a.Value.GetType().GetGenericArgument(typeof(IJsonConfigurationBinder<>), 0);
                    return genericArg != a.Key;
                }))
                throw new ArgumentException("Incorrect relation of type and instance");
        }

        public void Set<T>(T item) where T: class
        {
            if (!_binders.ContainsKey(typeof(T)))
                throw new ConfigurationException($"Not found configuration binder for type {typeof(T)}");            
            var configuration = _provider.Get();            
            try
            {
                ((IJsonConfigurationBinder<T>)_binders[typeof(T)]).Set(item, configuration);                
                _provider.Set(configuration);
            }
            catch (Exception ex) { throw new ConfigurationException($"Cant save configuration: {ex.Message}", ex); }            
        }

        public T Get<T>() where T: class
        {
            if (!_binders.ContainsKey(typeof(T)))
                throw new ConfigurationException($"Not found configuration binder for type {typeof(T)}");                        
            var configuration = _provider.Get();            
            try { return ((IJsonConfigurationBinder<T>)_binders[typeof(T)]).Get(configuration); }
            catch (Exception ex) { throw new ConfigurationException($"Cant extract configuration: {ex.Message}", ex); }
        }
    }
}
