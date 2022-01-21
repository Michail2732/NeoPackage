using Package.Abstraction.Entities;
using Package.Output.Outputers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CheckPackage.PackageOutput.Outputers
{
    public class PackageEntityParameterOutputer : IPackageEntityParameterOutputer
    {
        public EntityStateResult Output(KeyValuePair<string, string> parameter, PackageContext context)
        {
            return new EntityStateResult(parameter.Key, null, State.success);
        }

        public async Task<EntityStateResult> OutputAsync(KeyValuePair<string, string> parameter, PackageContext context, CancellationToken ct)
        {
            return new EntityStateResult(parameter.Key, null, State.success);
        }
    }
}
