using CheckPackage.Core.Entities;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Core.Output
{
    public abstract class ParameterOutputCommand: IOutputCommand<Parameter>
    {
        public string Message { get; }

        protected ParameterOutputCommand(string message)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
        }


        public Result Output(Parameter parameter, PackageContext context) => InnerOutput(parameter, context);

        protected abstract Result InnerOutput(Parameter parameter, PackageContext context);

    }
}
