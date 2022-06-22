using System;
using System.Collections.Generic;

namespace Package.Utility.Collection
{
    public class Bag<T>: IBag<T>
        where T: class
    {
        private List<T> _items = new List<T>();

        public int Count => _items.Count;

        public Bag()
        {
                
        }
        
        public Bag(IEnumerable<T> items)
        {
            AddRange(items);
        }

        public List<T> TakeAll()
        {
            var takeItems = _items;
            _items = new List<T>();
            return takeItems;
        }

        public T? TakeOne(Predicate<T> predicate)
        {
            var res = _items.Find(predicate);
            if (res != null)
                _items.Remove(res);
            return res;
        }
        
        public List<T> TakeMany(Predicate<T> predicate)
        {
            var res = _items.FindAll(predicate);
            foreach (var item in res)
                    _items.Remove(item);
            return res;
        }

        public void Add(T item)
        {
            if (_items.Contains(item))
                throw new ArgumentException("Item already exist");
            _items.Add(item);
        }

        public void AddRange(IEnumerable<T> items)
        {
            foreach (var item in items)
                Add(item);
        }
    }
}