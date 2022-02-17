using System;
using Package.Abstraction.Services;
using Package.Configuration.Exceptions;
using Package.Repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Package.Abstraction.Entities;
using CheckPackage.Configuration.JsonEntities;

namespace CheckPackage.Base.Repositories
{
    public abstract class RepositoryWithCache<TItem, TKey> : IRepository<TItem, TKey>
        where TItem : class, IRepositoryItem<TKey>
    {
        protected readonly IConfigurationReader _configuration;
        private List<TItem>? _cachedItems;
        private readonly object _lock = new object();

        public RepositoryWithCache(IConfigurationReader configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }        

        protected abstract List<TItem> GetAllInternal();

        public IEnumerable<TItem> Get()
        {            
            var repositoryConfig = _configuration.Get<RepositoryConfigurationJson>();
            if (repositoryConfig.UseCache)
            {
                lock (_lock)
                {
                    if (_cachedItems == null)
                        _cachedItems = GetAllInternal();
                }                
                return _cachedItems;
            }
            return GetAllInternal();
        }

        public IEnumerable<TItem> Get(Func<TItem, bool> filter)
        {
            return Get().Where(filter);
        }        

        public Task<IEnumerable<TItem>> GetAsync(Func<TItem, bool> filter, CancellationToken ct)
        {
            var result = Get(filter);
            return Task.FromResult(result);
        }

        public TItem? GetItem(TKey id)
        {
            return Get().FirstOrDefault(a => a.Id.Equals(id));
        }

        public Task<TItem?> GetItemAsync(TKey id, CancellationToken ct)
        {
            var result = Get().FirstOrDefault(a => a.Id.Equals(id));
            return Task.FromResult(result);
        }
    }
}
