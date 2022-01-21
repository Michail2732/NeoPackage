using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Configuration.Dependencies
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddConfigurationConvertStrategies(this IServiceCollection collection,
            Action<StrategyConverterBuilder> configure)
        {
            var builder = collection.BuildServiceProvider().GetService<StrategyConverterBuilder>();            
            if (builder == null)
            {
                builder = new StrategyConverterBuilder(collection);
                collection.AddSingleton(builder);
            }                                                    
            configure.Invoke(builder);
            return collection;
        }
    }
}
