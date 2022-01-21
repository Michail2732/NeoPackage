using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Package.Resourcing.Resources;
using CheckPackage.Core.Validation;
using CheckPackage.Configuration.Services;

namespace CheckPackage.Base.Resource
{
    public class EntityCheckRuleResource : ResourceStorage<EntityCheckRule, string>
    {
        private readonly IConfigurationAdapter<EntityCheckRule> _adapter;

        public EntityCheckRuleResource(IConfigurationAdapter<EntityCheckRule> adapter)
        {
            _adapter = adapter ?? throw new ArgumentNullException(nameof(adapter));
        }

        public override IEnumerable<EntityCheckRule> Get()
        {
            return GetPrivate();
        }

        public override IEnumerable<EntityCheckRule> Get(Func<EntityCheckRule, bool> filter)
        {
            return GetPrivate().Where(filter);
        }

        public async override Task<IEnumerable<EntityCheckRule>> GetAsync(Func<EntityCheckRule, bool> filter, CancellationToken ct)
        {
            return GetPrivate().Where(filter);
        }

        public override EntityCheckRule? GetItem(string id)
        {
            return GetPrivate().FirstOrDefault(a => a.Id == id);
        }

        public async override Task<EntityCheckRule?> GetItemAsync(string id, CancellationToken ct)
        {
            return GetPrivate().FirstOrDefault(a => a.Id == id);
        }

        private IEnumerable<EntityCheckRule> GetPrivate() => _adapter.Get();
    }
}
