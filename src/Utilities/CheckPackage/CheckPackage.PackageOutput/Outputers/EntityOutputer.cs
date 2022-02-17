using CheckPackage.Core.Conditions;
using CheckPackage.PackageOutput.Rules;
using Package.Abstraction.Entities;
using Package.Output.Outputers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CheckPackage.PackageOutput.Outputers
{
    public class EntityOutputer : IEntityOutputer
    {
        private readonly IConditionService _conditionService;

        public EntityOutputer(IConditionService conditionService)
        {
            _conditionService = conditionService ?? throw new ArgumentNullException(nameof(conditionService));
        }

        public EntityStateResult Output(Entity_ entity, PackageContext context)
        {
            StringBuilder sb = new StringBuilder();
            Critical resultState = Critical.notcritical;
            var entityRules = context.RepositoryProvider.GetRepository<EntityOutputRule, string>().Get();
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

        public async Task<EntityStateResult> OutputAsync(Entity_ entity, PackageContext context, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            StringBuilder sb = new StringBuilder();
            Critical resultState = Critical.notcritical;
            var entityRules = await context.RepositoryProvider.GetRepository<EntityOutputRule, string>().GetAsync(a => true, ct);
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
