using System;
using System.Text.RegularExpressions;
using Package.Utility.Enums;

namespace Package.Utility.Extensions
{

    public static class RegexTypeExtensions
    {
        public static string Extract(this RegexType type, string input, string regex, string? regexParam)
        {
            switch (type)
            {
                case RegexType.None:
                    throw new ArgumentException($"regex type is none");
                case RegexType.Match:
                    var newValueMatch = Regex.Match(
                        input,
                        regex,
                        RegexOptions.None,
                        TimeSpan.FromSeconds(5));
                    return string.IsNullOrEmpty(regexParam)
                        ? newValueMatch.Value
                        : newValueMatch.Groups[regexParam].Value;
                case RegexType.Replace:
                    return Regex.Replace(
                        input,
                        regex,
                        regexParam ?? "",
                        RegexOptions.None,
                        TimeSpan.FromSeconds(5));
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}