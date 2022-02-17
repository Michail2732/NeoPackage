using Package.Abstraction.Entities;
using Package.Abstraction.Extensions;
using Package.Abstraction.Services;
using Package.Repository.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Package.Repository.Services
{
    public class RepositoriesProvider: IRepositoriesProvider
    {
        private readonly Dictionary<Type, object> _repositories;            

        internal RepositoriesProvider(Dictionary<Type, object> repositories)
        {
            _repositories = repositories ?? throw new ArgumentNullException(nameof(repositories));              
            if (_repositories.Any(a => !a.Value.GetType().IsAssignableToGenericType(typeof(IRepository<,>))))
                throw new ArgumentException("Incorrect repository instance");
            if (_repositories.Any(a =>
            {
                var itemType = a.Value.GetType().GetGenericArgument(typeof(IRepository<,>), 0);                
                return itemType != a.Key || !a.Key.IsAssignableToGenericType(typeof(IRepositoryItem<>));
            }))
                throw new ArgumentException("Incorrect relation of type and instance");
        }

        public IRepository<TData, TKey> GetRepository<TData, TKey>() where TData : class, IRepositoryItem<TKey>
        {
            if (!HasRepository<TData, TKey>())
                throw new RepositoryNotFoundException($"Repository of items {typeof(TData).Name} not registred");
            return (IRepository<TData, TKey>)_repositories[typeof(TData)];
        }

        public bool HasRepository<TData, TKey>() where TData : class, IRepositoryItem<TKey>
            => _repositories.ContainsKey(typeof(TData));        
        
    }
}
