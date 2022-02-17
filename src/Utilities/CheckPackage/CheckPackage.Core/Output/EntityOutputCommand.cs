using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Core.Output
{
    public abstract class EntityOutputCommand: IOutputCommand<Entity_>
    {
        public string Message { get; }        

        protected EntityOutputCommand(string message)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
        }


        public Result Output(Entity_ entity, PackageContext context) => InnerOutput(entity, context);        

        protected abstract Result InnerOutput(Entity_ entity, PackageContext context);
    }
}
