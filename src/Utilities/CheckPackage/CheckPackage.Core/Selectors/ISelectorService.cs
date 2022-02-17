using CheckPackage.Core.Entities;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Core.Selectors
{
    public interface ISelectorService
    {
        IEnumerable<Parameter> Select(IEnumerable<Entity_> entities, ParameterSelectCommand selector);
        IEnumerable<Parameter> Select(IEnumerable<Entity_> entities, IEnumerable<ParameterSelectCommand> selectors);
        IEnumerable<Parameter> Select(Entity_ entity, ParameterSelectCommand selector);
        IEnumerable<Parameter> Select(Entity_ entity, IEnumerable<ParameterSelectCommand> selector);

    }
}
