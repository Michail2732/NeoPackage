using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Core.Output
{
    public abstract class PackageEntityOutputCommand
    {
        public string Message { get; }        

        protected PackageEntityOutputCommand(string message)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
        }


        public Result Output(PackageEntity entity, PackageContext context) => InnerOutput(entity, context);        

        protected abstract Result InnerOutput(PackageEntity entity, PackageContext context);
    }
}
