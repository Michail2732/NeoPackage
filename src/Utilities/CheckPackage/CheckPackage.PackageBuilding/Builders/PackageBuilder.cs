using Package.Abstraction.Entities;
using Package.Building.Builders;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CheckPackage.PackageBuilding.Builders
{
    public class PackageBuilder : IPackageBuilder
    {

        public PackageBuildingResult Build(IEnumerable<Entity_> entites, PackageContext context)
        {
            return new PackageBuildingResult(Guid.NewGuid().ToString(), "package");
        }

        public async Task<PackageBuildingResult> BuildAsync(IEnumerable<Entity_> entites, PackageContext context, CancellationToken ct)
        {
            return new PackageBuildingResult(Guid.NewGuid().ToString(), "package");
        }
    }
}
