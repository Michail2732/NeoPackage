using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Package.Building.Builders
{
    public interface IPackageBuilder
    {
        Task<PackageBuildingResult> BuildAsync(IEnumerable<Entity_> entites, PackageContext context, CancellationToken ct);
        PackageBuildingResult Build(IEnumerable<Entity_> entities, PackageContext context);
    }
}
