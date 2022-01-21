using CheckPackage.Core.Entities;
using Package.Abstraction.Entities;
using Package.Abstraction.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Core.Selectors
{
    public class SelectorService : ISelectorService
    {
        private readonly IPackageContextBuilder _contextBuilder;

        public SelectorService(IPackageContextBuilder contextBuilder)
        {
            _contextBuilder = contextBuilder ?? throw new ArgumentNullException(nameof(contextBuilder));
        }

        public IList<Parameter> Select(IEnumerable<PackageEntity> entities, ParameterSelectCommand selector)
        {
            var context = _contextBuilder.Build();
            return selector.Select(entities, context);
        }

        public IList<Parameter> Select(IEnumerable<PackageEntity> entities, IEnumerable<ParameterSelectCommand> selectors)
        {
            var context = _contextBuilder.Build();
            List<Parameter> parameters = new List<Parameter>();
            foreach (var selector in selectors)
            {
                var paramsLocal = selector.Select(entities, context);
                if (paramsLocal.Count > 0)
                parameters.AddRange(paramsLocal);                
            }
            return parameters;
        }

        public IList<Parameter> Select(PackageEntity entity, ParameterSelectCommand selector)
        {
            var context = _contextBuilder.Build();
            return selector.Select(new PackageEntity[1] { entity }, context);
        }

        public IList<Parameter> Select(PackageEntity entity, IEnumerable<ParameterSelectCommand> selectors)
        {            
            var entities = new PackageEntity[1] { entity };
            return Select(entities, selectors);
        }
    }
}
