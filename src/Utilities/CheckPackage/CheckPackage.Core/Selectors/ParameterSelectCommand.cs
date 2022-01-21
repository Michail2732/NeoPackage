using CheckPackage.Core.Entities;
using Package.Abstraction.Entities;
using System.Collections.Generic;

namespace CheckPackage.Core.Selectors
{
    public abstract class ParameterSelectCommand
    {
        public IList<Parameter> Select(IEnumerable<PackageEntity> entities, PackageContext context)
        {
            return InnerSelect(entities, context);
        }

        protected abstract IList<Parameter> InnerSelect(IEnumerable<PackageEntity> entities, PackageContext context);
    }    
    
}
