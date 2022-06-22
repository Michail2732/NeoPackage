using System;
using System.Collections.Generic;

namespace Package.Utility.Collection
{
    public interface IBag<T>: ITakeOnlyBag<T> where T : class
    {
        void Add(T item);
        void AddRange(IEnumerable<T> items);
    }
    
}