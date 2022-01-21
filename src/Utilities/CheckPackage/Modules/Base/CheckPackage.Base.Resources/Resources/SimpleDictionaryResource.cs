using CheckPackage.Core.Checks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Package.Resourcing.Resources;
using CheckPackage.Configuration.Services;

namespace CheckPackage.Base.Resource
{
    public class SimpleDictionaryResource : ResourceStorage<Core.Checks.SimpleDictionaryResource, string>
    {
        private readonly IConfigurationAdapter<Core.Checks.SimpleDictionaryResource> _adapter;

        public SimpleDictionaryResource(IConfigurationAdapter<Core.Checks.SimpleDictionaryResource> adapter)
        {
            _adapter = adapter ?? throw new ArgumentNullException(nameof(adapter));
        }

        public override IEnumerable<Core.Checks.SimpleDictionaryResource> Get()
        {
            return GetPrivate();
        }

        public override IEnumerable<Core.Checks.SimpleDictionaryResource> Get(Func<Core.Checks.SimpleDictionaryResource, bool> filter)
        {
            return GetPrivate().Where(filter);
        }

        public async override Task<IEnumerable<Core.Checks.SimpleDictionaryResource>> GetAsync(Func<Core.Checks.SimpleDictionaryResource, bool> filter, CancellationToken ct)
        {
            return GetPrivate().Where(filter);
        }

        public override Core.Checks.SimpleDictionaryResource? GetItem(string id)
        {
            return GetPrivate().FirstOrDefault(a => a.Id == id);
        }

        public async override Task<Core.Checks.SimpleDictionaryResource?> GetItemAsync(string id, CancellationToken ct)
        {
            return GetPrivate().FirstOrDefault(a => a.Id == id);
        }

        private IEnumerable<Core.Checks.SimpleDictionaryResource> GetPrivate() => _adapter.Get();
    }
}
