using Package.Domain;
using System.Threading;
using System.Threading.Tasks;
using Package.Infrastructure;

namespace Package.Exporting.Rules
{
    public interface IEndpoint
    {
        string Id { get; }

        Task<ExportResult> ExportAsync(
            RootPackage package,
            InfrastructureContext context,
            CancellationToken ct);

        Task<ExportResult> ExportAsync(
            PackageItem packageItem,
            InfrastructureContext context,
            CancellationToken ct);
    }
}