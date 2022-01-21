using CheckPackage.Core.Abstract;
using CheckPackage.Core.Regex;
using CheckPackage.Localizer;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Tests.Core.Stubs
{
    public class StubRegexContext : RegexContext
    {
        public StubRegexContext() 
            : base(new StubRepository<ParameterTemplate, string>(null),
                  new StubRepository<RegexTemplate, string>(null), 
                  new MessagesService(NSubstitute.Substitute.For<
                      IStringLocalizer<MessagesService>>()))
        {
        }
    }
}
