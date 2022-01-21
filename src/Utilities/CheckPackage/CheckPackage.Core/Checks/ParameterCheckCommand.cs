using CheckPackage.Core.Entities;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Core.Checks
{
    public abstract class ParameterCheckCommand: ICheckCommand<Parameter>
    {
        public string Message { get; }
        public LogicalOperator Logic { get; set; }
        public bool Inverse { get; set; }

        protected ParameterCheckCommand(string message)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
        }
        

        public Result Check(Parameter parameter, PackageContext context)
        {
            var result = InnerCheck(parameter, context);
            return Inverse ? new Result(!result.IsSuccess, result.Details) : result;
        }

        protected abstract Result InnerCheck(Parameter parameter, PackageContext context);        
    }
}
