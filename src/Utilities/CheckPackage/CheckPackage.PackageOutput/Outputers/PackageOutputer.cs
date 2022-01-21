using CheckPackage.Core.Conditions;
using CheckPackage.PackageOutput.Resources;
using Package.Abstraction.Entities;
using Package.Output.Outputers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CheckPackage.PackageOutput.Outputers
{
    public class PackageOutputer : IPackageOutputer
    {
        private readonly IConditionService _conditionService;

        public PackageOutputer(IConditionService conditionService)
        {
            _conditionService = conditionService ?? throw new ArgumentNullException(nameof(conditionService));
        }

        public EntityStateResult Output(Package_ package, PackageContext context)
        {
            StringBuilder sb = new StringBuilder();
            State resultState = State.success;
            var packageRules = context.ResourceProvider.GetStorage<PackageOutputRuleResource, string>().Get();
            foreach (var entityRule in packageRules)
            {
                if (entityRule.Conditions == null || _conditionService.Resolve(package, entityRule.Conditions))
                {
                    var result = entityRule.OutputCommand.Output(package, context);
                    if (!result.IsSuccess)
                    {
                        resultState = entityRule.State > resultState ? entityRule.State : resultState;
                        sb.Append($"{entityRule.OutputCommand.Message}\n");
                    }
                }
            }
            return new EntityStateResult(package.Name, sb.ToString(), resultState);
        }

        public async Task<EntityStateResult> OutputAsync(Package_ package, PackageContext context, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            StringBuilder sb = new StringBuilder();
            State resultState = State.success;
            var packageRules = await context.ResourceProvider.GetStorage<PackageOutputRuleResource, string>().GetAsync(a => true, ct);
            foreach (var entityRule in packageRules)
            {
                ct.ThrowIfCancellationRequested();
                if (entityRule.Conditions == null || _conditionService.Resolve(package, entityRule.Conditions))
                {
                    var result = entityRule.OutputCommand.Output(package, context);
                    if (!result.IsSuccess)
                    {
                        resultState = entityRule.State > resultState ? entityRule.State : resultState;
                        sb.Append($"{entityRule.OutputCommand.Message}\n");
                    }
                }
            }
            return new EntityStateResult(package.Name, sb.ToString(), resultState);
        }
    }
}
