using Package.Abstraction.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CheckPackage.Core.Regex
{
    //TODO: поставить время ограничения на регулярках
    public class RegexTemplate
    {
        public readonly IReadOnlyList<string> ParameterPatternsIds;
        public readonly IReadOnlyList<string> GroupParametersPatternsIds;
        public readonly string Template;

        public RegexTemplate(string template)
        {
            Template = template;
            string psevdoName = "param";
            MatchCollection keyRegexTemplates = System.Text.RegularExpressions.Regex.Matches(
               Template, @"\&\{(?<" + psevdoName + @">[^\}]+)\}", RegexOptions.None);
            MatchCollection keyRegexElements = System.Text.RegularExpressions.Regex.Matches(
                Template, @"\$\{(?<" + psevdoName + @">[^\}]+)\}", RegexOptions.None);
            ParameterPatternsIds = new List<string>(keyRegexElements.Count);
            GroupParametersPatternsIds = new List<string>(keyRegexTemplates.Count);
            for (int i = 0; i < keyRegexElements.Count; i++)
                ((IList<string>)ParameterPatternsIds).Add(keyRegexElements[i].Groups[psevdoName].Value);
            for (int i = 0; i < keyRegexTemplates.Count; i++)
                ((IList<string>)GroupParametersPatternsIds).Add(keyRegexTemplates[i].Groups[psevdoName].Value);            
        }

        

    }
}
