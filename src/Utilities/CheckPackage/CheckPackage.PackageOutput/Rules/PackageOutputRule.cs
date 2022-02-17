using CheckPackage.Core.Conditions;
using CheckPackage.Core.Output;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.PackageOutput.Rules
{
    public class PackageOutputRule : IRepositoryItem<string>
    {
        public string Id { get; }
        public Critical State { get; }
        public IReadOnlyList<PackageConditionCommand>? Conditions { get; }
        public PackageOutputCommand  OutputCommand { get; }

        public PackageOutputRule(string id, IReadOnlyList<PackageConditionCommand>? conditions,
            PackageOutputCommand output, Critical state)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            State = state;
            Conditions = conditions;
            OutputCommand = output ?? throw new ArgumentNullException(nameof(output));
        }
    }
}
