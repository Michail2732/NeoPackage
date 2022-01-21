using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Package.Validation.Services
{
    public interface IPackageValidationService
    {
        PackageReport Validate(Package_ package);
        Task<PackageReport> ValidateAsync(Package_ package, CancellationToken ct);
        event EventHandler<EntityValidationEventArgs> EntityValidated;
    }
}
