using CheckPackage.Core.Entities;
using Microsoft.Extensions.Logging;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckPackage.Core.Extractors
{
    public abstract class ParameterExtractCommand
    {


        public IEnumerable<Parameter> ExtractParameters(IEnumerable<Parameter> source, PackageContext context) => InnerExtractParameters(source, context);

        protected abstract IEnumerable<Parameter> InnerExtractParameters(IEnumerable<Parameter> source, PackageContext context);

        // todo: messages
        protected bool IsSourceNotEmpty(IEnumerable<Parameter> source, PackageContext context) 
        {
            if (!source.Any())
            {
                context.Logger.LogError("todo: messages");
                return false;
            }
            return true;
        }
    }
}
