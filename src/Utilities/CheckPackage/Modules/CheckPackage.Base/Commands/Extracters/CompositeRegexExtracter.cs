using CheckPackage.Core.Entities;
using CheckPackage.Core.Extractors;
using CheckPackage.Core.Regexes;
using CheckPackage.Core.Resources;
using Microsoft.Extensions.Logging;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CheckPackage.Base.Commands
{
    public sealed class CompositeRegexExtracter : ParameterExtractCommand
    {        
        public string RegexPatternId { get; }             

        public CompositeRegexExtracter(string groupParametersTemplateId)
        {
            RegexPatternId = groupParametersTemplateId ?? throw new ArgumentNullException(nameof(groupParametersTemplateId));            
        }

        //todo: messages
        protected override IEnumerable<Parameter> InnerExtractParameters(IEnumerable<Parameter> paramsSource, PackageContext context)
        {
            if (!IsSourceNotEmpty(paramsSource, context))
                yield break;
            var groupParamTemplates = context.RepositoryProvider.GetRepository<RegexItemResource, string>();
            var groupParamTemp = groupParamTemplates.GetItem(RegexPatternId);
            string sourceValue = paramsSource.First().Value.ToString();
            if (groupParamTemp == null)
            {
                context.Logger.LogError($"todo: messages {nameof(CompositeRegexExtracter)}");
                yield break;
            }
            var regexService = context.GetService<IRegexService>();
            RegexObject regexObject = new RegexObject(sourceValue, 
                groupParamTemp.RegexPattern, groupParamTemp.IsComposite);
            var matchResult = regexService.Match(regexObject);
            if (!matchResult.IsMatch)            
            {
                context.Logger.LogError($"todo: messages {nameof(CompositeRegexExtracter)}");
                yield break;
            }
            if (matchResult.Groupes != null)
                foreach (var group in matchResult.Groupes)                                    
                     yield return new Parameter(group.Key, group.Value);
            yield break;
        }
        
    }
}
