using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Package.Resourcing.Resources;
using CheckPackage.Configuration.Services;
using CheckPackage.Core.Validation;

namespace CheckPackage.Base.Resource
{
    public class ParameterCheckRuleResource : ResourceStorage<ParameterCheckRule, string>
    {       
        private readonly IConfigurationAdapter<ParameterCheckRule> _adapter;

        public ParameterCheckRuleResource(IConfigurationAdapter<ParameterCheckRule> adapter)
        {
            _adapter = adapter ?? throw new ArgumentNullException(nameof(adapter));
        }

        public override IEnumerable<ParameterCheckRule> Get()
        {
            return GetPrivate();
        }

        public override IEnumerable<ParameterCheckRule> Get(Func<ParameterCheckRule, bool> filter)
        {
            return GetPrivate().Where(filter);
        }
        

        public override async Task<IEnumerable<ParameterCheckRule>> GetAsync(Func<ParameterCheckRule, bool> filter, CancellationToken ct)
        {
            return GetPrivate().Where(filter);
        }

        public override ParameterCheckRule? GetItem(string id)
        {
            return GetPrivate().FirstOrDefault(a => a.Id == id);

        }

        public override async Task<ParameterCheckRule?> GetItemAsync(string id, CancellationToken ct)
        {
            return GetPrivate().FirstOrDefault(a => a.Id == id);
        }

        private IEnumerable<ParameterCheckRule> GetPrivate()
        {
            return _adapter.Get();
        }

    }
}
