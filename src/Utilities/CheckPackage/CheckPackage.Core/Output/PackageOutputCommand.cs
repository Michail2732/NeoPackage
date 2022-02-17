using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Core.Output
{
    public abstract class PackageOutputCommand : IOutputCommand<Package_>
    {
        public string Message { get; }        

        protected PackageOutputCommand(string message)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));            
        }

        public Result Output(Package_ entity, PackageContext context) => InnerOutput(entity, context);

        protected abstract Result InnerOutput(Package_ entity, PackageContext context);
    }
}
