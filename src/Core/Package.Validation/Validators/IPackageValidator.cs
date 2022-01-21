using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Package.Validation.Validators
{
    public interface IPackageValidator
    {
        EntityStateResult Validate(Package_ package, PackageContext context);
        Task<EntityStateResult> ValidateAsync(Package_ package, PackageContext context, CancellationToken ct);
    }
}
