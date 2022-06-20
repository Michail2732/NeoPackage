using System;
using System.Collections.Generic;
using Package.Infrastructure;
using Package.Utility.Booleans;

namespace Package.Exporting.Rules
{
    public sealed class ExportRule<TItem>
    {
        private readonly IReadOnlyList<IBooleanExpression<TItem, InfrastructureContext>> _conditions;
        public readonly string EndPointId;

        public ExportRule(
            IReadOnlyList<IBooleanExpression<TItem, InfrastructureContext>> conditions,
            string endPointId)
        {
            _conditions =
                conditions ?? throw new ArgumentNullException(nameof(conditions));
            EndPointId = endPointId ?? throw new ArgumentNullException(nameof(endPointId));
        }


        public bool IsMatch(
            TItem item,
            InfrastructureContext context)
            => BooleanSolver.Solve(_conditions, item, context);
    }
}