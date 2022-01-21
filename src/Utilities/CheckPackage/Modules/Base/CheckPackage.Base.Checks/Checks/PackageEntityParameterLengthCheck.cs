using CheckPackage.Core.Checks;
using CheckPackage.Core.Context;
using Package.Abstraction.Entities;
using Package.Localization;
using Package.Validation.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Base.Checks
{
    public class PackageEntityParameterLengthCheck : PackageEntityCheckCommand
    {
        public string ParameterId { get; }
        public uint MinLength { get; }
        public uint MaxLength { get; }

        public PackageEntityParameterLengthCheck(string parameterId, string errorMessage,
            uint minLength, uint maxLength): base(errorMessage)
        {
            if (string.IsNullOrEmpty(parameterId))            
                throw new ArgumentException(nameof(parameterId));
            if (minLength > maxLength)
                throw new ArgumentException(nameof(minLength));
            ParameterId = parameterId;
            MinLength = minLength;
            MaxLength = maxLength;
        }       

        protected override Result InnerCheck(PackageEntity entity, CheckPackageContext context)
        {
            if (!entity.Parameters.ContainsKey(ParameterId))
                return Result.Error(context.MessageBuilder.Get(MessageKeys.NotFoundParameterInEntity, ParameterId, entity.Name));
            string parameterValue = entity.Parameters[ParameterId];
            bool result = parameterValue.Length >= MinLength && parameterValue.Length <= MaxLength;
            return new Result(result, null);
        }
    }
}
