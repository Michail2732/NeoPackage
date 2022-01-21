using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Package.Validation.Validators
{
    public interface IEntityUserParameterValidator
    {
        EntityStateResult Validate(UserParameter userparameter, PackageContext context);
        Task<EntityStateResult> ValidateAsync(UserParameter userparameter, PackageContext context, CancellationToken ct);
    }
}
