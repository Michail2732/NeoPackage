using System;
using System.Collections.Generic;
using System.Text;

namespace Package.Building.Extensions
{
    public static class ListExtensions
    {

        public static void ReplaceItems<T>(this List<T> items, IEnumerable<T> replacedItems, T item)
        {
            foreach (var replacedItem in replacedItems)            
                if (items.Contains(replacedItem))
                    items.Remove(replacedItem);
            items.Add(item);
        }


    }
}
