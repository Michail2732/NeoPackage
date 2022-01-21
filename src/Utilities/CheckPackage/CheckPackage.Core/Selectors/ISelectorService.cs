using CheckPackage.Core.Entities;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Core.Selectors
{
    public interface ISelectorService
    {
        IList<Parameter> Select(IEnumerable<PackageEntity> entities, ParameterSelectCommand selector);
        IList<Parameter> Select(IEnumerable<PackageEntity> entities, IEnumerable<ParameterSelectCommand> selectors);
        IList<Parameter> Select(PackageEntity entity, ParameterSelectCommand selector);
        IList<Parameter> Select(PackageEntity entity, IEnumerable<ParameterSelectCommand> selector);

    }
}
