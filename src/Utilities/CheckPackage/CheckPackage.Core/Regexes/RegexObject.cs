using Package.Abstraction.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CheckPackage.Core.Regexes
{    
    public readonly struct RegexObject
    {
        public readonly string InputString;
        public readonly string RegexPattern;
        public readonly bool IsComposite;

        public RegexObject(string inputRow, string regexPattern, bool isComposite)
        {
            InputString = inputRow ?? throw new ArgumentNullException(nameof(inputRow));
            RegexPattern = regexPattern ?? throw new ArgumentNullException(nameof(regexPattern));
            IsComposite = isComposite;
        }

        public RegexObject(string regexPattern, bool isComposite)
        {
            InputString = string.Empty;
            RegexPattern = regexPattern ?? throw new ArgumentNullException(nameof(regexPattern));
            IsComposite = isComposite;
        }
    }
}
