using System;
using System.Collections.Generic;
using System.Text;

namespace Package.Abstraction.Entities
{
    public readonly struct EntityInfo
    {
        public readonly string Id;
        public readonly string Name;
        public readonly uint Level;
        public readonly IEnumerable<Entity_> Children;

        public EntityInfo(string id, string name, uint level,
            IEnumerable<Entity_> children)
        {
            Id = id;
            Name = name;
            Level = level;
            Children = children ?? throw new ArgumentNullException(nameof(children));
        }
    }
}
