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
    public class ColumnMapResource : ResourceStorage<ColumnMappingResource, string>
    {
        private readonly IConfigurationAdapter<ColumnMappingResource> _dataAdapter;

        public ColumnMapResource(IConfigurationAdapter<ColumnMappingResource> dataAdapter)
        {
            _dataAdapter = dataAdapter ?? throw new ArgumentNullException(nameof(dataAdapter));
        }

        public override IEnumerable<ColumnMappingResource> Get()
        {
            return GetPrivate();
        }

        public override IEnumerable<ColumnMappingResource> Get(Func<ColumnMappingResource, bool> filter)
        {
            return GetPrivate().Where(filter);
        }

        public async override Task<IEnumerable<ColumnMappingResource>> GetAsync(Func<ColumnMappingResource, bool> filter, CancellationToken ct)
        {
            return GetPrivate().Where(filter);
        }

        public override ColumnMappingResource? GetItem(string id)
        {
            return GetPrivate().FirstOrDefault(a => a.Id == id);
        }

        public async override Task<ColumnMappingResource?> GetItemAsync(string id, CancellationToken ct)
        {
            return GetPrivate().FirstOrDefault(a => a.Id == id);
        }

        private IList<ColumnMappingResource> GetPrivate()
        {
            return _dataAdapter.Get();
        }
    }
}
