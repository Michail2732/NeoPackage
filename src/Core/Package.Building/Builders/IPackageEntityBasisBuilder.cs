using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Package.Building.Builders
{
    public interface IPackageEntityBasisBuilder
    {
        Task<IEnumerable<EntityBuildingResult>> BuildAsync(PackageContext context, CancellationToken ct);
        IEnumerable<EntityBuildingResult> Build(PackageContext context);

    }
}
