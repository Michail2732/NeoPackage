using CheckPackage.Core.Entities;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Core.Conditions
{
    public interface IConditionService
    {
        bool Resolve(UserParameter_ parameter, ParameterConditionCommand command);
        bool Resolve(UserParameter_ parameter, IReadOnlyList<ParameterConditionCommand> commands);
        bool Resolve(KeyValuePair<string, string> parameter, ParameterConditionCommand command);
        bool Resolve(KeyValuePair<string, string> parameter, IReadOnlyList<ParameterConditionCommand> commands);
        bool Resolve(Entity_ entity, IReadOnlyList<EntityConditionCommand> commands);
        bool Resolve(Entity_ entity, EntityConditionCommand command);
        bool Resolve(Package_ package, IReadOnlyList<PackageConditionCommand> commands);
        bool Resolve(Package_ package, PackageConditionCommand command);
    }
}
