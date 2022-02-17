using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Package.Abstraction.Services
{
    public interface IRepositoriesProvider
    {        
        bool HasRepository<TItem, TKey>() where TItem : class, IRepositoryItem<TKey>;
        IRepository<TItem, TKey> GetRepository<TItem, TKey>() where TItem : class, IRepositoryItem<TKey>;        
    }
}
