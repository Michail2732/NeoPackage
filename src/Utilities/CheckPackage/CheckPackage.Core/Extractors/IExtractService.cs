using CheckPackage.Core.Entities;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Core.Extractors
{
    public interface IExtractService
    {
        public IEnumerable<Parameter> Extract(IEnumerable<Parameter> source, IEnumerable<ParameterExtractCommand> extracter);
        public IEnumerable<Parameter> Extract(IEnumerable<Parameter> source, ParameterExtractCommand extracter);
        public IEnumerable<Parameter> Extract(Parameter source, ParameterExtractCommand extracter);
        public IEnumerable<Parameter> Extract(Parameter source, IEnumerable<ParameterExtractCommand> extracter);
    }
}
