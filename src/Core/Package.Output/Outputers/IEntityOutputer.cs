using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Package.Output.Outputers
{
    public interface IEntityOutputer
    {
        EntityStateResult Output(Entity_ entity, PackageContext context);
        Task<EntityStateResult> OutputAsync(Entity_ entity, PackageContext context, CancellationToken ct);
    }
}
