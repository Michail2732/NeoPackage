using CheckPackage.Base.Configuration;
using CheckPackage.Base.Extensions;
using CheckPackage.Configuration.Json.Entities;
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
    public class StaticParametersRepository : RepositoryWithCache<StaticParameterResource, string>
    {        

        public StaticParametersRepository(IConfigurationReader configuration): base(configuration)
        {            
        }

        protected override List<StaticParameterResource> GetAllInternal()
        {
            var rules = _configuration.GetCurrentRule();
            var parameters = (StaticParameterResourceJson?)rules.Resources?.Find(a => a.GetType() == typeof(StaticParameterResourceJson)) ??
                throw new ConfigurationException($"Could not find static parameters in rules id = {rules.Info?.Id}");
            return parameters.Parameters.Select(a => new StaticParameterResource(Guid.NewGuid().ToString(), a.Key, a.Value.Value)).ToList();
        }
      
    }
}
