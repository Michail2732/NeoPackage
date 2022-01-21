using Microsoft.Extensions.DependencyInjection;
using Package.Resourcing.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace Package.Resourcing.Dependency
{
    public static class SeviceColletionExtensions
    {
        public static ResourceStoragesBuilder AddResourceStorages(this IServiceCollection collection)
        {            
            var provider = collection.BuildServiceProvider();
            ResourceStoragesBuilder? resourceBuilder = provider.GetService<ResourceStoragesBuilder>();
            if (resourceBuilder == null)
            {
                resourceBuilder = new ResourceStoragesBuilder(collection);
                collection.AddSingleton(resourceBuilder);
            }
            return resourceBuilder;
        }

    }
}
