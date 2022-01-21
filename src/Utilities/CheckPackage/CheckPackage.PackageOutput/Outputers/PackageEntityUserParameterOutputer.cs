using Package.Abstraction.Entities;
using Package.Output.Outputers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CheckPackage.PackageOutput.Outputers
{
    public class PackageEntityUserParameterOutputer : IPackageEntityUserParameterOutputer
    {
        public EntityStateResult Output(UserParameter parameter, PackageContext context)
        {
            return new EntityStateResult(parameter.Id, null, State.success);
        }

        public async Task<EntityStateResult> OutputAsync(UserParameter parameter, PackageContext context, CancellationToken ct)
        {
            return new EntityStateResult(parameter.Id, null, State.success);
        }
    }
}
