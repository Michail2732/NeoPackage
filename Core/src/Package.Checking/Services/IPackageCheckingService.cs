using Package.Checking.Rules;
using Package.Domain;
using Package.Utility.Collection;

namespace Package.Checking.Services
{
    
    using CheckRuleList = CollectionsLocator<CheckRule<PackageItem>, CheckRule<RootPackage>>;
    public interface IPackageCheckingService
    {
        CheckReport CheckPackage(
            RootPackage package,
            CheckRuleList checkRuleList);
        
        //todo: implement async checks
        // Task<CheckReport> CheckPackageAsync(
        //     RootPackage package, 
        //     AsyncCheckRuleList checkRuleList,
        //     CancellationToken ct);
    }
}