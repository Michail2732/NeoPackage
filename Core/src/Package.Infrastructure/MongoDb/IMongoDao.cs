using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Package.Infrastructure.MongoDb
{
    public interface IMongoDao<TItem, TKey> : IReadOnlyMongoDao<TItem, TKey>
        where TItem : class
    {
        TItem Update(TKey key, TItem item);
        Task<TItem> UpdateAsync(TKey key, TItem item, CancellationToken ct);
        TItem Insert(TItem item);
        Task<TItem> InsertAsync(TItem item, CancellationToken ct);
        uint Delete(TKey key);
        Task<uint> DeleteAsync(TKey key, CancellationToken ct);
        uint Delete(IMongoSpecification spec);
        Task<uint> DeleteAsync(IMongoSpecification spec, CancellationToken ct);
        
    }
   
}