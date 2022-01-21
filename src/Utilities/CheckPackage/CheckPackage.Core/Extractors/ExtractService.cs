using CheckPackage.Core.Entities;
using Microsoft.Extensions.Logging;
using Package.Abstraction.Entities;
using Package.Abstraction.Services;
using Package.Localization;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Core.Extractors
{
    public class ExtractService : IExtractService
    {
        private readonly IPackageContextBuilder _contextBuilder;
        private readonly ILogger<ExtractService> _logger;

        public ExtractService(IPackageContextBuilder contextBuilder, ILogger<ExtractService> logger)
        {
            _contextBuilder = contextBuilder ?? throw new ArgumentNullException(nameof(contextBuilder));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IList<Parameter> Extract(IList<Parameter> source, IEnumerable<ParameterExtractCommand> extracters)
        {
            var context = _contextBuilder.Build();
            List<Parameter> result = new List<Parameter>();
            var parameters = source;
            foreach (var extracter in extracters)
            {
                parameters = Extract(parameters, extracter);
                if (parameters.Count > 0)
                    foreach (var parameter in parameters)
                    {
                        if (result.Exists(a => a.Id == parameter.Id))
                            result.Remove(result.Find(a => a.Id == parameter.Id));
                        result.Add(parameter);
                    }                
            }
            return result;
        }

        public IList<Parameter> Extract(IList<Parameter> source, ParameterExtractCommand extracter)
        {
            var context = _contextBuilder.Build();                        
            var parameters = extracter.ExtractParameters(source, context);
            if (parameters.Count == 0)            
                _logger.LogError(context.Messages[MessageKeys.CouldNotExtractParameters]);                            
            return parameters;
        }

        public IList<Parameter> Extract(Parameter source, ParameterExtractCommand extracter)
        {
            throw new NotImplementedException();
        }

        public IList<Parameter> Extract(Parameter source, IEnumerable<ParameterExtractCommand> extracter)
        {
            throw new NotImplementedException();
        }
    }
}
