using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Package.Infrastructure.MongoDb
{
    public class DbMapperStorage
    {
        private static readonly Dictionary<Type,IDbCapabilityMapper> _mappers 
            = new Dictionary<Type, IDbCapabilityMapper>();
        private static SpinLock _lock = new SpinLock();

        public static void AddMapper<TItem>(IDbMapper<TItem> mapper)
        where TItem: class
        {
            bool isLocked = false;
            try
            {
                _lock.Enter(ref isLocked);
                if (_mappers.ContainsKey(typeof(TItem)))
                    throw new ArgumentException("Mapper already exist");
                _mappers[typeof(TItem)] = mapper;
            }
            finally { if (isLocked) _lock.Exit(); }
        }

        public static IDbMapper<TItem> Get<TItem>()
            where TItem: class
        {
            bool isLocked = false;
            try
            {
                _lock.Enter(ref isLocked);
                if (!_mappers.ContainsKey(typeof(TItem)))
                    throw new ArgumentException("Mapper not exist");
                return (IDbMapper<TItem>)_mappers[typeof(TItem)];
            }
            finally { if (isLocked) _lock.Exit(); }
        }
    }
}