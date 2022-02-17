using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CheckPackage.Core.Regexes
{
    public readonly struct RegexMatchResult
    {
        public readonly string? Match;
        public readonly bool IsMatch;
        public readonly IReadOnlyDictionary<string, string>? Groupes;

        public RegexMatchResult(string? match, bool isMatch, IReadOnlyDictionary<string, string>? groupes)
        {
            Match = match;
            IsMatch = isMatch;
            Groupes = groupes;
        }

        public static RegexMatchResult BuildFromMatch(Regex regex, Match match, RegexParceResult parceResult)
        {
            if (!match.Success)
                return new RegexMatchResult(null, false, null);
            Dictionary<string, string> groupes = new Dictionary<string, string>();
            foreach (var parameter in regex.GetGroupNames())            
                groupes[parameter] = match.Groups[parameter].Value;
            if (parceResult.InterpolatedValues != null)
                foreach (var interpolatedValue in parceResult.InterpolatedValues)
                    groupes[interpolatedValue.Key] = interpolatedValue.Value;
            return new RegexMatchResult(match.Value, true, groupes);
        }
    }
}
