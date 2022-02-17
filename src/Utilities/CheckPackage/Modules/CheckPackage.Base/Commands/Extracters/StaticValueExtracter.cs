using CheckPackage.Core.Entities;
using CheckPackage.Core.Extractors;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;

namespace CheckPackage.Base.Commands
{
    public class StaticValueExtracter : ParameterExtractCommand
    {
        public string Value { get; }        
        public string ParameterId { get; }

        public StaticValueExtracter(string value, string parameterId) 
        {             
            Value = value ?? throw new System.ArgumentNullException(nameof(value));
            if (string.IsNullOrEmpty(parameterId))
                throw new ArgumentNullException(parameterId);
            ParameterId = parameterId;
        }

        protected override IEnumerable<Parameter> InnerExtractParameters(IEnumerable<Parameter> paramsSource, PackageContext context)
        {
            yield return new Parameter(ParameterId, Value);
        }        
    }
}
