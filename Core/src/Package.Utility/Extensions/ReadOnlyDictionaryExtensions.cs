using System;
using System.Collections.Generic;
using System.Text;

namespace Package.Building.Extensions
{
    public static class ReadOnlyDictionaryExtensions
    {
        public static Dictionary<T1, T2> Copy<T1, T2>(this IReadOnlyDictionary<T1, T2>? dict)
        {
            Dictionary<T1, T2> result = new Dictionary<T1, T2>();
            if (dict != null)
                foreach (var dictItem in dict)                
                    result.Add(dictItem.Key, dictItem.Value);
            return result;
        }
    }
}
