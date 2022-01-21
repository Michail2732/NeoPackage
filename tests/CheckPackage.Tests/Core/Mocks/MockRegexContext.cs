using CheckPackage.Core.Abstract;
using CheckPackage.Core.Regex;
using CheckPackage.Localizer;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Tests.Core.Mocks
{
    public class MockRegexContext : RegexContext
    {
        


        public MockRegexContext(List<ParameterTemplate>? paramTemps = null, 
            List<RegexTemplate>? regexTemps = null) 
            : base(new MockRepository<ParameterTemplate, string>(paramTemps),
                  new MockRepository<RegexTemplate, string>(regexTemps), 
                  new MessagesService(NSubstitute.Substitute.For<
                      IStringLocalizer<MessagesService>>()))
        {
        }
    }
}
