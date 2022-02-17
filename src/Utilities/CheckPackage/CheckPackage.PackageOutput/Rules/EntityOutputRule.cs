using CheckPackage.Core.Conditions;
using CheckPackage.Core.Output;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;

namespace CheckPackage.PackageOutput.Rules
{
    public class EntityOutputRule: IRepositoryItem<string>
    {
        public string Id { get; }
        public Critical State { get; }
        public IReadOnlyList<EntityConditionCommand>? Conditions { get; }
        public EntityOutputCommand OutputCommand { get; }

        public EntityOutputRule(string id, Critical state, 
            IReadOnlyList<EntityConditionCommand>? conditions, EntityOutputCommand output)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            State = state;
            Conditions = conditions;
            OutputCommand = output ?? throw new ArgumentNullException(nameof(output));
        }
    }
}
