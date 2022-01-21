using CheckPackage.Core.Entities;
using Package.Abstraction.Entities;
using System;

namespace CheckPackage.Core.Checks
{
    public abstract class PackageEntityCheckCommand : ICheckCommand<PackageEntity>
    {
        public string Message { get; }
        public LogicalOperator Logic { get; set; }
        public bool Inverse { get; set; }

        protected PackageEntityCheckCommand(string message)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
        }
        
        public Result Check(PackageEntity entity, PackageContext context)
        {                        
            var result = InnerCheck(entity, context);
            return Inverse ? new Result(!result.IsSuccess, result.Details) : result;
        }

        protected abstract Result InnerCheck(PackageEntity entity, PackageContext context);                
    }
}
