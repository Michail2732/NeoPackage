using Microsoft.Extensions.DependencyInjection;
using Package.Configuration.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Package.Configuration.Dependency
{
    public static class ServiceCollectionsExtensions
    {
        public static JsonConfigurationServiceBuilder AddJsonConfiguration(this IServiceCollection collection)
        {
            var provider = collection.BuildServiceProvider();
            JsonConfigurationServiceBuilder? builder = provider.GetService<JsonConfigurationServiceBuilder>();
            if (builder == null)
            {
                builder = new JsonConfigurationServiceBuilder(collection);
                collection.AddSingleton(builder);
            }
            return builder;
        }

    }
}
