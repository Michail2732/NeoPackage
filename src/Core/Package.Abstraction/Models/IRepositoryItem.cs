using System;
using System.Collections.Generic;
using System.Text;

namespace Package.Abstraction.Entities
{
    public interface IRepositoryItem<TKey>
    {
        public TKey Id { get; }
    }
}
