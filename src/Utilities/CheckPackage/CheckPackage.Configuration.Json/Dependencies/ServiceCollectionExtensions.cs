using CheckPackage.Configuration.Json.Binder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Configuration.Dependencies
{
    public static class ServiceCollectionExtensions
    {
        public static IJsonToCommandBinderBuilder AddCommandBinder(this IServiceCollection collection)
        {
            var provider = collection.BuildServiceProvider();
            IJsonToCommandBinderBuilder? builder = provider.GetService<IJsonToCommandBinderBuilder>();
            if (builder == null)
            {
                builder = new JsonToCommandBinderBuilder(collection);
                collection.AddSingleton(builder);
            }
            return builder;
        }

    }
}
