using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Package.Validation.Validators
{
    public interface IParameterValidator
    {
        EntityStateResult Validate(KeyValuePair<string, string> parameter, PackageContext context);
        Task<EntityStateResult> ValidateAsync(KeyValuePair<string, string> parameter, PackageContext context, CancellationToken ct);
    }
}
