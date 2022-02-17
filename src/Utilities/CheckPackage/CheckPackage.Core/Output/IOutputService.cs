using CheckPackage.Core.Entities;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Core.Output
{
    public interface IOutputService
    {
        Result Output(Package_ package, PackageOutputCommand command);
        Result Output(Entity_ entity, EntityOutputCommand command);
        Result Output(Parameter parameter, ParameterOutputCommand command);
    }
}
