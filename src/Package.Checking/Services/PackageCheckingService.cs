using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Package.Checking.Exceptions;
using Package.Checking.Rules;
using Package.Domain;
using Package.Infrastructure;
using Package.Utility.Collection;

namespace Package.Checking.Services
{
    using CheckRuleList = CollectionsLocator<CheckRule<PackageItem>, CheckRule<RootPackage>>;
    
    public class PackageCheckingService: IPackageCheckingService
    {
        private readonly IInfrastructureContextBuilder _contextBuilder;

        public PackageCheckingService(IInfrastructureContextBuilder contextBuilder)
        {
            _contextBuilder = contextBuilder ?? throw new ArgumentNullException(nameof(contextBuilder));
        }

        public CheckReport CheckPackage(
            RootPackage package,
            CheckRuleList checkRuleList)
        {
            var context = _contextBuilder.Build();
            var report = new CheckReport();
            try
            {
                foreach (var packageCheck in checkRuleList.Items2)
                    if ( packageCheck.Condition(package, context))
                    {
                        bool checkRes = packageCheck.Check(package, context);
                        report.Add(checkRes ? new CheckResult(package.Id, packageCheck.Id): 
                            new CheckResult(package.Id, packageCheck.Id, packageCheck.ErrorMessage));
                    }

                foreach (var packageItemCheck in checkRuleList.Items1)
                {
                    var matchPackageItems = package.GetStackEnumerable().Where(a =>
                        packageItemCheck.Condition(a, context));
                    foreach (var matchPackageItem in matchPackageItems)
                    {
                        bool checkRes = packageItemCheck.Check(matchPackageItem, context);
                        report.Add(checkRes ? new CheckResult(matchPackageItem.Id, packageItemCheck.Id) :
                            new CheckResult(matchPackageItem.Id, packageItemCheck.Id, packageItemCheck.ErrorMessage));
                    }
                }
                return report;
            }
            catch (Exception e)
            {
                throw new PackageCheckingException("Error checking", e);
            }
        }

        //todo: implement async checks
        // public async Task<CheckReport> CheckPackageAsync(
        //     RootPackage package,
        //     AsyncCheckRuleList checkRuleList,
        //     CancellationToken ct)
        // {
        //     ct.ThrowIfCancellationRequested();
        //     var context = _contextBuilder.Build();
        //     var report = new CheckReport();
        //     try
        //     {
        //         foreach (var packageCheck in checkRuleList.PackageChecks)
        //             if (packageCheck.Condition(package, context))
        //             {
        //                 bool checkRes = await packageCheck.CheckAsync(package, context, ct);
        //                 report.Add(checkRes ? new CheckResult(package.Id, packageCheck.Id): 
        //                     new CheckResult(package.Id, packageCheck.Id, packageCheck.ErrorMessage));
        //                 ct.ThrowIfCancellationRequested();
        //             }
        //
        //         foreach (var packageItemCheck in checkRuleList.PackageItemChecks)
        //         {
        //             var matchPackageItems = package.GetStackEnumerable().Where(a =>
        //                 packageItemCheck.Condition(a, context));
        //             foreach (var matchPackageItem in matchPackageItems)
        //             {
        //                 bool checkRes = await packageItemCheck.CheckAsync(matchPackageItem, context, ct);
        //                 report.Add(checkRes ? new CheckResult(matchPackageItem.Id, packageItemCheck.Id) :
        //                     new CheckResult(matchPackageItem.Id, packageItemCheck.Id, packageItemCheck.ErrorMessage));
        //             }
        //             ct.ThrowIfCancellationRequested();
        //         }
        //         return report;
        //     }
        //     catch (Exception e)
        //     {
        //         throw new PackageCheckingException("Error checking", e);
        //     }
        // }
    }
}