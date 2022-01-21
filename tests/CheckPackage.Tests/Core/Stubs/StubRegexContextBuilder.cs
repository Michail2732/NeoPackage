using CheckPackage.Core.Abstract;
using CheckPackage.Core.Regex;
using CheckPackage.Localizer;
using Microsoft.Extensions.Localization;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Tests.Core.Stubs
{
    public class StubRegexContextBuilder : ContextBuilder<RegexContext>
    {
        public StubRegexContextBuilder( ) 
            : base(Substitute.For<IRepositoryProvider>(), 
                  new MessagesService(Substitute.For<IStringLocalizer<
                      MessagesService>>()))
        {
        }

        public override RegexContext Build()
        {
            return new StubRegexContext();
        }
    }
}
