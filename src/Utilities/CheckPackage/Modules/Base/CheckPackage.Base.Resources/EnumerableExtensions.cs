using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Core.Extensions
{
    public static class EnumerableExtensions
    {
        public static List<T> AppendToEnd<T,D>(this List<T> enumerable,
            Func<D, IEnumerable<T>> itemsFunc, D data)
        {
            enumerable.AddRange(itemsFunc.Invoke(data));
            return enumerable;
        }

    }
}
