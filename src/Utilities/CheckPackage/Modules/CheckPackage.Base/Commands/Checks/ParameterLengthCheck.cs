using CheckPackage.Core.Checks;
using CheckPackage.Core.Entities;
using Package.Abstraction.Entities;
using Package.Localization;
using System;

namespace CheckPackage.Base.Commands
{
    public class ParameterLengthCheck : ParameterCheckCommand
    {        
        public uint MinLength { get; }
        public uint MaxLength { get; }

        public ParameterLengthCheck(string errorMessage,
            uint minLength, uint maxLength): base(errorMessage)
        {     
            if (minLength > maxLength)
                throw new ArgumentException(nameof(minLength));         
            MinLength = minLength;
            MaxLength = maxLength;
        }       

        protected override Result InnerCheck(Parameter parameter, PackageContext context)
        {            
            string parameterValue = parameter.Value.ToString();
            bool result = parameterValue.Length >= MinLength && parameterValue.Length <= MaxLength;
            return new Result(result, null);
        }
    }
}
