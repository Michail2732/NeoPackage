using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Package.Resourcing.Resources
{
    public abstract class ResourceStorage<TData, TKey> : IResourceStorageInstance
        where TData : class, IEntity<TKey>
    {                
        public abstract IEnumerable<TData> Get();
        public abstract IEnumerable<TData> Get(Func<TData, bool> filter);
        public abstract Task<IEnumerable<TData>> GetAsync(Func<TData, bool> filter, CancellationToken ct);
        public abstract TData? GetItem(TKey id);
        public abstract Task<TData?> GetItemAsync(TKey id, CancellationToken ct);
    }
}
