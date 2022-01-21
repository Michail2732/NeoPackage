using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Core.Dependencies
{
    public interface ICheckPackageModule
    {
        void AddDependencies(CheckPackageOptions options);
        void AddDependencies(IServiceCollection collection);
    }
}
