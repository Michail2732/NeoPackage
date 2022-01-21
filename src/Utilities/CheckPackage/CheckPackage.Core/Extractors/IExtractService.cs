using CheckPackage.Core.Entities;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Core.Extractors
{
    public interface IExtractService
    {
        public IList<Parameter> Extract(IList<Parameter> source, IEnumerable<ParameterExtractCommand> extracter);
        public IList<Parameter> Extract(IList<Parameter> source, ParameterExtractCommand extracter);
        public IList<Parameter> Extract(Parameter source, ParameterExtractCommand extracter);
        public IList<Parameter> Extract(Parameter source, IEnumerable<ParameterExtractCommand> extracter);
    }
}
