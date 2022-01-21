using CheckPackage.Core.Check;
using System;

namespace CheckPackage.Base.Checks
{
    public static class CheckOperatorTypeExtensions
    {

        public static bool Resolve(this CheckOperatorType operatorType, string value1, string? value2)
        {
            switch (operatorType)
            {
                case CheckOperatorType.equals:
                    return value1 == value2;
                case CheckOperatorType.notequals:
                    return value1 != value2;
                case CheckOperatorType.contains:
                    return value1 == null || value2 == null ? false : value1.Contains(value2);
                case CheckOperatorType.notcontains:
                    return value1 == null || value2 == null ? false : !value1.Contains(value2);
                case CheckOperatorType.startWith:
                    return value1 == null || value2 == null ? false : value1.StartsWith(value2);
                case CheckOperatorType.endWith:
                    return value1 == null || value2 == null ? false : value1.EndsWith(value2);
                default:
                    throw new ArgumentException("Incorrect type of operation check");
            }
        }
    }
}
