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
    public class MatrixDictionaryResource : ResourceStorage<MatrixDictionary, string>
    {
        private readonly IConfigurationAdapter<MatrixDictionary> _adapter;

        public MatrixDictionaryResource(IConfigurationAdapter<MatrixDictionary> adapter)
        {
            _adapter = adapter ?? throw new ArgumentNullException(nameof(adapter));
        }

        public override IEnumerable<MatrixDictionary> Get()
        {
            return GetPrivate();
        }

        public override IEnumerable<MatrixDictionary> Get(Func<MatrixDictionary, bool> filter)
        {
            return GetPrivate().Where(filter);
        }

        public override async Task<IEnumerable<MatrixDictionary>> GetAsync(Func<MatrixDictionary, bool> filter, CancellationToken ct)
        {
            return GetPrivate().Where(filter);
        }

        public override MatrixDictionary? GetItem(string id)
        {
            return GetPrivate().FirstOrDefault(a => a.Id == id);
        }

        public override async Task<MatrixDictionary> GetItemAsync(string id, CancellationToken ct)
        {
            return GetPrivate().FirstOrDefault(a => a.Id == id);
        }

        private IEnumerable<MatrixDictionary> GetPrivate() => _adapter.Get();
    }
}
