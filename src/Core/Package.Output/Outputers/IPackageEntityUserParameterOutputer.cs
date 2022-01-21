using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Package.Output.Outputers
{
    public interface IPackageEntityUserParameterOutputer
    {
        EntityStateResult Output(UserParameter parameter, PackageContext context);
        Task<EntityStateResult> OutputAsync(UserParameter parameter, PackageContext context, CancellationToken ct);

    }
}
