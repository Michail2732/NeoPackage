using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Core.Checks
{
    public interface ICheckService
    {
        Result Check(PackageEntity entity, PackageEntityCheckCommand check);
        Result Check(PackageEntity entity, IReadOnlyList<PackageEntityCheckCommand> checks);
        Result Check(Package_ package, PackageCheckCommand check);
        Result Check(Package_ package, IReadOnlyList<PackageCheckCommand> checks);
        Result Check(KeyValuePair<string, string> parameter, ParameterCheckCommand check);
        Result Check(KeyValuePair<string, string> parameter, IReadOnlyList<ParameterCheckCommand> checks);
        Result Check(UserParameter parameter, ParameterCheckCommand check);
        Result Check(UserParameter parameter, IReadOnlyList<ParameterCheckCommand> checks);
    }
}
