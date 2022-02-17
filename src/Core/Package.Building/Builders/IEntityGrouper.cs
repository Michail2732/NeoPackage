using Package.Abstraction.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Package.Building.Builders
{
    public interface IEntityGrouper
    {
        IEnumerable<IGrouping<GroupKey, Entity_>> Group(IEnumerable<Entity_> entities, uint level, PackageContext context);
        Task<IEnumerable<IGrouping<GroupKey, Entity_>>> GroupAsync(IEnumerable<Entity_> entities, uint level, PackageContext context, CancellationToken ct);
    }
}
