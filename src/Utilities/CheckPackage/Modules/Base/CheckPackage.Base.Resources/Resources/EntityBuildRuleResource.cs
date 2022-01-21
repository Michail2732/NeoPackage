using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Package.Resourcing.Resources;
using CheckPackage.Core.Building;
using CheckPackage.Configuration.Services;

namespace CheckPackage.Base.Resource
{
    public class EntityBuildRuleResource : ResourceStorage<Core.Building.EntityBuildRuleResource, string>
    {
        private readonly IConfigurationAdapter<Core.Building.EntityBuildRuleResource> _adapter;

        public EntityBuildRuleResource( IConfigurationAdapter<Core.Building.EntityBuildRuleResource> adapter)
        {
            _adapter = adapter ?? throw new ArgumentNullException(nameof(adapter));
        }

        public override IEnumerable<Core.Building.EntityBuildRuleResource> Get()
        {
            return GetPrivate();
        }

        public override IEnumerable<Core.Building.EntityBuildRuleResource> Get(Func<Core.Building.EntityBuildRuleResource, bool> filter)
        {
            return GetPrivate().Where(filter);
        }

        public async override Task<IEnumerable<Core.Building.EntityBuildRuleResource>> GetAsync(Func<Core.Building.EntityBuildRuleResource, bool> filter, CancellationToken ct)
        {
            return GetPrivate().Where(filter);
        }

        public override Core.Building.EntityBuildRuleResource? GetItem(string id)
        {
            return GetPrivate().FirstOrDefault(a => a.Id == id);
        }

        public async override Task<Core.Building.EntityBuildRuleResource?> GetItemAsync(string id, CancellationToken ct)
        {
            return GetPrivate().FirstOrDefault(a => a.Id == id);
        }

        private IEnumerable<Core.Building.EntityBuildRuleResource> GetPrivate() => _adapter.Get();
    }
}
