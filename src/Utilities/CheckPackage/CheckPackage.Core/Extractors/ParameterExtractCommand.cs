using CheckPackage.Core.Entities;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;

namespace CheckPackage.Core.Extractors
{
    public abstract class ParameterExtractCommand
    {
        public IList<Parameter> ExtractParameters(IList<Parameter> source, PackageContext context) => InnerExtractParameters(source, context);

        protected abstract IList<Parameter> InnerExtractParameters(IList<Parameter> source, PackageContext context);        
    }
}
