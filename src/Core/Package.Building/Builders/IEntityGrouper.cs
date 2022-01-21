using Package.Abstraction.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Package.Building.Builders
{
    public interface IEntityGrouper
    {
        IEnumerable<IGrouping<GroupKey, PackageEntity>> Group(IEnumerable<PackageEntity> entities, uint level, PackageContext context);
        Task<IEnumerable<IGrouping<GroupKey, PackageEntity>>> GroupAsync(IEnumerable<PackageEntity> entities, uint level, PackageContext context, CancellationToken ct);
    }
}
