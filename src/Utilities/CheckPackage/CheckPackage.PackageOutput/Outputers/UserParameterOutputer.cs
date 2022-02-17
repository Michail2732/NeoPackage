using Package.Abstraction.Entities;
using Package.Output.Outputers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CheckPackage.PackageOutput.Outputers
{
    public class UserParameterOutputer : IUserParameterOutputer
    {
        public EntityStateResult Output(UserParameter_ parameter, PackageContext context)
        {
            return new EntityStateResult(parameter.Id, null, Critical.notcritical);
        }

        public async Task<EntityStateResult> OutputAsync(UserParameter_ parameter, PackageContext context, CancellationToken ct)
        {
            return new EntityStateResult(parameter.Id, null, Critical.notcritical);
        }
    }
}
