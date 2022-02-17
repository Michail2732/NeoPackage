using Microsoft.Extensions.DependencyInjection;
using Package.Abstraction.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Package.Configuration.Services
{
    public class JsonConfigurationServiceBuilder
    {
        private readonly IServiceCollection _services;
        private readonly Dictionary<Type, Type> _itemAndBinderTypes = new Dictionary<Type, Type>();

        public JsonConfigurationServiceBuilder(IServiceCollection services)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
            services.AddSingleton(BuildReader);
            services.AddSingleton(BuildWriter);
        }

        public JsonConfigurationServiceBuilder AddBinder<TBinder, TItem>() 
            where TBinder : class, IJsonConfigurationBinder<TItem>            
        {
            _itemAndBinderTypes[typeof(TItem)] = typeof(TBinder);
            _services.AddSingleton<TBinder>();
            return this;
        }

        internal IConfigurationReader BuildReader(IServiceProvider provider)
        {
            Dictionary<Type, object> binders = new Dictionary<Type, object>();
            foreach (var item in _itemAndBinderTypes)            
                binders[item.Key] = provider.GetRequiredService(item.Value);
            return new JsonConfigurationService(JsonConfigurationProvider.Instance, binders);
        }

        internal IConfigurationWriter BuildWriter(IServiceProvider provider)
        {
            return (JsonConfigurationService)provider.GetRequiredService<IConfigurationReader>();            
        }
    }
}
