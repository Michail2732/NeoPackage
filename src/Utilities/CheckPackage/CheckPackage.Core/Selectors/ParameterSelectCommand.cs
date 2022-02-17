using CheckPackage.Core.Entities;
using Package.Abstraction.Entities;
using System.Collections.Generic;

namespace CheckPackage.Core.Selectors
{
    public abstract class ParameterSelectCommand
    {
        public IEnumerable<Parameter> Select(IEnumerable<Entity_> entities, PackageContext context)
        {
            return InnerSelect(entities, context);
        }

        protected abstract IEnumerable<Parameter> InnerSelect(IEnumerable<Entity_> entities, PackageContext context);
    }    
    
}
