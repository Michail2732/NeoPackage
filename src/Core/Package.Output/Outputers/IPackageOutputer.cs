using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Package.Output.Outputers
{
    public interface IPackageOutputer
    {
        EntityStateResult Output(Package_ package, PackageContext context);
        Task<EntityStateResult> OutputAsync(Package_ package, PackageContext context, CancellationToken ct);
    }
}
