using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Core.Output
{
    public interface IOutputCommand<T>
    {
        string Message { get; }
        Result Output(T item, PackageContext context);
    }
}
