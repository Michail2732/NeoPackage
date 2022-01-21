using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Package.Abstraction.Services
{
    public interface IResourceStoragesProvider
    {        
        bool HasResource<TItem, TKey>() where TItem : class, IEntity<TKey>;
        IResourceStorage<TItem, TKey> GetStorage<TItem, TKey>() where TItem : class, IEntity<TKey>;        
    }
}
