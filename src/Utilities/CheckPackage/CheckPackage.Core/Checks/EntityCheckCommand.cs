using CheckPackage.Core.Entities;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using Package.Abstraction.Extensions;
using System.Linq;

namespace CheckPackage.Core.Checks
{
    public abstract class EntityCheckCommand : ICheckCommand<Entity_>
    {
        public string Message { get; }
        public ChildEntitiesCheck? ChildCheck { get; }
        public Logical Logic { get; set; }
        public bool Inverse { get; set; }


        protected EntityCheckCommand(string message, ChildEntitiesCheck? childCheck)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
            ChildCheck = childCheck;
        }

        public Result Check(Entity_ entity, PackageContext context)
        {
            Result result = default;
            if (ChildCheck == null)
                result = InnerCheck(entity, context);
            else
            {
                EntityStackEnumerable entitesEnumerable = new EntityStackEnumerable(entity);
                var entitesForCheck = entitesEnumerable.Where(a => ChildCheck.Levels.Contains(a.Level));
                IEnumerable<Result> results = entitesForCheck.Select(a => InnerCheck(a, context));
                Result aggregateResult = results.ToAggregateResult(ChildCheck.Logic == Logical.and ? true : false);
            }            
            return Inverse ? new Result(!result.IsSuccess, result.Details) : result;
        }

        protected abstract Result InnerCheck(Entity_ entity, PackageContext context);                
    }



    public class ChildEntitiesCheck
    {
        public readonly IReadOnlyList<uint> Levels;
        public readonly Logical Logic;

        public ChildEntitiesCheck(IReadOnlyList<uint> levels, Logical @operator)
        {
            Levels = levels ?? throw new ArgumentNullException(nameof(levels));
            Logic = @operator;
        }
    }
}
