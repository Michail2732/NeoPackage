using CheckPackage.Core.Entities;
using System;
using System.Text.RegularExpressions;

namespace CheckPackage.Base.Extensions
{
    public static class ValueActionExtensions
    {

        public static bool Resolve(this ValueAction operatorType, string value1, string? value2)
        {
            switch (operatorType)
            {
                case ValueAction.equals:
                    return value1 == value2;
                case ValueAction.notequals:
                    return value1 != value2;
                case ValueAction.contains:
                    return value1 == null || value2 == null ? false : value1.Contains(value2);
                case ValueAction.notcontains:
                    return value1 == null || value2 == null ? false : !value1.Contains(value2);
                case ValueAction.startWith:
                    return value1 == null || value2 == null ? false : value1.StartsWith(value2);
                case ValueAction.endWith:
                    return value1 == null || value2 == null ? false : value1.EndsWith(value2);
                case ValueAction.regexmatch:
                    return value1 == null || value2 == null ? false : Regex.IsMatch(value1, value2, RegexOptions.None, TimeSpan.FromSeconds(10));
                case ValueAction.regexnotmatch:
                    return value1 == null || value2 == null ? false : !Regex.IsMatch(value1, value2, RegexOptions.None, TimeSpan.FromSeconds(10));
                default:
                    throw new ArgumentException("Incorrect type of action operation");
            }
        }
    }
}
