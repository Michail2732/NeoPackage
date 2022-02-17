using CheckPackage.Base.Extensions;
using CheckPackage.Core.Checks;
using CheckPackage.Core.Entities;
using Package.Abstraction.Entities;
using Package.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace CheckPackage.Base.Commands
{
    public class EntityParameterValueCheck : EntityCheckCommand
    {
        public string ParameterId { get; }
        public string Value { get; }
        public ValueAction Operator { get; }

        public EntityParameterValueCheck(string parameterId, string errorMessage, 
            string value, ValueAction @operator, ChildEntitiesCheck? childCheck = null) : base(errorMessage, childCheck)
        {
            if (string.IsNullOrEmpty(parameterId))            
                throw new ArgumentException(nameof(parameterId));            
            ParameterId = parameterId;
            Value = value ?? throw new ArgumentNullException(nameof(value));
            Operator = @operator;
        }        

        protected override Result InnerCheck(Entity_ entity, PackageContext context)
        {
            if (!entity.Parameters.ContainsKey(ParameterId))            
                return Result.Error(context.Messages[MessageKeys.NotFoundParameterInEntity, ParameterId, entity.Name]);            
            string parameterValue = entity.Parameters[ParameterId];
            bool result = Operator.Resolve(parameterValue, Value);
            return new Result(result, null);
        }
    }
}
