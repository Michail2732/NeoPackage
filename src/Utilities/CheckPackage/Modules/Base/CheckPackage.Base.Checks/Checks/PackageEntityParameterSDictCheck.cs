using CheckPackage.Core.Checks;
using CheckPackage.Core.Context;
using Package.Abstraction.Entities;
using Package.Localization;
using Package.Validation.Context;
using System;
using System.Linq;

namespace CheckPackage.Base.Checks
{
    public class PackageEntityParameterSDictCheck : PackageEntityCheckCommand
    {
        public string DictionaryName { get; }
        public string ParameterId { get; }

        public PackageEntityParameterSDictCheck(string dictionaryName, string errorMessage, 
            string parameterId): base(errorMessage)
        {
            DictionaryName = dictionaryName ?? throw new ArgumentNullException(nameof(dictionaryName));
            ParameterId = parameterId ?? throw new ArgumentNullException(nameof(parameterId));
        }
        

        protected override Result InnerCheck(PackageEntity entity, CheckPackageContext context)
        {
            if (!entity.Parameters.ContainsKey(ParameterId))
                return Result.Error(context.MessageBuilder.Get(MessageKeys.NotFoundParameterInEntity, ParameterId, entity.Name));
            string parameterValue = entity.Parameters[ParameterId!];
            var dictionary = context.Resources.GetStorage<SimpleDictionaryResource, string>().
                GetItem(DictionaryName);
            bool result = dictionary?.Contains(parameterValue) == true;
            return new Result(result, null);
        }
    }
}
