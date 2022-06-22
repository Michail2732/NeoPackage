using System;
using System.Threading;
using System.Threading.Tasks;
using Package.Building.Pipeline;
using Package.Domain;

namespace Package.Building.Services
{
    public interface IPackageBuildingService
    {
        RootPackage BuildPackage(
            PackageBuildingPipeline pipeline);
        
        // Task<RootPackage> BuildPackageAsync(
        //     PackageBuildingPipeline pipeline,
        //     CancellationToken ct);
    }
}
