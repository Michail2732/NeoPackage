using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Package.Validation.Validators
{
    public interface IEntityValidator             
    {
        EntityStateResult Validate(Entity_ entity, PackageContext context);
        Task<EntityStateResult> ValidateAsync(Entity_ entity, PackageContext context, CancellationToken ct);
    }
}
