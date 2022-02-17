using CheckPackage.Base.Extensions;
using CheckPackage.Core.Checks;
using CheckPackage.Core.Entities;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Base.Commands
{
    public class ParameterValueCheck: ParameterCheckCommand
    {
        public string Value { get; }
        public ValueAction Operator { get; }

        public ParameterValueCheck(string errorMessage, string value, ValueAction @operator) : 
            base(errorMessage)
        {                        
            Value = value ?? throw new ArgumentNullException(nameof(value));
            Operator = @operator;
        }

        protected override Result InnerCheck(Parameter parameter, PackageContext context)
        {            
            string parameterValue = parameter.ToString();
            bool result = Operator.Resolve(parameterValue, Value);
            return new Result(result, null);
        }
    }
}
