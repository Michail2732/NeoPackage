using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Core.Checks
{
    public interface ICheckService
    {
        Result Check(Entity_ entity, EntityCheckCommand command);
        Result Check(Entity_ entity, IReadOnlyList<EntityCheckCommand> commands);
        Result Check(Package_ package, PackageCheckCommand command);
        Result Check(Package_ package, IReadOnlyList<PackageCheckCommand> commands);
        Result Check(KeyValuePair<string, string> parameter, ParameterCheckCommand command);
        Result Check(KeyValuePair<string, string> parameter, IReadOnlyList<ParameterCheckCommand> commands);
        Result Check(UserParameter_ parameter, ParameterCheckCommand command);
        Result Check(UserParameter_ parameter, IReadOnlyList<ParameterCheckCommand> commands);
    }
}
