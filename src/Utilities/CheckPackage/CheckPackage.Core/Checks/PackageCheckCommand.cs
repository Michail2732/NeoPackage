using CheckPackage.Core.Entities;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Core.Checks
{
    public abstract class PackageCheckCommand: ICheckCommand<Package_>
    {
        public string Message { get; }
        public LogicalOperator Logic { get; set; }
        public bool Inverse { get; set; }

        protected PackageCheckCommand(string message)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
        }        
        public Result Check(Package_ package, PackageContext context)
        {
            var result = InnerCheck(package, context);
            return Inverse ? new Result(!result.IsSuccess, result.Details) : result;
        }

        protected abstract Result InnerCheck(Package_ package, PackageContext context);
                
    }
}
