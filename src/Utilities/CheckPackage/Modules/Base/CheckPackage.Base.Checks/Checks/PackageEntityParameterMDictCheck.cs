using CheckPackage.Core.Checks;
using CheckPackage.Core.Context;
using CheckPackage.Core.Resources;
using Package.Abstraction.Entities;
using Package.Localization;
using Package.Validation.Context;
using System;
using System.Linq;

namespace CheckPackage.Base.Checks
{
    public class PackageEntityParameterMDictCheck : PackageEntityCheckCommand
    {
        public string DictionaryName { get; }
        public string SecondParameterId { get; }
        public string ParameterId { get; }

        public PackageEntityParameterMDictCheck(string dictionaryName, string errorMessage, 
            string secondParameterId, string parameterId): base(errorMessage)
        {
            DictionaryName = dictionaryName ?? throw new ArgumentNullException(nameof(dictionaryName));
            SecondParameterId = secondParameterId ?? throw new ArgumentNullException(nameof(secondParameterId));
            ParameterId = parameterId ?? throw new ArgumentNullException(nameof(parameterId));
        }        

        protected override Result InnerCheck(PackageEntity entity, CheckPackageContext context)
        {
            if (!entity.Parameters.ContainsKey(ParameterId))
                return Result.Error(context.MessageBuilder.Get(MessageKeys.NotFoundParameterInEntity, ParameterId, entity.Name));
            if (!entity.Parameters.ContainsKey(SecondParameterId))
                return Result.Error(context.MessageBuilder.Get(MessageKeys.NotFoundParameterInEntity, SecondParameterId, entity.Name));
            string keyParameterValue = entity.Parameters[SecondParameterId];
            string parameterValue = entity.Parameters[ParameterId];
            var dictionary = context.Resources.GetStorage<MatrixDictionaryResource, string>()
                .GetItem(DictionaryName);
            bool result = dictionary?[keyParameterValue]?.Contains(parameterValue) == true;
            return new Result(result, null);
        }
    }
}
