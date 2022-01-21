using CheckPackage.Core.Condition;
using CheckPackage.Core.Context;
using Package.Abstraction.Entities;
using Package.Localization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckPackage.Base.Conditions
{
    public sealed class ContainsConditionResolver : PackageEntityCondition
    {
        public IReadOnlyList<string> Values { get; }
        public string ParameterId { get; }

        public ContainsConditionResolver(IReadOnlyList<string> values, string parameterId)
        {
            Values = values ?? throw new ArgumentNullException(nameof(values));
            ParameterId = parameterId ?? throw new ArgumentNullException(nameof(parameterId));
        }        

        protected override bool InnerResolve(PackageEntity entity, CheckPackageContext context)
        {
            if (!entity.Parameters.ContainsKey(ParameterId))
                return false;
            string parameterValue = entity.Parameters[ParameterId];
            return Values.Contains(parameterValue);
        }
    }
}
