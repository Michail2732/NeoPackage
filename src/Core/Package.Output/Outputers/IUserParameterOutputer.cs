using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Package.Output.Outputers
{
    public interface IUserParameterOutputer
    {
        EntityStateResult Output(UserParameter_ parameter, PackageContext context);
        Task<EntityStateResult> OutputAsync(UserParameter_ parameter, PackageContext context, CancellationToken ct);

    }
}
