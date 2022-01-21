using CheckPackage.Core.Condition;
using CheckPackage.Core.Context;
using CheckPackage.Core.Regex;
using Package.Abstraction.Entities;
using System;
using System.Text.RegularExpressions;

namespace CheckPackage.Base.Conditions
{
    public class RegexConditionResolver : PackageEntityCondition
    {
        public string RegexTemplate { get; }
        public string ParameterId { get; }

        public RegexConditionResolver(string regexTemplate, string parameterId)
        {
            RegexTemplate = regexTemplate ?? throw new ArgumentNullException(nameof(regexTemplate));
            ParameterId = parameterId ?? throw new ArgumentNullException(nameof(parameterId));
        }
        

        protected override bool InnerResolve(PackageEntity entity, CheckPackageContext context)
        {
            if (!entity.Parameters.ContainsKey(ParameterId))
                return false;
            var parameters = context.Resources.GetStorage<ParameterTemplateResource, string>().Get();
            var groupParameters = context.Resources.GetStorage<GroupParametersTemplateResource, string>().Get();
            string parameterValue = entity.Parameters[ParameterId];
            RegexTemplateValue regexValue = new RegexTemplateValue(RegexTemplate);
            string? regexPattern = RegexTemplateValue.ConvertToRegexPattern(regexValue, context.Resources);
            if (regexPattern == null) return false;
            var regex = new Regex(regexPattern, RegexOptions.None);
            return regex.Match(parameterValue).Success;
        }
    }
}
