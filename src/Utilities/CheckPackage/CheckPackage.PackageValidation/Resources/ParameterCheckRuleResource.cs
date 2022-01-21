using CheckPackage.Core.Checks;
using CheckPackage.Core.Entities;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.PackageValidation.Resources
{
    public class ParameterCheckRuleResource : IEntity<string>
    {
        public string Id { get; }
        public string ParameterId { get; }
        public bool IsUserParameter { get; }
        public State State { get; }
        public IReadOnlyList<ParameterCheckCommand> Checks { get; }

        public ParameterCheckRuleResource(string id, string parameterId, State state,
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
