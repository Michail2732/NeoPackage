using Package.Abstraction.Entities;
using Package.Abstraction.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Core.Output
{
    public class OutputService : IOutputService
    {
        private readonly IPackageContextBuilder _contextBuilder;

        public OutputService(IPackageContextBuilder contextBuilder)
        {
            _contextBuilder = contextBuilder ?? throw new ArgumentNullException(nameof(contextBuilder));
        }

        public Result Output(Package_ package, PackageOutputCommand command)
        {
            var context = _contextBuilder.Build();
            return command.Output(package, context);
        }

        public Result Output(PackageEntity entity, PackageEntityOutputCommand command)
        {
            var context = _contextBuilder.Build();
            return command.Output(entity, context);
        }
    }
}
