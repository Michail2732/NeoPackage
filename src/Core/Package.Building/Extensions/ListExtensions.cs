using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Package.Building.Extensions
{
    public static class ListExtensions
    {

        public static void ReplaceItems<T>(this List<T> items, IEnumerable<T> replacedItems, T item)
        {
            if (items != null && replacedItems != null)
            {
                foreach (var replacedItem in replacedItems)
                    if (items.Contains(replacedItem))
                        items.Remove(replacedItem);
                items.Add(item);
            }            
        }


    }
}
