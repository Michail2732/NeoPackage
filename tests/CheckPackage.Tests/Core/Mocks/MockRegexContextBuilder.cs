using CheckPackage.Core.Abstract;
using CheckPackage.Core.Regex;
using CheckPackage.Localizer;
using Microsoft.Extensions.Localization;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Tests.Core.Mocks
{
    public class MockRegexContextBuilder : ContextBuilder<RegexContext>
    {
        public uint CountCallBuild { get; private set; }
        private RegexContext _contextResult;

        public MockRegexContextBuilder SetBuildResult(List<ParameterTemplate>? paramTemps = null,
            List<RegexTemplate>? regexTemps = null)
        {
            _contextResult = new MockRegexContext(paramTemps, regexTemps);
            return this;
        }

        public MockRegexContextBuilder SetBuildResult(string id, string regexTemplateRaw)
        {
            _contextResult = new MockRegexContext(null, new List<RegexTemplate> { 
            new RegexTemplate(id, regexTemplateRaw)});
            return this;
        }


        public MockRegexContextBuilder(List<ParameterTemplate>? paramTemps = null,
            List<RegexTemplate>? regexTemps = null) 
            : base(Substitute.For<IRepositoryProvider>(), 
                  new MessagesService(Substitute.For<IStringLocalizer<
                      MessagesService>>()))
        {
            _contextResult = new MockRegexContext(paramTemps, regexTemps);
        }

        public override RegexContext Build()
        {
            CountCallBuild++;
            return _contextResult;
        }
    }
}
