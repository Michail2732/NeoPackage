using CheckPackage.Core.Entities;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Core.Conditions
{
    internal interface IConditionCommand<TItem>
    {
        Logical Logic { get; set; }
        bool Inverse { get; set; }

        bool Resolve(TItem item, PackageContext context);
    }
}
