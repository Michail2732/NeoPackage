using CheckPackage.Core.Context;
using CheckPackage.Core.Extracts;
using CheckPackage.Core.Regex;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CheckPackage.Base.Extracters
{
    public sealed class RegexTemplateExtracter : ParameterExtracter
    {
        public string ParameterId { get; }
        public string GroupParametersTemplateId { get; }        

        public RegexTemplateExtracter(PackageEntityFilter filter, ParameterSelector selector, 
            string groupParametersTemplateId, string parameterId): base(filter, selector)
        {
            GroupParametersTemplateId = groupParametersTemplateId ?? throw new ArgumentNullException(nameof(groupParametersTemplateId));
            if (string.IsNullOrEmpty(parameterId))
                throw new ArgumentNullException(parameterId);
            ParameterId = parameterId;
        }        

        public override IEnumerable<ParameterResult> ExtractParameters(IEnumerable<ParameterResult> paramsSource, CheckPackageContext context)
        {            
            var groupParamTemplates = context.Resources.GetStorage<GroupParametersTemplateResource, string>();
            var groupParamTemp = groupParamTemplates.GetItem(GroupParametersTemplateId);
            if (groupParamTemp == null) return EmptyParamsResult;

            RegexTemplateValue templateValue = new RegexTemplateValue(groupParamTemp.TemplateRaw);
            string? regexPattern = RegexTemplateValue.ConvertToRegexPattern(templateValue, context.Resources);
            if (regexPattern == null) return EmptyParamsResult;

            var regex = new Regex(regexPattern, RegexOptions.None);
            var groupNames = regex.GetGroupNames();
            string? sourceValue = paramsSource.FirstOrDefault().Value?.ToString();
            if (sourceValue == null) return EmptyParamsResult;

            var matchResult = regex.Match(sourceValue);
            if (!matchResult.Success) return EmptyParamsResult;
            List<ParameterResult> parameters = new List<ParameterResult>();
            foreach (string group in groupNames.Skip(1))
                if (!parameters.Any(a => a.Id == group))
                    parameters.Add(new ParameterResult(group, matchResult.Groups[group].Value));
            return parameters;
        }
        
    }
}
