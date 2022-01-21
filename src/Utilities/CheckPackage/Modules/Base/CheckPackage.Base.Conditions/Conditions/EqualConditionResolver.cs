using CheckPackage.Core.Condition;
using CheckPackage.Core.Context;
using Package.Abstraction.Entities;
using System;

namespace CheckPackage.Base.Conditions
{
    public sealed class EqualConditionResolver : PackageEntityCondition
    {
        public string? Value { get; }
        public string ParameterId { get; }

        public EqualConditionResolver(string? value, string parameterId)
        {
            Value = value;
            ParameterId = parameterId ?? throw new ArgumentNullException(nameof(parameterId));
        }        

        protected override bool InnerResolve(PackageEntity entity, CheckPackageContext context)
        {
            if (!entity.Parameters.ContainsKey(ParameterId))
                return false;
            string parameterValue = entity.Parameters[ParameterId];
            return parameterValue == Value;
        }
    }
}
