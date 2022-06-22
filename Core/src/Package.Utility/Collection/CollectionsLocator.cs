using System;
using System.Collections.Generic;
using Package.Domain;

namespace Package.Utility.Collection
{
    public class CollectionsLocator<TItem1, TItem2>
    {
        private readonly List<TItem1> _items1 = new List<TItem1>();
        private readonly List<TItem2> _items2 = new List<TItem2>();

        public IReadOnlyList<TItem1> Items1 => _items1;
        public IReadOnlyList<TItem2> Items2 => _items2;

        internal CollectionsLocator()
        {
        }

        internal void AddItem1(TItem1 checkRule)
        {
            if (_items1.Contains(checkRule))
                throw new ArgumentException(nameof(checkRule));
            _items1.Add(checkRule);
        }
        
        internal void AddItem2(TItem2 checkRule)
        {
            if (_items2.Contains(checkRule))
                throw new ArgumentException(nameof(checkRule));
            _items2.Add(checkRule);
        }
    }
    
    public sealed class CollectionsLocatorBuilder<TItem1, TItem2>
    {
        private readonly CollectionsLocator<TItem1, TItem2> _collectionLocator;

        public CollectionsLocatorBuilder()
        {
            _collectionLocator = new CollectionsLocator<TItem1, TItem2>();
        }

        public CollectionsLocatorBuilder<TItem1, TItem2> AddItem1(TItem1 item1)
        {
            _collectionLocator.AddItem1(item1);
            return this;
        }
        
        public CollectionsLocatorBuilder<TItem1, TItem2> AddItem2(TItem2 item2)
        {
            _collectionLocator.AddItem2(item2);
            return this;
        }

    }
}