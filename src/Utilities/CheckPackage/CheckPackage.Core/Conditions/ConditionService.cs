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

        public bool Resolve(Entity_ entity, EntityConditionCommand command)
            => ResolvePrivate(entity, command);

        public bool Resolve(Entity_ entity, IReadOnlyList<EntityConditionCommand> commands)
            => ResolvePrivate(entity, commands);

        public bool Resolve(Package_ package, IReadOnlyList<PackageConditionCommand> commands)
            => ResolvePrivate(package, commands);

        public bool Resolve(Package_ package, PackageConditionCommand command)
            => ResolvePrivate(package, command);        

        public bool Resolve(UserParameter_ parameter, ParameterConditionCommand command)
            => ResolvePrivate(new Parameter(parameter.Id, parameter.Value), command);

        public bool Resolve(UserParameter_ parameter, IReadOnlyList<ParameterConditionCommand> commands)
            => ResolvePrivate(new Parameter(parameter.Id, parameter.Value), commands);

        public bool Resolve(KeyValuePair<string, string> parameter, ParameterConditionCommand command)
            => ResolvePrivate(new Parameter(parameter.Key, parameter.Value), command);

        public bool Resolve(KeyValuePair<string, string> parameter, IReadOnlyList<ParameterConditionCommand> commands)
            => ResolvePrivate(new Parameter(parameter.Key, parameter.Value), commands);

        private bool ResolvePrivate<T>(T obj, IConditionCommand<T> condition)
        {
            var context = _contextBuilder.Build();            
            return condition.Resolve(obj, context);                       
        }

        private bool ResolvePrivate<T>(T obj, IReadOnlyList<IConditionCommand<T>> conditions)
        {
            var context = _contextBuilder.Build();            
            if (conditions.Count > 0) conditions.First().Logic = Logical.or;
            return BooleanSolver.Solve(conditions.Select(a => 
            new KeyValuePair<Logical, Func<bool>>(a.Logic, () => a.Resolve(obj, context))).ToList());            
        }
    }
}
