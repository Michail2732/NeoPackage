using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Package.Building.Builders
{
    public interface IEntityBuilder
    {
        Task<EntityBuildingResult> BuildAsync(IEnumerable<Entity_> children, uint level, PackageContext context, CancellationToken ct);
        EntityBuildingResult Build(IEnumerable<Entity_> children, uint level, PackageContext context);
    }
}
