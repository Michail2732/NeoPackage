using Package.Abstraction.Entities;
using Package.Abstraction.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Package.Resourcing.Resources
{
    public class ResourceStoragesProvider: IResourceStoragesProvider
    {
        private readonly ResourceStorageInternal[] _repositories;        

        internal ResourceStoragesProvider(IEnumerable<ResourceStorageInternal> repositories)
        {
            if (repositories is null)
                throw new ArgumentNullException(nameof(repositories));
            _repositories = repositories.ToArray();            
        }

        public IResourceStorage<TData, TKey> GetStorage<TData, TKey>() where TData : class, IEntity<TKey>
        {
            if (!HasResource<TData, TKey>())
                throw new ArgumentException($"Repository of items {typeof(TData).Name} not registred");
            return (IResourceStorage<TData, TKey>)_repositories.First(a => a.Info.ItemType == typeof(TData) 
                && a.Info.KeyType == typeof(TKey)).Instance;
        }

        public bool HasResource<TData, TKey>() where TData : class, IEntity<TKey>
            => _repositories.Any(a => a.Info.ItemType == typeof(TData) && a.Info.KeyType == typeof(TKey));        
        
    }
}
