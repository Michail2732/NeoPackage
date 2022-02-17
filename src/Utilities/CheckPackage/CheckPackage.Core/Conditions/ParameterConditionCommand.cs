using CheckPackage.Core.Entities;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Core.Conditions
{
    public abstract class ParameterConditionCommand : IConditionCommand<Parameter>
    {
        public Logical Logic { get; set; }
        public bool Inverse { get; set; }

        public bool Resolve(Parameter item, PackageContext context)
        {
            return Inverse ? !InnerResolve(item, context) : InnerResolve(item, context);
        }

        protected abstract bool InnerResolve(Parameter parameter, PackageContext context);
    }
}
