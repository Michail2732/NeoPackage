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
    public class RegexItemsRepository : RepositoryWithCache<RegexItemResource, string>
    {        

        public RegexItemsRepository(IConfigurationReader configuration): base(configuration)
        {     
        }

        protected override List<RegexItemResource> GetAllInternal()
        {
            var rules = _configuration.GetCurrentRule();
            var regexes = (RegexResourceJson?)rules.Resources?.Find(a => a.GetType() == typeof(RegexResourceJson)) ??
                throw new ConfigurationException($"Could not find regex items in rules id = {rules.Info?.Id}");
            List<RegexItemResource> result = new List<RegexItemResource>();
            if (regexes.Patterns != null)
                foreach (var pattern in regexes.Patterns)            
                    result.Add(new RegexItemResource(pattern.Key, pattern.Value, false));
            if (regexes.CompositePatterns != null)
                foreach (var compositePattern in regexes.CompositePatterns)            
                    result.Add(new RegexItemResource(compositePattern.Key, compositePattern.Value, true));            
            return result;
        }
    }
}
