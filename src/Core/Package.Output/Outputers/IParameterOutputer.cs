using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Package.Output.Outputers
{
    public interface IParameterOutputer
    {
        EntityStateResult Output(KeyValuePair<string, string> parameter, PackageContext context);
        Task<EntityStateResult> OutputAsync(KeyValuePair<string, string> parameter, PackageContext context, CancellationToken ct);
    }
}
