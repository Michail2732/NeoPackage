using CheckPackage.Core.Conditions;
using CheckPackage.Core.Output;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.PackageOutput.Rules
{
    public class ParameterOutputRule: IRepositoryItem<string>
    {
        public string Id { get; }
        public string ParameterId { get; }
        public bool IsUserParameter { get; }
        public Critical State { get; }        
        public ParameterOutputCommand OutputCommand { get; }

        public ParameterOutputRule(string id, string parameterId, bool isUserParameter, Critical state, 
            ParameterOutputCommand outputCommand)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            ParameterId = parameterId ?? throw new ArgumentNullException(nameof(parameterId));
            IsUserParameter = isUserParameter;
            State = state;
            OutputCommand = outputCommand ?? throw new ArgumentNullException(nameof(outputCommand));
        }
    }
}
