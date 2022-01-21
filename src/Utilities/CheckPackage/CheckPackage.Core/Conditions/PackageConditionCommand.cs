using CheckPackage.Core.Entities;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Core.Conditions
{
    public abstract class PackageConditionCommand : IConditionCommand<Package_>
    {
        public LogicalOperator Logic { get; set; }
        public bool Inverse { get; set; }        

        public  bool Resolve(Package_ package, PackageContext context)
        {            
            return Inverse ? !InnerResolve(package, context) : InnerResolve(package, context);
        }


        protected abstract bool InnerResolve(Package_ package, PackageContext context);
    }
}
