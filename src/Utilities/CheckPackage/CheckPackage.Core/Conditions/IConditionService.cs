using CheckPackage.Core.Entities;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Core.Conditions
{
    public interface IConditionService
    {
        bool Resolve(PackageEntity entity, IReadOnlyList<PackageEntityConditionCommand> conditions);
        bool Resolve(PackageEntity entity, PackageEntityConditionCommand condition);
        bool Resolve(Package_ package, IReadOnlyList<PackageConditionCommand> conditions);
        bool Resolve(Package_ package, PackageConditionCommand condition);
    }
}
