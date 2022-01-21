using CheckPackage.Core.Regex;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Tests.Core.Mocks
{
    public class MockRegexTemplateParcer : IRegexTemplateParcer
    {
        private RegexAnalysisInfo _analyzesInfoResult;
        private string _parceResult;

        public uint CountCallAnalyzes { get; private set; }
        public uint CountCallParce { get; private set; }

        public MockRegexTemplateParcer SetAnalyzesResult(List<string>? regexTempIds,
            List<string>? regexParamIds)
        {
            _analyzesInfoResult = new RegexAnalysisInfo(regexTempIds ??
                new List<string>(), regexParamIds ?? new List<string>());
            return this;
        }

        public MockRegexTemplateParcer SetParceResult(string res)
        {
            _parceResult = res;
            return this;
        }

        public MockRegexTemplateParcer()
        {
            _analyzesInfoResult = new RegexAnalysisInfo(new List<string>(),
                new List<string>());
            _parceResult = "";
        }

        public RegexAnalysisInfo Analyzes(RegexTemplate template)
        {
            CountCallAnalyzes++;
            return _analyzesInfoResult;
        }

        public string Parce(RegexTemplate template, RegexContext context)
        {
            CountCallParce++;
            return _parceResult;
        }
    }
}
