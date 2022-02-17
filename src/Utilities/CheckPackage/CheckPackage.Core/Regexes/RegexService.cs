using CheckPackage.Core.Resources;
using Package.Abstraction.Services;
using Package.Repository.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CheckPackage.Core.Regexes
{
    public class RegexService : IRegexService
    {
        private readonly IRepositoriesProvider _resourceProvider;

        public RegexService(IRepositoriesProvider resourceProvider)
        {
            _resourceProvider = resourceProvider ?? throw new ArgumentNullException(nameof(resourceProvider));
        }        

        public RegexMatchResult Match(RegexObject regexObj)
        {        
            var parceResult = Parce(regexObj);            
            var regex = new Regex(parceResult.Pattern, RegexOptions.None, TimeSpan.FromSeconds(10));
            var match = regex.Match(regexObj.InputString);
            return RegexMatchResult.BuildFromMatch(regex, match, parceResult);            
        }

        // todo: messages
        public RegexParceResult Parce(RegexObject regexObj)
        {
            string psevdoName = "param";    
            var regexResources = _resourceProvider.GetRepository<RegexItemResource, string>();
            StringBuilder result = new StringBuilder(regexObj.RegexPattern);
            MatchCollection patternCollection = Regex.Matches(regexObj.RegexPattern,
                @"\$\{(?<" + psevdoName + @">[^\}]+)\}", RegexOptions.None);
            MatchCollection interpolationCollection = Regex.Matches(regexObj.RegexPattern,
                @"\%\{(?<" + psevdoName + @">[^\}]+)\}", RegexOptions.None);
            List<string> regexGroups = new List<string>(patternCollection.Count);
            Dictionary<string, string> interpolatedGroups = new Dictionary<string, string>();
            for (int i = 0; i < patternCollection.Count; i++)
            {
                string elementPair = patternCollection[i].Groups[psevdoName].Value;
                var splitResult = elementPair.Split(';');
                if (splitResult.Length == 2)
                {
                    string patternId = splitResult[0].Trim();
                    string patternName = splitResult[1].Trim();
                    var regexItem = regexResources.GetItem(patternId);
                    if (regexItem == null)
                        throw new RepositoryItemNotFoundException($"todo: messages");
                    result = result.Replace(patternCollection[i].Groups[psevdoName].Value, $"(?<{patternName}>{regexItem.RegexPattern})");
                    if (!regexGroups.Contains(patternName))
                        regexGroups.Add(patternName);
                }                
            }
            for (int i = 0; i < interpolationCollection.Count; i++)
            {
                string elementPair = interpolationCollection[i].Groups[psevdoName].Value;
                var splitResult = elementPair.Split(';');
                if (splitResult.Length == 2)
                {
                    string groupName = splitResult[0].Trim();
                    string groupValue = splitResult[1].Trim();                    
                    result = result.Replace(interpolationCollection[i].Groups[psevdoName].Value, "");
                    interpolatedGroups[groupName] = groupValue;
                }
            }
            return new RegexParceResult(result.ToString(), regexGroups, interpolatedGroups);
        }       
    }
}
