using CheckPackage.Core.Resources;
using Package.Abstraction.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CheckPackage.Core.Regex
{
    public class RegexTemplateService : IRegexTemplateService
    {
        private readonly IResourceStoragesProvider _resourceProvider;

        public RegexTemplateService(IResourceStoragesProvider resourceProvider)
        {
            _resourceProvider = resourceProvider ?? throw new ArgumentNullException(nameof(resourceProvider));
        }

        public string BuildRegexPattern(RegexTemplate template)
        {
            StringBuilder result = new StringBuilder(template.Template);
            var groupParamsResource = _resourceProvider.GetStorage<GroupParametersTemplateResource, string>();
            var paramsResource = _resourceProvider.GetStorage<ParameterTemplateResource, string>();
            foreach (var groupParamsId in template.GroupParametersPatternsIds)
            {
                var groupParamPattern = groupParamsResource.GetItem(groupParamsId)?.TemplateRaw;
                result = result.Replace("&{" + groupParamsId + "}", groupParamPattern ?? "");
            }
            string psevdoName = "psevdoName";
            MatchCollection paramsPatternsIdsResult = System.Text.RegularExpressions.Regex.Matches(result.ToString(),
                 @"\$\{(?<" + psevdoName + @">[^\}]+)\}", RegexOptions.None);
            foreach (Match parameterPatternIdMatch in paramsPatternsIdsResult)
            {
                var paramPatternId = parameterPatternIdMatch.Groups[psevdoName].Value;
                var paramPattern = paramsResource.GetItem(paramPatternId);
                result = result.Replace("${" + paramPatternId + "}", paramPattern != null ?
                    $"(?<{paramPatternId}>{paramPattern.RegexTemplate})" : "");
            }
            return result.ToString();
        }

        public bool TryMatch(string inputRow, RegexTemplate template, out Match result)
        {
            string regexPattern = BuildRegexPattern(template);            
            var regex = new System.Text.RegularExpressions.Regex(regexPattern, RegexOptions.None);                        
            result = regex.Match(inputRow);
            return result.Success;            
        }
    }
}
