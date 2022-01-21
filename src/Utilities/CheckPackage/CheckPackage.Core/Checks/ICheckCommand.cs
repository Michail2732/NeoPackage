using CheckPackage.Core.Entities;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Core.Checks
{
    internal interface ICheckCommand<TItem>
    {
        Result Check(TItem item, PackageContext context);
        LogicalOperator Logic { get; set; }
        string Message { get; }
    }
}
