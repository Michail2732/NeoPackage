using CheckPackage.Core.Checks;
using CheckPackage.Core.Entities;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.PackageValidation.Rules
{
    public class ParameterCheckRule : IRepositoryItem<string>
    {
        public string Id { get; }
        public string ParameterId { get; }
        public bool IsUserParameter { get; }
        public Critical State { get; }
        public IReadOnlyList<ParameterCheckCommand> Checks { get; }

        public ParameterCheckRule(string id, string parameterId, Critical state,
            IReadOnlyList<ParameterCheckCommand> checks, bool isUserParameter)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            ParameterId = parameterId ?? throw new ArgumentNullException(nameof(parameterId));
            State = state;
            Checks = checks ?? throw new ArgumentNullException(nameof(checks));
            IsUserParameter = isUserParameter;
        }
    }
}
