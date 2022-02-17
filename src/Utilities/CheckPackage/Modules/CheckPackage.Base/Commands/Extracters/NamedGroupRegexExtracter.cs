using CheckPackage.Core.Entities;
using CheckPackage.Core.Extractors;
using CheckPackage.Core.Regexes;
using Microsoft.Extensions.Logging;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CheckPackage.Base.Commands
{
    public sealed class NamedGroupRegexExtracter : ParameterExtractCommand
    {
        public string RegexPattern { get; }
        public string GroupName { get; }            

        public NamedGroupRegexExtracter(string regexTemplate, string groupName) 
        {                        
            RegexPattern = regexTemplate ?? throw new ArgumentNullException(nameof(regexTemplate));
            GroupName = groupName ?? throw new ArgumentNullException(nameof(groupName));            
        }        

        // todo: messages
        protected override IEnumerable<Parameter> InnerExtractParameters(IEnumerable<Parameter> paramsSource, PackageContext context)
        {
            if (!IsSourceNotEmpty(paramsSource, context))
                yield break;
            string sourceValue = paramsSource.First().Value.ToString();            
            RegexObject regexObject = new RegexObject(sourceValue, RegexPattern, false);
            var regexService = context.GetService<IRegexService>();
            var matchResult = regexService.Match(regexObject);
            if (!matchResult.IsMatch || matchResult.Groupes?.ContainsKey(GroupName) != true)
            {
                context.Logger.LogError($"todo: messages {nameof(NamedGroupRegexExtracter)}");
                yield break;
            }            
            yield return new Parameter(GroupName, matchResult.Groupes[GroupName]);
        }
    }
}
