using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPackage.DownloadSheet.Checks
{
    public static class ColumnCheckTypeExtensions
    {
        public static bool Check(this CheckType type, IEnumerable<string> values)
        {
            switch (type)
            {
                case CheckType.filled:
                    return values.All(a => !string.IsNullOrEmpty(a));
                case CheckType.notfilled:
                    return values.All(a => string.IsNullOrEmpty(a));
                case CheckType.identical:
                    return values.GroupBy(a => a).Count() == 1;
                case CheckType.notidentical:
                    return values.GroupBy(a => a).Count() != 1;
                case CheckType.unique:
                    return values.GroupBy(a => a).Count() == values.Count();
                default:
                    return false;                    
            }
        }

    }
}
