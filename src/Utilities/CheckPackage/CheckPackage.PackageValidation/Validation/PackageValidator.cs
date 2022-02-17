using CheckPackage.Core.Checks;
using CheckPackage.Core.Conditions;
using CheckPackage.PackageValidation.Rules;
using Package.Abstraction.Entities;
using Package.Validation.Validators;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CheckPackage.PackageValidation.Validation
{
    public class PackageValidator : IPackageValidator
    {
        private readonly IConditionService _conditionService;
        private readonly ICheckService _checkService;

        public PackageValidator(IConditionService conditionResolverExec, ICheckService checkerExec)
        {
            _conditionService = conditionResolverExec ?? throw new ArgumentNullException(nameof(conditionResolverExec));
            _checkService = checkerExec ?? throw new ArgumentNullException(nameof(checkerExec));
        }

        public EntityStateResult Validate(Package_ package, PackageContext context)
        {            
            var packageCheckRules = context.RepositoryProvider.GetRepository<PackageCheckRule, string>()
                    .Get(a => a.Conditions == null || _conditionService.Resolve(package, a.Conditions));
            StringBuilder sb = new StringBuilder();
            Critical state = Critical.notcritical;            
            foreach (var packageCheckRule in packageCheckRules)
            {
                var result = _checkService.Check(package, packageCheckRule.Checks);
                if (!result.IsSuccess)
                {
                    if (string.IsNullOrEmpty(result.Details)) sb.Append($"{result.Details}\n");
                    state = packageCheckRule.State > state ? packageCheckRule.State : state;
                }
            }
            return new EntityStateResult(package.Name, sb.ToString(), state);
        }

        public async Task<EntityStateResult> ValidateAsync(Package_ package, PackageContext context, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            var packageCheckRules = await context.RepositoryProvider.GetRepository<PackageCheckRule, string>()
                    .GetAsync(a => a.Conditions == null || _conditionService.Resolve(package, a.Conditions), ct);
            StringBuilder sb = new StringBuilder();
            Critical state = Critical.notcritical;
            foreach (var packageCheckRule in packageCheckRules)
            {
                ct.ThrowIfCancellationRequested();
                var result = _checkService.Check(package, packageCheckRule.Checks);
                if (!result.IsSuccess)
                {
                    if (string.IsNullOrEmpty(result.Details)) sb.Append($"{result.Details}\n");
                    state = packageCheckRule.State > state ? packageCheckRule.State : state;
                }
            }
            return new EntityStateResult(package.Name, sb.ToString(), state);
        }
    }
}
