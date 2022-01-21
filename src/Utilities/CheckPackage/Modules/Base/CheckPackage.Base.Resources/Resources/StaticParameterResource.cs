using CheckPackage.Core.Extracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Package.Resourcing.Resources;
using CheckPackage.Configuration.Services;

namespace CheckPackage.Base.Resource
{
    public class StaticParameterResource : ResourceStorage<StaticParameter, string>
    {        
        private readonly IConfigurationAdapter<StaticParameter> _adapter;

        public StaticParameterResource(IConfigurationAdapter<StaticParameter> adapter)
        {            
            _adapter = adapter ?? throw new ArgumentNullException(nameof(adapter));
        }

        public override IEnumerable<StaticParameter> Get()
        {
            return GetPrivate();
        }

        public override IEnumerable<StaticParameter> Get(Func<StaticParameter, bool> filter)
        {
            return GetPrivate().Where(filter);
        }

        public async override Task<IEnumerable<StaticParameter>> GetAsync(Func<StaticParameter, bool> filter, CancellationToken ct)
        {
            return GetPrivate().Where(filter);
        }

        public override StaticParameter? GetItem(string id)
        {
            return GetPrivate().FirstOrDefault(a => a.Id == id);
        }

        public async override Task<StaticParameter?> GetItemAsync(string id, CancellationToken ct)
        {
            return GetPrivate().FirstOrDefault(a => a.Id == id);
        }        

        private IEnumerable<StaticParameter> GetPrivate() => _adapter.Get();
    }
}
