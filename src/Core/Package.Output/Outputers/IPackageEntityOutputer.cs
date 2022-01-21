using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Package.Output.Outputers
{
    public interface IPackageEntityOutputer
    {
        EntityStateResult Output(PackageEntity entity, PackageContext context);
        Task<EntityStateResult> OutputAsync(PackageEntity entity, PackageContext context, CancellationToken ct);
    }
}
