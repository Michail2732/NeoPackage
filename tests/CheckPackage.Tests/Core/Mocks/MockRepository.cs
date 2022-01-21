﻿using CheckPackage.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CheckPackage.Tests.Core.Mocks
{
    public class MockRepository<TData, TKey> : Repository<TData, TKey>
        where TData : class, IEntity<TKey>
    {
        private List<TData> _items = new List<TData>();

        public MockRepository<TData, TKey> SetItems(List<TData>? items)
        {
            _items = items ?? new List<TData>();
            return this;
        }

        public MockRepository(List<TData>? items)
        {
            SetItems(items);
        }

        public override IEnumerable<TData> Get()
        {
            return _items;
        }

        public override IEnumerable<TData> Get(Func<TData, bool> filter)
        {
            return _items.Where(filter);
        }

        public async override Task<IEnumerable<TData>> GetAsync(Func<TData, bool> filter, CancellationToken ct)
        {
            return _items.Where(filter);
        }

        public override TData? GetItem(string id)
        {
            return _items.FirstOrDefault(a => a.Id as string == id);
        }

        public async override Task<TData?> GetItemAsync(string id, CancellationToken ct)
        {
            return _items.FirstOrDefault(a => a.Id as string == id);
        }
    }
}