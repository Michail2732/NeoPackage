using System;
using System.Collections.Generic;
using System.Text;

namespace Package.Abstraction.Entities
{
    public interface IEntity<TKey>
    {
        public TKey Id { get; }
    }
}
