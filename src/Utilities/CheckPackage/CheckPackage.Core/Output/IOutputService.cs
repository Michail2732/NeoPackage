using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Core.Output
{
    public interface IOutputService
    {
        Result Output(Package_ package, PackageOutputCommand command);
        Result Output(PackageEntity entity, PackageEntityOutputCommand command);
    }
}
