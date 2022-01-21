using CheckPackage.Core.Conditions;
using CheckPackage.Core.Output;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;

namespace CheckPackage.PackageOutput.Resources
{
    public class EntityOutputRuleResource: IEntity<string>
    {
        public string Id { get; }
        public State State { get; }
        public IReadOnlyList<PackageEntityConditionCommand>? Conditions { get; }
        public PackageEntityOutputCommand OutputCommand { get; }

        public EntityOutputRuleResource(string id, State state, 
            IReadOnlyList<PackageEntityConditionCommand>? conditions, PackageEntityOutputCommand output)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            State = state;
            Conditions = conditions;
            OutputCommand = output ?? throw new ArgumentNullException(nameof(output));
        }
    }
}
