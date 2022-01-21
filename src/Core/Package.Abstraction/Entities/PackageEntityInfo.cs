using System;
using System.Collections.Generic;
using System.Text;

namespace Package.Abstraction.Entities
{
    public readonly struct PackageEntityInfo
    {
        public readonly string Id;
        public readonly string Name;
        public readonly uint Level;
        public readonly IEnumerable<PackageEntity> Children;

        public PackageEntityInfo(string id, string name, uint level,
            IEnumerable<PackageEntity> children)
        {
            Id = id;
            Name = name;
            Level = level;
            Children = children ?? throw new ArgumentNullException(nameof(children));
        }
    }
}
