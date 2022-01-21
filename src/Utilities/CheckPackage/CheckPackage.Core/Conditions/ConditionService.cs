using CheckPackage.Core.Entities;
using Package.Abstraction.Entities;
using Package.Abstraction.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPackage.Core.Conditions
{
    public class ConditionService :  IConditionService
    {
        private readonly IPackageContextBuilder _contextBuilder;

        public ConditionService(IPackageContextBuilder contextBuilder)
        {
            _contextBuilder = contextBuilder ?? throw new ArgumentNullException(nameof(contextBuilder));
        }

        public bool Resolve(PackageEntity entity, PackageEntityConditionCommand condition)
            => ResolvePrivate(entity, condition);

        public bool Resolve(PackageEntity entity, IReadOnlyList<PackageEntityConditionCommand> conditions)
            => ResolvePrivate(entity, conditions);

        public bool Resolve(Package_ package, IReadOnlyList<PackageConditionCommand> conditions)
            => ResolvePrivate(package, conditions);

        public bool Resolve(Package_ package, PackageConditionCommand condition)
            => ResolvePrivate(package, condition);

        private bool ResolvePrivate<T>(T obj, IConditionCommand<T> condition)
        {
            var context = _contextBuilder.Build();            
            return condition.Resolve(obj, context);                       
        }

        private bool ResolvePrivate<T>(T obj, IReadOnlyList<IConditionCommand<T>> conditions)
        {
            var context = _contextBuilder.Build();            
            if (conditions.Count > 0) conditions.First().Logic = LogicalOperator.or;
            return BooleanSolver.Solve(conditions.Select(a => 
            new KeyValuePair<LogicalOperator, Func<bool>>(a.Logic, () => a.Resolve(obj, context))).ToList());            
        }
    }
}
