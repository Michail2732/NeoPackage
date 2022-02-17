using CheckPackage.Base.Extensions;
using CheckPackage.Core.Conditions;
using CheckPackage.Core.Entities;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Base.Commands
{
    public class EntityParameterValueCondition : EntityConditionCommand
    {
        public string ParameterId { get; }
        public string Value { get; }
        public ValueAction Operator { get; }

        public EntityParameterValueCondition(string parameterId, string value, ValueAction @operator)
        {
            ParameterId = parameterId ?? throw new ArgumentNullException(nameof(parameterId));
            Value = value ?? throw new ArgumentNullException(nameof(value));
            Operator = @operator;
        }

        protected override bool InnerResolve(Entity_ entity, PackageContext context)
        {
            if (!entity.Parameters.ContainsKey(ParameterId))
                return false;
            string parameterValue = entity.Parameters[ParameterId];
            return Operator.Resolve(parameterValue, Value);            
        }
    }
}
