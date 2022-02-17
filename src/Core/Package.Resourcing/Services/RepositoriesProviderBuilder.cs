using Microsoft.Extensions.DependencyInjection;
using Package.Abstraction.Entities;
using Package.Abstraction.Extensions;
using Package.Abstraction.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Package.Repository.Services
{
    public class RepositoriesProviderBuilder
    {
        private readonly IServiceCollection _serviceCollection;
        private readonly List<Type> _repositoryTypes = new List<Type>();

        public RepositoriesProviderBuilder(IServiceCollection serviceCollection)
        {
            _serviceCollection = serviceCollection ?? throw new ArgumentNullException(nameof(serviceCollection));
            _serviceCollection.AddSingleton(Build);
        }

        public RepositoriesProviderBuilder AddRepository<TRepository, TData, TKey>(ServiceLifetime lifetime = ServiceLifetime.Singleton)            
            where TRepository : IRepository<TData, TKey> 
            where TData: class, IRepositoryItem<TKey>
        {            
            _serviceCollection.AddSingleton(typeof(TRepository));
            if (!_repositoryTypes.Contains(typeof(TRepository)))
                _repositoryTypes.Add(typeof(TRepository));
            return this;
        }

        internal IRepositoriesProvider Build(IServiceProvider serviceProvider)
        {
            List<object> repositories = new List<object>();
            foreach (var repositoryType in _repositoryTypes)            
                repositories.Add(serviceProvider.GetRequiredService(repositoryType));
            var repositoriesDict = repositories.ToDictionary(a => a.GetType().
                GetGenericArgument(typeof(IRepository<,>), 0));            
            return new RepositoriesProvider(repositoriesDict);
        }
    }
}
