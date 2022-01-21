using CheckPackage.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Tests.Core.Stubs
{
    public class StubRepositoryProvider : IRepositoryProvider
    {        

        public bool HasRepository<TData, TKey>()
             where TData : class, IEntity<TKey>
        {
            return true;
        }

        public Repository<TData, TKey> GetRepository<TData, TKey>()
             where TData : class, IEntity<TKey>
        {
            return new StubRepository<TData, TKey>(null);
        }
    }
}
