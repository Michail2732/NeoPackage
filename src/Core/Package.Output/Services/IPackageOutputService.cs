using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Package.Output.Services
{
    public interface IPackageOutputService
    {
        PackageReport Output(Package_ package);
        Task<PackageReport> OutputAsync(Package_ package, CancellationToken ct);
        event EventHandler<EntityOutputEventArgs> EntityOutputted;
    }
}
