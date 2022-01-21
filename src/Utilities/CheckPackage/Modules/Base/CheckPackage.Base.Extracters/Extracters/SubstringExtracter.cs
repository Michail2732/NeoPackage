using CheckPackage.Core.Context;
using CheckPackage.Core.Extracts;
using CheckPackage.Core.Regex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CheckPackage.Base.Extracters
{
    public sealed class SubstringExtracter : ParameterExtracter
    {
        public string RegexTemplate { get; }
        public string GroupName { get; }    
        public string ParameterId { get; }

        public SubstringExtracter(string regexTemplate, string groupName, 
            string parameterId, PackageEntityFilter filter, ParameterSelector selector) : 
            base(filter, selector)
        {
            if (string.IsNullOrEmpty(parameterId))
                throw new ArgumentNullException(parameterId);
            ParameterId = parameterId;
            RegexTemplate = regexTemplate ?? throw new ArgumentNullException(nameof(regexTemplate));
            GroupName = groupName ?? throw new ArgumentNullException(nameof(groupName));            
        }        

        public override IEnumerable<ParameterResult> ExtractParameters(IEnumerable<ParameterResult> paramsSource, CheckPackageContext context)
        {
            RegexTemplateValue regexTempValue = new RegexTemplateValue(RegexTemplate);
            string? regexPattern = RegexTemplateValue.ConvertToRegexPattern(regexTempValue, context.Resources);
            if (regexPattern == null) return EmptyParamsResult;

            var regex = new Regex(regexPattern, RegexOptions.None);
            string? sourceValue = paramsSource.FirstOrDefault().Value?.ToString();
            if (sourceValue == null) return EmptyParamsResult;

            var matchResult = regex.Match(sourceValue);
            if (!matchResult.Success) return EmptyParamsResult;

            return new ParameterResult[1] { new ParameterResult(ParameterId, matchResult.Groups[GroupName].Value) };
        }
    }
}
