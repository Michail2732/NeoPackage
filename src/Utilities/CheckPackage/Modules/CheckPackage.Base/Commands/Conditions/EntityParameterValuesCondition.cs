using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CheckPackage.Base.Extensions;
using CheckPackage.Core.Conditions;
using CheckPackage.Core.Entities;
using Package.Abstraction.Entities;

namespace CheckPackage.Base.Commands
{
    public class EntityParameterValuesCondition : EntityConditionCommand
    {
        public string ParameterId { get; }
        public string[] Values { get; }
        public bool IsContains { get; }

        public EntityParameterValuesCondition(string parameterId, string[] values, bool isContains)
        {
            ParameterId = parameterId ?? throw new ArgumentNullException(nameof(parameterId));
            Values = values ?? throw new ArgumentNullException(nameof(values));
            IsContains = isContains;
        }


        protected override bool InnerResolve(Entity_ entity, PackageContext context)
        {
            if (!entity.Parameters.ContainsKey(ParameterId))
                return false;
            string parameterValue = entity.Parameters[ParameterId];            
            return IsContains ? Values.Contains(parameterValue) : !Values.Contains(parameterValue);
        }
    }
}
