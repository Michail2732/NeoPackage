using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CheckPackage.Core.Regex
{
    public interface IRegexTemplateService
    {
        string BuildRegexPattern(RegexTemplate template);
        bool TryMatch(string inputRow, RegexTemplate template, out Match result);
    }
}
