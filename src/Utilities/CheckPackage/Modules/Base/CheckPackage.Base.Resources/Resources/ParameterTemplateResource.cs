using CheckPackage.Core.Regex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Package.Resourcing.Resources;
using CheckPackage.Configuration.Services;

namespace CheckPackage.Base.Resource
{
    public class ParameterTemplateResource: ResourceStorage<Core.Regex.ParameterTemplateResource, string>
    {        
        private readonly IConfigurationAdapter<Core.Regex.ParameterTemplateResource> _adapter;

        public ParameterTemplateResource(IConfigurationAdapter<Core.Regex.ParameterTemplateResource> adapter)
        {
            _adapter = adapter ?? throw new ArgumentNullException(nameof(adapter));
        }

        public override IEnumerable<Core.Regex.ParameterTemplateResource> Get()
        {
            return GetPrivate();
        }

        public override IEnumerable<Core.Regex.ParameterTemplateResource> Get(Func<Core.Regex.ParameterTemplateResource, bool> filter)
        {
            return GetPrivate().Where(filter);
        }

        public override async Task<IEnumerable<Core.Regex.ParameterTemplateResource>> GetAsync(Func<Core.Regex.ParameterTemplateResource, bool> filter, CancellationToken ct)
        {
            return GetPrivate().Where(filter);
        }

        public override Core.Regex.ParameterTemplateResource? GetItem(string id)
        {
            return GetPrivate().FirstOrDefault(a => a.Id == id);
        }

        public override async Task<Core.Regex.ParameterTemplateResource?> GetItemAsync(string id, CancellationToken ct)
        {
            return GetPrivate().FirstOrDefault(a => a.Id == id);
        }

        private IEnumerable<Core.Regex.ParameterTemplateResource> GetPrivate() => _adapter.Get();        
    }
}
