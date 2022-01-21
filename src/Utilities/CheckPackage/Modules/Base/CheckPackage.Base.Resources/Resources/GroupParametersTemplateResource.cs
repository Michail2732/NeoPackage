using CheckPackage.Core.Regex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Package.Resourcing.Resources;
using CheckPackage.Configuration.Services;

namespace CheckPackage.Base.Resource
{
    public class GroupParametersTemplateResource : ResourceStorage<Core.Regex.GroupParametersTemplateResource, string>
    {
        private readonly IConfigurationAdapter<Core.Regex.GroupParametersTemplateResource> _adapter;

        public GroupParametersTemplateResource(IConfigurationAdapter<Core.Regex.GroupParametersTemplateResource> adapter)
        {
            _adapter = adapter ?? throw new ArgumentNullException(nameof(adapter));
        }

        public override IEnumerable<Core.Regex.GroupParametersTemplateResource> Get()
        {
            return GetPrivate();
        }

        public override IEnumerable<Core.Regex.GroupParametersTemplateResource> Get(Func<Core.Regex.GroupParametersTemplateResource, bool> filter)
        {
            return GetPrivate().Where(filter);
        }

        public async override Task<IEnumerable<Core.Regex.GroupParametersTemplateResource>> GetAsync(Func<Core.Regex.GroupParametersTemplateResource, bool> filter, CancellationToken ct)
        {
            return GetPrivate().Where(filter);
        }

        public override Core.Regex.GroupParametersTemplateResource? GetItem(string id)
        {
            return GetPrivate().FirstOrDefault(a => a.Id == id);
        }

        public async override Task<Core.Regex.GroupParametersTemplateResource?> GetItemAsync(string id, CancellationToken ct)
        {
            return GetPrivate().FirstOrDefault(a => a.Id == id);
        }

        private IEnumerable<Core.Regex.GroupParametersTemplateResource> GetPrivate() => _adapter.Get();
    }
}
