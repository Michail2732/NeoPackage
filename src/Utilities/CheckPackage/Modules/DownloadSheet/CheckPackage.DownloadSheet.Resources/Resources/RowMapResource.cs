using CheckPackage.Configuration.Services;
using CheckPackage.DownloadSheet.Mapping;
using Package.Resourcing.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CheckPackage.DownloadSheet.Resources
{
    public class RowMapResource : ResourceStorage<RowMappingResource, string>
    {
        private readonly IConfigurationAdapter<RowMappingResource> _dataAdapter;

        public RowMapResource(IConfigurationAdapter<RowMappingResource> dataAdapter)
        {
            _dataAdapter = dataAdapter ?? throw new ArgumentNullException(nameof(dataAdapter));
        }

        public override IEnumerable<RowMappingResource> Get()
        {
            return GetPrivate();
        }

        public override IEnumerable<RowMappingResource> Get(Func<RowMappingResource, bool> filter)
        {
            return GetPrivate().Where(filter);
        }

        public async override Task<IEnumerable<RowMappingResource>> GetAsync(Func<RowMappingResource, bool> filter, CancellationToken ct)
        {
            return GetPrivate().Where(filter);
        }

        public override RowMappingResource? GetItem(string id)
        {
            return GetPrivate().FirstOrDefault(a => a.Id == id);
        }

        public async override Task<RowMappingResource?> GetItemAsync(string id, CancellationToken ct)
        {
            return GetPrivate().FirstOrDefault(a => a.Id == id);
        }

        private IList<RowMappingResource> GetPrivate()
        {
            return _dataAdapter.Get();
        }
    }
}
