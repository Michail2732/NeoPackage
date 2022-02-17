using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Package.Abstraction.Services;
using Package.Repository.Services;

namespace Package.Tests.Unit.Repository
{
    public class StubRepository : IRepository<StubRepositoryItem, string>
    {
        public IEnumerable<StubRepositoryItem> Get()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StubRepositoryItem> Get(Func<StubRepositoryItem, bool> filter)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<StubRepositoryItem>> GetAsync(Func<StubRepositoryItem, bool> filter, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public StubRepositoryItem GetItem(string id)
        {
            throw new NotImplementedException();
        }

        public Task<StubRepositoryItem> GetItemAsync(string id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
