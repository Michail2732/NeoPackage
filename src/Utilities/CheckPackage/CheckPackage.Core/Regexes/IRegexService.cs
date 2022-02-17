using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CheckPackage.Core.Regexes
{
    public interface IRegexService
    {
        RegexMatchResult Match(RegexObject regexObj);
        RegexParceResult Parce(RegexObject regexObj);
    }
}
