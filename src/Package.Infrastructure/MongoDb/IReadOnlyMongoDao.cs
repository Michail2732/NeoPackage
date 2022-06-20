using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Package.Infrastructure.MongoDb
{
    public interface IReadOnlyMongoDao<TItem, TKey> where TItem: class
    {
        TItem Get(TKey key);
        Task<TItem> GetAsync(TKey key, CancellationToken ct);
        IList<TItem> GetAll();
        Task<IList<TItem>> GetAllAsync( CancellationToken ct);
        IList<TItem> Get(IMongoSpecification spec);
        Task<IList<TItem>> GetAsync(IMongoSpecification spec, CancellationToken ct);
    }
}