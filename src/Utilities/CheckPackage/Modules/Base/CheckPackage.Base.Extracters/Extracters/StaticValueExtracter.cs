using CheckPackage.Core.Context;
using CheckPackage.Core.Extracts;
using System;
using System.Collections.Generic;

namespace CheckPackage.Base.Extracters
{
    public class StaticValueExtracter : ParameterExtracter
    {
        public string Value { get; }        
        public string ParameterId { get; }

        public StaticValueExtracter(string value, string parameterId) :
            base(PackageEntityFilter.Empty, ParameterSelector.Empty)
        {             
            Value = value ?? throw new System.ArgumentNullException(nameof(value));
            if (string.IsNullOrEmpty(parameterId))
                throw new ArgumentNullException(parameterId);
            ParameterId = parameterId;
        }

        public override IEnumerable<ParameterResult> ExtractParameters(IEnumerable<ParameterResult> paramsSource, CheckPackageContext context)
        {
            yield return new ParameterResult(ParameterId, Value);
        }        
    }
}
