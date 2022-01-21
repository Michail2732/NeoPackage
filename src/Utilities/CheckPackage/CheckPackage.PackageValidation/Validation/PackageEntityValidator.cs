using CheckPackage.Core.Checks;
using CheckPackage.Core.Conditions;
using CheckPackage.PackageValidation.Resources;
using Package.Abstraction.Entities;
using Package.Validation.Validators;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CheckPackage.PackageValidation.Validation
{
    public class PackageEntityValidator : IPackageEntityValidator
    {
        private readonly IConditionService _conditionService;
        private readonly ICheckService _checkService;

        public PackageEntityValidator(IConditionService conditionResolverExec, ICheckService checkerExec)
        {
            _conditionService = conditionResolverExec ?? throw new ArgumentNullException(nameof(conditionResolverExec));
            _checkService = checkerExec ?? throw new ArgumentNullException(nameof(checkerExec));
        }

        public EntityStateResult Validate(PackageEntity entity, PackageContext context)
        {
            var entityCheckRules = context.ResourceProvider.GetStorage<EntityCheckRuleResource, string>()
                    .Get(a => a.Conditions == null || _conditionService.Resolve(entity, a.Conditions));                                                
            StringBuilder sb = new StringBuilder();
            State state = State.success;
            foreach (var checkRule in entityCheckRules)
            {
                var result = _checkService.Check(entity, checkRule.Checks);
                if (!result.IsSuccess)
                {
                    if (string.IsNullOrEmpty(result.Details)) sb.Append($"{result.Details}\n");
                    state = checkRule.State > state ? checkRule.State : state;
                }                
            }
            return new EntityStateResult(entity.Name, sb.ToString(), state);
        }

        public async Task<EntityStateResult> ValidateAsync(PackageEntity entity, PackageContext context, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            var entityCheckRules = await context.ResourceProvider.GetStorage<EntityCheckRuleResource, string>()
                    .GetAsync(a => a.Conditions == null || _conditionService.Resolve(entity, a.Conditions), ct);
            StringBuilder sb = new StringBuilder();
            State state = State.success;
            foreach (var checkRule in entityCheckRules)
            {
                ct.ThrowIfCancellationRequested();
                var result = _checkService.Check(entity, checkRule.Checks);
                if (!result.IsSuccess)
                {
                    if (string.IsNullOrEmpty(result.Details)) sb.Append($"{result.Details}\n");
                    state = checkRule.State > state ? checkRule.State : state;
                }
            }
            return new EntityStateResult(entity.Name, sb.ToString(), state);
        }
    }
}
