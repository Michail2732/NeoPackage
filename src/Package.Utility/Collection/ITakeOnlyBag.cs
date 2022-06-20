using System;
using System.Collections.Generic;

namespace Package.Utility.Collection
{
    public interface ITakeOnlyBag<T>
        where T: class
    {
        List<T> TakeAll();
        T? TakeOne(Predicate<T> predicate);
        List<T> TakeMany(Predicate<T> predicate);
    }
}