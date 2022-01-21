using Microsoft.Extensions.DependencyInjection;
using Package.Abstraction.Entities;
using Package.Abstraction.Services;
using Package.Resourcing.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Package.Resourcing.Resources
{
    public class ResourceStoragesBuilder
    {
        public IServiceCollection ServiceCollection { get; }

        public ResourceStoragesBuilder(IServiceCollection serviceCollection)
        {
            ServiceCollection = serviceCollection ?? throw new ArgumentNullException(nameof(serviceCollection));
            ServiceCollection.AddSingleton<IResourceStoragesProvider>(Build);
        }

        public ResourceStoragesBuilder AddResource<TRepository, TData, TKey>(ServiceLifetime lifetime)            
            where TRepository : ResourceStorage<TData, TKey> 
            where TData: class, IEntity<TKey>
        {
            var descr = new ServiceDescriptor(typeof(IResourceStorageInstance), typeof(TRepository), lifetime);
            ServiceCollection.Add(descr);
            return this;
        }

        public ResourceStoragesProvider Build(IServiceProvider serviceProvider)
        {
            var repositories = serviceProvider.GetServices<IResourceStorageInstance>();
            var internalRepositories = repositories.Select(a => new ResourceStorageInternal
                (
                    new ResourceStorageItemInfo
                    (
                        a.GetType().GetGenericArgument(typeof(ResourceStorage<,>), 0)!,
                        a.GetType().GetGenericArgument(typeof(ResourceStorage<,>), 1)!
                    ),
                    a
                )
            );            
            return new ResourceStoragesProvider(internalRepositories);
        }
    }
}
