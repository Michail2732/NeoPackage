using CheckPackage.Core.Entities;
using CheckPackage.Core.Extractors;
using CheckPackage.Core.Resources;
using Microsoft.Extensions.Logging;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;

namespace CheckPackage.Base.Commands
{
    public class StaticParameterExtracter : ParameterExtractCommand
    {
        public string StaticParameterId { get; }        
        public string ParameterId { get; }

        public StaticParameterExtracter(string staticParameterId, string parameterId) 
        {
            if (string.IsNullOrEmpty(staticParameterId))            
                throw new ArgumentException(nameof(staticParameterId));                        
            StaticParameterId = staticParameterId;
            if (string.IsNullOrEmpty(parameterId))
                throw new ArgumentNullException(parameterId);
            ParameterId = parameterId;
        }

        // todo: message
        protected override IEnumerable<Parameter> InnerExtractParameters(IEnumerable<Parameter> paramsSource, PackageContext context)
        {
            var staticParameter = context.RepositoryProvider.GetRepository<StaticParameterResource, string>().GetItem(StaticParameterId);
            if (staticParameter == null)
            {
                context.Logger.LogError($"todo: messages {nameof(StaticParameterExtracter)}");
                yield break;
            }
            yield return new Parameter(ParameterId, staticParameter.Value);
        }        
    }
}
