using CheckPackage.Core.Entities;
using Package.Abstraction.Entities;
using Package.Abstraction.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<Parameter> Select(IEnumerable<Entity_> entities, ParameterSelectCommand selector)
        {
            var context = _contextBuilder.Build();
            return selector.Select(entities, context);
        }

        public IEnumerable<Parameter> Select(IEnumerable<Entity_> entities, IEnumerable<ParameterSelectCommand> selectors)
        {
            var context = _contextBuilder.Build();
            List<Parameter> parameters = new List<Parameter>();
            foreach (var selector in selectors)
            {
                var paramsLocal = selector.Select(entities, context);
                if (paramsLocal.Count() > 0)
                parameters.AddRange(paramsLocal);                
            }
            return parameters;
        }

        public IEnumerable<Parameter> Select(Entity_ entity, ParameterSelectCommand selector)
        {
            var context = _contextBuilder.Build();
            return selector.Select(new Entity_[1] { entity }, context);
        }

        public IEnumerable<Parameter> Select(Entity_ entity, IEnumerable<ParameterSelectCommand> selectors)
        {            
            var entities = new Entity_[1] { entity };
            return Select(entities, selectors);
        }
    }
}
