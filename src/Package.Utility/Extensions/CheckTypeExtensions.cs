using System;
using Package.Utility.Enums;

namespace Package.Utility.Extensions
{

    public static class CheckTypeExtensions
    {
        public static bool Check(this CheckType type, string valueSrc, string valueDst)
        {
            switch (type)
            {
                case CheckType.Equals:
                    return valueSrc == valueDst;
                case CheckType.NotEquals:
                    return valueSrc != valueDst;
                case CheckType.Contains:
                    return valueSrc.Contains(valueDst);
                case CheckType.NotContains:
                    return !valueSrc.Contains(valueDst);
                case CheckType.StartWith:
                    return valueSrc.StartsWith(valueDst);
                case CheckType.EndWith:
                    return valueSrc.EndsWith(valueDst);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}