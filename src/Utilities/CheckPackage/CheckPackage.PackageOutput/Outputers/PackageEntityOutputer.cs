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
    public class PackageEntityOutputer : IPackageEntityOutputer
    {
        private readonly IConditionService _conditionService;

        public PackageEntityOutputer(IConditionService conditionService)
        {
            _conditionService = conditionService ?? throw new ArgumentNullException(nameof(conditionService));
        }

        public EntityStateResult Output(PackageEntity entity, PackageContext context)
        {
            StringBuilder sb = new StringBuilder();
            State resultState = State.success;
            var entityRules = context.ResourceProvider.GetStorage<EntityOutputRuleResource, string>().Get();
            foreach (var entityRule in entityRules)
            {
                if (entityRule.Conditions == null || _conditionService.Resolve(entity, entityRule.Conditions))
                {
                    var result = entityRule.OutputCommand.Output(entity, context);
                    if (!result.IsSuccess)
                    {
                        resultState = entityRule.State > resultState ? entityRule.State : resultState;
                        sb.Append($"{entityRule.OutputCommand.Message}\n");
                    }
                }
            }
            return new EntityStateResult(entity.Name, sb.ToString(), resultState);
        }

        public async Task<EntityStateResult> OutputAsync(PackageEntity entity, PackageContext context, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            StringBuilder sb = new StringBuilder();
            State resultState = State.success;
            var entityRules = await context.ResourceProvider.GetStorage<EntityOutputRuleResource, string>().GetAsync(a => true, ct);
            foreach (var entityRule in entityRules)
            {
                ct.ThrowIfCancellationRequested();
                if (entityRule.Conditions == null || _conditionService.Resolve(entity, entityRule.Conditions))
                {
                    var result = entityRule.OutputCommand.Output(entity, context);
                    if (!result.IsSuccess)
                    {
                        resultState = entityRule.State > resultState ? entityRule.State : resultState;
                        sb.Append($"{entityRule.OutputCommand.Message}\n");
                    }
                }
            }
            return new EntityStateResult(entity.Name, sb.ToString(), resultState);
        }
    }
}
