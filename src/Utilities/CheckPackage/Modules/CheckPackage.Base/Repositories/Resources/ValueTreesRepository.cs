using CheckPackage.Base.Configuration;
using CheckPackage.Base.Extensions;
using CheckPackage.Core.Resources;
using Package.Abstraction.Services;
using Package.Configuration.Exceptions;
using Package.Repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CheckPackage.Base.Repositories
{
    public class ValueTreesRepository : RepositoryWithCache<ValueTreeResource, string>
    {        

        public ValueTreesRepository(IConfigurationReader configuration) : base(configuration)
        {     
        }

        protected override List<ValueTreeResource> GetAllInternal()
        {
            var rules = _configuration.GetCurrentRule();
            var trees = (ValueTreeResourceJson?)rules.Resources?.Find(a => a.GetType() == typeof(ValueTreeResourceJson)) ??
                throw new ConfigurationException($"Could not find value trees in rules id = {rules.Info?.Id}");            
            return trees.Trees.Select(a => new ValueTreeResource(new[] { a.Value }, a.Key)).ToList();

        }
    }
}
