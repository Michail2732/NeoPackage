using CheckPackage.Core.Check;
using CheckPackage.Core.Checks;
using CheckPackage.Core.Context;
using Package.Abstraction.Entities;
using Package.Localization;
using Package.Validation.Context;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace CheckPackage.Base.Checks
{
    public class PackageEntityParameterCheck : PackageEntityCheckCommand
    {
        public string ParameterId { get; }
        public string? Value { get; }
        public CheckOperatorType Operator { get; }

        public PackageEntityParameterCheck(string parameterId, string errorMessage, 
            string? value, CheckOperatorType @operator): base(errorMessage)
        {
            if (string.IsNullOrEmpty(parameterId))            
                throw new ArgumentException(nameof(parameterId));            
            ParameterId = parameterId;
            Value = value;
            Operator = @operator;
        }        

        protected override Result InnerCheck(PackageEntity entity, CheckPackageContext context)
        {
            if (!entity.Parameters.ContainsKey(ParameterId))            
                return Result.Error(context.MessageBuilder.Get(MessageKeys.NotFoundParameterInEntity, ParameterId, entity.Name));            
            string parameterValue = entity.Parameters[ParameterId];
            bool result = Operator.Resolve(parameterValue, Value);
            return new Result(result, null);
        }
    }
}
