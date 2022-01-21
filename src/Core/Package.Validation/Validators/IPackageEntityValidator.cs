using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Package.Validation.Validators
{
    public interface IPackageEntityValidator             
    {
        EntityStateResult Validate(PackageEntity entity, PackageContext context);
        Task<EntityStateResult> ValidateAsync(PackageEntity entity, PackageContext context, CancellationToken ct);
    }
}
