using CheckPackage.Core.Checks;
using CheckPackage.Core.Conditions;
using CheckPackage.Core.Entities;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.PackageValidation.Rules
{
    public class EntityCheckRule: IRepositoryItem<string>
    {
        public string Id { get; }        
        public Critical State { get; }
        public IReadOnlyList<EntityConditionCommand>? Conditions { get; }
        public IReadOnlyList<EntityCheckCommand> Checks { get; }

        public EntityCheckRule(string id, IReadOnlyList<EntityConditionCommand>? conditions,
            IReadOnlyList<EntityCheckCommand> parameterRules, Critical state)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Conditions = conditions;
            Checks = parameterRules ?? throw new ArgumentNullException(nameof(parameterRules));
            State = state;
        }
    }
}
