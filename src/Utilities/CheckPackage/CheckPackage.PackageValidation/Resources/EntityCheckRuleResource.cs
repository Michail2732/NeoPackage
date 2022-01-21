using CheckPackage.Core.Checks;
using CheckPackage.Core.Conditions;
using CheckPackage.Core.Entities;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.PackageValidation.Resources
{
    public class EntityCheckRuleResource: IEntity<string>
    {
        public string Id { get; }        
        public State State { get; }
        public IReadOnlyList<PackageEntityConditionCommand>? Conditions { get; }
        public IReadOnlyList<PackageEntityCheckCommand> Checks { get; }

        public EntityCheckRuleResource(string id, IReadOnlyList<PackageEntityConditionCommand>? conditions,
            IReadOnlyList<PackageEntityCheckCommand> parameterRules, State state)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Conditions = conditions;
            Checks = parameterRules ?? throw new ArgumentNullException(nameof(parameterRules));
            State = state;
        }
    }
}
