using CheckPackage.Core.Context;
using CheckPackage.Core.Extracts;
using System;
using System.Collections.Generic;

namespace CheckPackage.Base.Extracters
{
    public class StaticParameterExtracter : ParameterExtracter
    {
        public string StaticParameterId { get; }        
        public string ParameterId { get; }

        public StaticParameterExtracter(string staticParameterId, string parameterId) :
            base(PackageEntityFilter.Empty, ParameterSelector.Empty)
        {
            if (string.IsNullOrEmpty(staticParameterId))            
                throw new ArgumentException(nameof(staticParameterId));                        
            StaticParameterId = staticParameterId;
            if (string.IsNullOrEmpty(parameterId))
                throw new ArgumentNullException(parameterId);
            ParameterId = parameterId;
        }

        public override IEnumerable<ParameterResult> ExtractParameters(IEnumerable<ParameterResult> paramsSource, CheckPackageContext context)
        {
            var staticParameterValue = context.Resources.GetStorage<StaticParameterResource, string>().GetItem(StaticParameterId)?.Value;
            yield return new ParameterResult(ParameterId, staticParameterValue ?? string.Empty);
        }        
    }
}
