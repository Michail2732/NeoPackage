using CheckPackage.Core.Entities;
using CheckPackage.Core.Extractors;
using CheckPackage.Core.Regexes;
using CheckPackage.Core.Resources;
using Microsoft.Extensions.Logging;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPackage.Base.Commands
{
    public class AutoCompositeRegexExtractor : ParameterExtractCommand
    {
        public string? ParameterId { get; }

        public AutoCompositeRegexExtractor(string? parameterId)
        {
            ParameterId = parameterId;
        }


        // todo: messages
        protected override IEnumerable<Parameter> InnerExtractParameters(IEnumerable<Parameter> source, PackageContext context)
        {
            if (!IsSourceNotEmpty(source, context))
                yield break;
            var regexRepository = context.RepositoryProvider.GetRepository<RegexItemResource, string>();
            var compositeRegexItems = regexRepository.Get(a => a.IsComposite);
            string? sourceValue = source.First().Value.ToString();
            var regexService = context.GetService<IRegexService>();
            foreach (var compositeRegex in compositeRegexItems)
            {
                var regexObject = new RegexObject(sourceValue, compositeRegex.RegexPattern, true);
                var matchResult = regexService.Match(regexObject);
                if (matchResult.IsMatch)
                {
                    if (ParameterId != null)
                        yield return new Parameter(ParameterId, sourceValue);
                    else if (matchResult.Groupes != null)
                        foreach (var group in matchResult.Groupes)
                            yield return new Parameter(group.Key, group.Value);
                    yield break;
                }                    
            }
            context.Logger.LogError($"todo: messages");
            yield break;
        }
    }
}
