using CheckPackage.Core.Checks;
using CheckPackage.Core.Conditions;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.PackageValidation.Rules
{
    public class PackageCheckRule : IRepositoryItem<string>
    {
        public string Id { get; }
        public Critical State { get; }
        public IReadOnlyList<PackageConditionCommand>? Conditions { get; }
        public IReadOnlyList<PackageCheckCommand> Checks { get; }


        public PackageCheckRule(string id, IReadOnlyList<PackageConditionCommand>? conditions,
            IReadOnlyList<PackageCheckCommand> checks, Critical state)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Conditions = conditions;
            Checks = checks ?? throw new ArgumentNullException(nameof(checks));
            State = state;
        }
    }
}
