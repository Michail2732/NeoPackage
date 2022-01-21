using CheckPackage.Core.Regex;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Tests.Core.Stubs
{
    public class StubRegexTemplateParcer : IRegexTemplateParcer
    {
        public RegexAnalysisInfo Analyzes(RegexTemplate template)
        {
            return new RegexAnalysisInfo(new List<string>(),
                new List<string>());
        }

        public string Parce(RegexTemplate template, RegexContext context)
        {
            return "safsfsfasdfdsfa";
        }
    }
}
