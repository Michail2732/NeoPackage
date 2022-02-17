using System;
using System.Collections.Generic;

namespace CheckPackage.Core.Regexes
{    
    public readonly struct RegexParceResult
    {
        public readonly string Pattern;
        public readonly IReadOnlyList<string> Groups;
        public readonly IReadOnlyDictionary<string, string>? InterpolatedValues;

        public RegexParceResult(string pattern, IReadOnlyList<string> groups)
        {
            Pattern = pattern ?? throw new ArgumentNullException(nameof(pattern));
            Groups = groups ?? throw new ArgumentNullException(nameof(groups));
            InterpolatedValues = null;
        }

        public RegexParceResult(string pattern, IReadOnlyList<string> groups,
            IReadOnlyDictionary<string, string>? interpolateValues) : this(pattern, groups)
        {
            InterpolatedValues = interpolateValues;
        }
    } 
}
