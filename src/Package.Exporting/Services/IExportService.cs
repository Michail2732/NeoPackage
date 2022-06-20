using System.Threading;
using System.Threading.Tasks;
using Package.Domain;
using Package.Exporting.Rules;
using Package.Utility.Collection;

namespace Package.Exporting.Services
{
    using ExportRulesList = CollectionsLocator<ExportRule<PackageItem>, ExportRule<RootPackage>>;

    public interface IExportService
    {
        Task<ExportReport> ExportAsync(
            RootPackage package,
            ExportRulesList rules,
            CancellationToken ct);
    }
}