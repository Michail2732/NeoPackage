using System;
using System.Collections.Generic;
using System.Linq;
using Package.Infrastructure;
using Package.Utility.Booleans;

namespace Package.Checking.Rules
{
    
    public class CheckRule<TItem>
    {
        public string Id { get; }
        public string ErrorMessage { get; }
        public IReadOnlyList<IBooleanExpression<TItem, InfrastructureContext>>? Conditions { get; }
        public IReadOnlyList<IBooleanExpression<TItem, InfrastructureContext>> Checks { get; }

        public CheckRule(
            IReadOnlyList<IBooleanExpression<TItem, InfrastructureContext>> checks,
            string errorMessage,
            IReadOnlyList<IBooleanExpression<TItem, InfrastructureContext>>? conditions = null,
            string? id = null)
        {
            Conditions = conditions;
            Id = id ?? Guid.NewGuid().ToString();
            Checks = checks ?? throw new ArgumentNullException(nameof(checks));
            ErrorMessage = errorMessage ?? throw new ArgumentNullException(nameof(errorMessage));
        }

        public bool Condition(TItem item, InfrastructureContext context)
        {
            if (Conditions == null)
                return true;
            return BooleanSolver.Solve(Conditions, item, context);
        }

        public bool Check(TItem item, InfrastructureContext context)
        {
            return BooleanSolver.Solve(Checks, item, context);
        }


    }
}