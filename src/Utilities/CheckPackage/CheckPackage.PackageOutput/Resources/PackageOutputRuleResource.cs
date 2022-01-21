using CheckPackage.Core.Conditions;
using CheckPackage.Core.Output;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.PackageOutput.Resources
{
    public class PackageOutputRuleResource : IEntity<string>
    {
        public string Id { get; }
        public State State { get; }
        public IReadOnlyList<PackageConditionCommand>? Conditions { get; }
        public PackageOutputCommand  OutputCommand { get; }

        public PackageOutputRuleResource(string id, IReadOnlyList<PackageConditionCommand>? conditions,
            PackageOutputCommand output, State state)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            State = state;
            Conditions = conditions;
            OutputCommand = output ?? throw new ArgumentNullException(nameof(output));
        }
    }
}
