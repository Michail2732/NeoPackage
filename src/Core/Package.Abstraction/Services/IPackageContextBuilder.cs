using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Package.Abstraction.Services
{
    public interface IPackageContextBuilder
    {
        PackageContext Build();
    }
}
