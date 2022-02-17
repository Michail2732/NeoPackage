using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Package.Validation.Validators
{
    public interface IUserParameterValidator
    {
        EntityStateResult Validate(UserParameter_ userparameter, PackageContext context);
        Task<EntityStateResult> ValidateAsync(UserParameter_ userparameter, PackageContext context, CancellationToken ct);
    }
}
